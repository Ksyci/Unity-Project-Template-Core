using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectTemplate
{
    /// <summary>
    /// Handles initialization of core managers and persistent objects both in editor and at runtime.
    /// </summary>
    [InitializeOnLoad]
    public static class Initializer
    {
        #region Constantes

        private const string MANAGER_FOLDER_NAME = "Managers";

        #endregion

        #region Variables

        public static UnityEvent<GameScene> OnSceneChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Static constructor called once when the class is first accessed.
        /// Subscribes to the Unity Editor event to update scenes when the build settings change,
        /// and performs an initial scene update.
        /// </summary>
        static Initializer()
        {
            static void update()
            {
                ProjectProperties properties = ProjectProperties.Get();

                if(properties != null)
                {
                    properties.UpdateScenes();
                }
            }

            EditorBuildSettings.sceneListChanged += update;

            update();
        }

        /// <summary>
        /// Called before any scene is loaded at runtime to initialize managers, persistent objects, 
        /// and scene-related systems.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            CreateManagers();
            CreatePersistents();
            InitializeOnSceneChanged();
        }

        /// <summary>
        /// Creates manager GameObjects under a common folder and attaches their respective manager components.
        /// </summary>
        private static void CreateManagers()
        {
            GameObject folder = new(MANAGER_FOLDER_NAME);

            CreateManager<ScenesManager>(folder.transform);
            CreateManager<BackupsManager>(folder.transform);
            CreateManager<DatasManager>(folder.transform);
            CreateManager<SettingsManager>(folder.transform);
            CreateManager<UIsManager>(folder.transform);
            CreateManager<LanguagesManager>(folder.transform);
            CreateManager<ControllersManager>(folder.transform);
            CreateManager<AudioManager>(folder.transform);
            CreateManager<GameManager>(folder.transform);

            Object.DontDestroyOnLoad(folder);
        }

        /// <summary>
        /// Instantiates persistent <see cref="GameObject"/> defined in project properties under a dedicated folder 
        /// to survive scene loads.
        /// </summary>
        private static void CreatePersistents()
        {
            IReadOnlyCollection<GameObject> persistentObjects = ProjectProperties.Get().PersistentObjects;

            if (persistentObjects.Count > 0)
            {
                string name = Format.Polish(nameof(persistentObjects));
                GameObject folder = new(name);

                foreach (GameObject prefab in persistentObjects)
                {
                    GameObject obj = Object.Instantiate(prefab, folder.transform);
                    obj.name = prefab.name;
                }

                Object.DontDestroyOnLoad(folder);
            }
        }

        /// <summary>
        /// Initializes the <see cref="OnSceneChanged"/> event by assigning listeners and
        /// immediately invokes the event with the current active scene.
        /// </summary>
        private static void InitializeOnSceneChanged()
        {
            GameScene scene = ScenesManager.FindScene(SceneManager.GetActiveScene().name);

            OnSceneChanged = new();
            OnSceneChanged.AddListener(UIsManager.Instance.LoadUIs);
            OnSceneChanged.AddListener(ControllersManager.Instance.LoadController);
            OnSceneChanged.AddListener(AudioManager.Instance.LoadSoundtracks);
            OnSceneChanged.AddListener(GameManager.Instance.ChangeGameState);
            OnSceneChanged?.Invoke(scene);
        }

        /// <summary>
        /// Helper method to create and parent a <see cref="Manager{T}"/> of specified types under a given folder.
        /// </summary>
        /// <typeparam name="TManager">Type of the manager component to add.</typeparam>
        /// <param name="folder">Parent transform under which the manager is created.</param>
        private static void CreateManager<TManager>(Transform folder) where TManager : Manager<TManager>
        {
            GameObject manager = new(Format.Polish(typeof(TManager).Name));
            manager.AddComponent<TManager>();
            manager.transform.parent = folder;
        }

        #endregion
    }
}