using System.Collections;
using System.Collections.Generic;

namespace ProjectTemplate
{
    /// <summary>
    /// Manages scene loading, unloading, and transitions with optional loading screens.
    /// </summary>
    public partial class ScenesManager : Manager<ScenesManager>
    {
        #region Variables

        private Queue<GameScene> _loadingQueue;

        #endregion

        #region Properties

        public GameScene CurrentScene => FindScene(BackupsManager.Instance.ActiveBackup.Scene);

        public bool IsLoading => _loadingQueue.Count > 0;

        #endregion

        #region Methods

        /// <summary>
        /// Finds a scene by its name from the project properties.
        /// </summary>
        /// <param name="sceneName">The name of the scene to find.</param>
        /// <returns>The matching Scene object, or null if not found.</returns>
        public static partial GameScene FindScene(string sceneName);

        /// <summary>
        /// Coroutine that manages the instant loading sequence.
        /// </summary>
        /// <returns>IEnumerator for coroutine.</returns>
        private partial IEnumerator InstantLoadingSequence();

        /// <summary>
        /// Initiates loading of a scene by name, optionally using a loading screen.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        public partial void LoadScene(string sceneName);

        /// <summary>
        /// Coroutine that manages the loading sequence, including loading screens and scene transitions.
        /// </summary>
        /// <returns>IEnumerator for coroutine.</returns>
        private partial IEnumerator LoadingSequence();

        /// <summary>
        /// Validates if the given scene name is not null, logs warning if invalid.
        /// </summary>
        /// <param name="sceneName">The scene name to validate.</param>
        /// <returns>True if valid; otherwise false.</returns>
        private partial bool ValidateScene(string sceneName);

        #endregion
    }
}