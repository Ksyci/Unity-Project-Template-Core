using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTemplate
{
    /// <summary>
    /// Manages backups by providing creation, saving, loading, deleting, and autosaving functionalities.
    /// Handles backup files and their associated screenshots on disk.
    /// </summary>
    public partial class BackupsManager : Manager<BackupsManager>
    {
        #region Constantes

        private const string AUTOSAVE_WARNING = "The game has been autosaved.";
        private const string BACKUP_EXTENSION = ".dat";
        private const string SCREENSHOT_EXTENSION = ".png";
        private const string BACKUP_FOLDER_NAME = "Backups";
        private const string SCREENSHOT_FOLDER_NAME = "Screenshots";

        private const float CAPTURE_DURATION = 0.5f;

        #endregion

        #region Static

        private static string BackupsFolder => Path.Combine(Application.persistentDataPath, BACKUP_FOLDER_NAME);

        private static string ScreenshotsFolder => Path.Combine(Application.persistentDataPath, SCREENSHOT_FOLDER_NAME);

        #endregion

        #region Variables

        private float _autosaveTimer;

        private Coroutine _autosave;

        private Dictionary<int, Backup> _backups;


        #endregion

        #region Properties


        public bool IsAutosaveTimerPaused { get; set; }

        public Backup ActiveBackup { get; private set; }

        public UnityEvent OnBackupChanged { get; private set; }

        public int BackupCount => _backups.Count;

        public Backup this[int index] => General.FindWith(_backups, index);

        #endregion

        #region Methods

        /// <summary>
        /// Loads a screenshot associated with the given backup.
        /// </summary>
        /// <param name="backup">The backup whose screenshot is to be loaded.</param>
        /// <returns>The loaded Sprite or null if not found.</returns>
        public static partial Sprite LoadScreenshot(Backup backup);

        /// <summary>
        /// Writes the given backup data to disk.
        /// </summary>
        /// <param name="backup">The backup to write.</param>
        private static partial void WriteBackup(Backup backup);

        /// <summary>
        /// Deletes the backup file from disk.
        /// </summary>
        /// <param name="backup">The backup to erase.</param>
        private static partial void EraseBackup(Backup backup);

        /// <summary>
        /// Reads and deserializes a backup from a file path.
        /// </summary>
        /// <param name="path">The file path of the backup.</param>
        /// <returns>The deserialized Backup object or null if not found.</returns>
        private static partial Backup ReadBackup(string path);

        /// <summary>
        /// Creates a new backup with the specified name and saves it.
        /// </summary>
        /// <param name="name">The name of the new backup.</param>
        public partial void MakeNew(string name);

        /// <summary>
        /// Copies the active backup to a new index.
        /// </summary>
        /// <param name="index">The index for the copied backup.</param>
        public partial void Copy(int index);

        /// <summary>
        /// Saves the specified backup to disk and captures a screenshot.
        /// </summary>
        /// <param name="backup">The backup to save.</param>
        public partial void Save(Backup backup);

        /// <summary>
        /// Loads the given backup and loads its associated scene.
        /// </summary>
        /// <param name="backup">The backup to load.</param>
        public partial void Load(Backup backup);

        /// <summary>
        /// Deletes the specified backup and its screenshot, updates active backup if necessary.
        /// </summary>
        /// <param name="backup">The backup to delete.</param>
        public partial void Delete(Backup backup);

        /// <summary>
        /// Starts the autosave coroutine to periodically save backups.
        /// </summary>
        public partial void StartAutosave();

        /// <summary>
        /// Stops the autosave coroutine if running.
        /// </summary>
        public partial void StopAutosave();

        /// <summary>
        /// Changes the currently <see cref="ActiveBackup"/>.
        /// </summary>
        /// <param name="backup">The backup to set as active.</param>
        private partial void ChangeActiveBackup(Backup backup);

        /// <summary>
        /// Captures a screenshot for the specified backup asynchronously.
        /// </summary>
        /// <param name="backup">The backup for which to capture the screenshot.</param>
        private partial void CaptureScreenshot(Backup backup);

        /// <summary>
        /// Finds a unique hash for the given backup name.
        /// </summary>
        /// <param name="name">The name to find hash for.</param>
        /// <returns>A unique integer hash.</returns>
        private partial int FindHash(string name);

        /// <summary>
        /// Finds the next available index for a new backup.
        /// </summary>
        /// <returns>The next free integer index.</returns>
        private partial int FindIndex();

        #endregion
    }
}