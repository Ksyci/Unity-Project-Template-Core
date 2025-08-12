using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Holds configuration values that control <see cref="GameScene"/> management behavior.
    /// </summary>
    [Serializable]
    public class ScenesConfiguration
    {
        #region Serialized

        [SerializeField]
        private bool _hasLoadingScreen;

        [SerializeField]
        private float _loadingDelay;

        [SerializeField]
        private int _firstSceneIndex;

        [SerializeField]
        private int _loadingSceneIndex;

        #endregion

        #region Properties

        public bool HasLoadingScreen => _hasLoadingScreen;

        public float LoadingDelay => _loadingDelay;

        public int FirstSceneIndex => _firstSceneIndex;

        public int LoadingSceneIndex => _loadingSceneIndex;

        #endregion
    }
}