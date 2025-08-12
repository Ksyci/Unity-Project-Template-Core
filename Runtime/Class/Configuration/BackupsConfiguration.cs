using System;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField]
        private string _activeBackupPath;

        #endregion

        #region Properties

        public float AutoSaveDelay => _autoSaveDelay;

        public int MaxBackup => _maxBackup;

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves the active <see cref="Backup"/> from the provided collection,
        /// matching the internally stored active backup path.
        /// </summary>
        /// <param name="backups">The collection where to retreive the active <see cref="Backup"/>.</param>
        /// <returns>The active <see cref="Backup"/> if found; otherwise, null.</returns>
        public Backup GetActiveBackup(IEnumerable<Backup> backups)
            => backups?.FirstOrDefault(x => x.Path == _activeBackupPath);

        /// <summary>
        /// Sets the active <see cref="Backup"/> by storing its path internally.
        /// </summary>
        /// <param name="backup">The <see cref="Backup"/> to set as active. If null, the active path will be cleared.</param>
        public void SetActiveBackup(Backup backup)
            => _activeBackupPath = backup?.Path;

        #endregion
    }
}