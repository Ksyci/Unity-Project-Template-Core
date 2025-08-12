using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTemplate
{
    public partial class ScenesManager : Manager<ScenesManager>
    {
        #region Unity Methods

        private void Start() => _loadingQueue = new();

        #endregion

        #region Static Methods

        public static partial GameScene FindScene(string sceneName)
            => ProjectProperties.Get().Scenes.FirstOrDefault(x => x.Name == sceneName);

        #endregion

        #region Public Methods

        public partial void LoadScene(string sceneName)
        {
            if (!ValidateScene(sceneName))
            {
                return;
            }

            GameScene nextScene = FindScene(sceneName);

            if (!IsLoading)
            {
                _loadingQueue.Enqueue(nextScene);

                if (ProjectProperties.Get().ScenesConfiguration.HasLoadingScreen)
                {
                    StartCoroutine(LoadingSequence());
                }
                else
                {
                    StartCoroutine(InstantLoadingSequence());
                }
            }
        }

        #endregion

        #region Private Methods

        private partial IEnumerator LoadingSequence()
        {
            while (IsLoading)
            {
                GameScene loadingScene = ProjectProperties.Get().LoadingScene;
                GameScene activeScene = FindScene(SceneManager.GetActiveScene().name);
                GameScene nextScene = _loadingQueue.Dequeue();

                float loadingDelay = ProjectProperties.Get().ScenesConfiguration.LoadingDelay;

                AsyncOperation loadLoading = SceneManager.LoadSceneAsync(loadingScene.Name, LoadSceneMode.Additive);
                yield return new WaitUntil(() => loadLoading.isDone);

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadingScene.Name));
                yield return new WaitForEndOfFrame();

                Initializer.OnSceneChanged?.Invoke(loadingScene);
                yield return new WaitForSeconds(UI.TRANSITION_DURATION);

                AsyncOperation unloadCurrent = SceneManager.UnloadSceneAsync(activeScene.Name);
                yield return new WaitUntil(() => unloadCurrent.isDone);

                AsyncOperation loadTarget = SceneManager.LoadSceneAsync(nextScene.Name, LoadSceneMode.Additive);
                yield return new WaitUntil(() => loadTarget.isDone);

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene.Name));
                yield return new WaitForSeconds(loadingDelay);

                Initializer.OnSceneChanged?.Invoke(nextScene);
                yield return new WaitForSeconds(UI.TRANSITION_DURATION);

                AsyncOperation unloadLoading = SceneManager.UnloadSceneAsync(loadingScene.Name);
                yield return new WaitUntil(() => unloadLoading.isDone);
            }
        }

        private partial IEnumerator InstantLoadingSequence()
        {
            while (IsLoading)
            {
                GameScene nextScene = _loadingQueue.Dequeue();

                AsyncOperation loadLoading = SceneManager.LoadSceneAsync(nextScene.Name, LoadSceneMode.Single);
                yield return new WaitUntil(() => loadLoading.isDone);

                Initializer.OnSceneChanged?.Invoke(nextScene);
                yield return new WaitForEndOfFrame();
            }
        }

        private partial bool ValidateScene(string sceneName)
        {
            if (sceneName == null)
            {
                try
                {
                    throw new Error.SceneNotFoundException(sceneName);
                }
                catch (Exception e)
                {
                    Error.Warn(e);
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}