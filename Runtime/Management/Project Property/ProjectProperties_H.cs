using System.Collections.Generic;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Holds general project-wide configuration settings and references.
    /// </summary>
    [CreateAssetMenu(fileName = "Project_Properties", menuName = "Project Properties")]
    public partial class ProjectProperties : ScriptableObject
    {
        #region Static

        private static ProjectProperties _instance;

        #endregion

        #region Serialized

        [SerializeField]
        private int _pageIndex;

        [SerializeField]
        private string[] _languages;

        [SerializeField]
        private BackupsConfiguration _backupsConfiguration;

        [SerializeField]
        private GameObject[] _persistentObjects;

        [SerializeField]
        private GameState[] _gameStates;

        [SerializeField]
        private GameScene[] _scenes;

        [SerializeField]
        private ScenesConfiguration _sceneConfiguration;

        [SerializeReference]
        private SettingSection[] _settingSections;

        #endregion

        #region Properties

        public BackupsConfiguration BackupsConfiguration => _backupsConfiguration;

        public GameScene FirstScene => General.FindAt(_scenes, _sceneConfiguration.FirstSceneIndex);

        public GameScene LoadingScene => General.FindAt(_scenes, _sceneConfiguration.LoadingSceneIndex);

        public ScenesConfiguration ScenesConfiguration => _sceneConfiguration;

        public IReadOnlyList<string> Languages => _languages;

        public IReadOnlyList<GameState> GameStates => _gameStates;

        public IReadOnlyList<GameScene> Scenes => _scenes;

        public IReadOnlyList<GameObject> PersistentObjects => _persistentObjects;

        public IReadOnlyList<SettingSection> SettingSections => _settingSections;

        #endregion

        #region Methods

        /// <summary>
        /// Returns the singleton instance of the <see cref="ProjectProperties"/>.
        /// </summary>
        /// <returns>The <see cref="ProjectProperties"/> instance.</returns>
        public static partial ProjectProperties Get();

        /// <summary>
        /// Returns the list of all scene names defined in the <see cref="ProjectProperties"/>.
        /// </summary>
        /// <returns>An array of scene names as <see cref="string"/>.</returns>
        private partial string[] FindSceneNames();

        /// <summary>
        /// Updates the <see cref="Scenes"/> list based on the scenes currently included in the build settings.
        /// </summary>
        public partial void UpdateScenes();

        /// <summary>
        /// Updates the <see cref="TypeEnum{T}"/> of the <see cref="SettingTemplate"/> of the <see cref="ProjectProperties"/>.
        /// </summary>
        public partial void UpdatePropertyTypeEnum();

        #endregion
    }
}