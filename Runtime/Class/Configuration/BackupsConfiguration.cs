using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Holds configuration values that control <see cref="Backup"/> behavior and selection.
    /// </summary>
    [Serializable]
    public class BackupsConfiguration
    {
        #region Serialized

        [SerializeField]
        private float _autoSaveDelay;

        [SerializeField]
        private int _maxBackup;

        #endregion

        #region Properties

        public float AutoSaveDelay => _autoSaveDelay;

        public int MaxBackup => _maxBackup;

        #endregion
    }
}