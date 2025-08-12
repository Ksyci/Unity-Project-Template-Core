using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Manages application settings: loading, saving, validating, and updating settings from JSON storage.
    /// </summary>
    public partial class SettingsManager : Manager<SettingsManager>
    {
        #region Constantes

        private const string SETTING_SHEET_NAME = "settings.json";
        private const string JSON_RECREATE_MESSAGE = SETTING_SHEET_NAME + " has been recreated.";

        #endregion

        #region Static

        private static string SettingsFilePath => Path.Combine(Application.persistentDataPath, SETTING_SHEET_NAME);

        #endregion

        #region Variables

        private Dictionary<string, List<Setting>> _settings;

        #endregion

        #region Properties

        public IReadOnlyDictionary<string, List<Setting>> Settings => _settings;

        public Setting this[string settingName] => _settings
            .SelectMany(p => p.Value)
            .FirstOrDefault(s => s.Name == settingName);

        #endregion

        #region Methods

        /// <summary>
        /// Creates the default settings dictionary based on project property templates and saves it to disk.
        /// </summary>
        /// <returns>A new dictionary of settings categorized by section.</returns>
        public static partial Dictionary<string, List<Setting>> CreateSettings();

        /// <summary>
        /// Saves the current settings dictionary to the JSON file.
        /// </summary>
        /// <param name="settings">The settings dictionary to save.</param>
        private static partial void SaveSettings(Dictionary<string, List<Setting>> settings);

        /// <summary>
        /// Loads settings from the JSON file and validates their integrity.
        /// </summary>
        /// <returns>The loaded and validated dictionary of settings.</returns>
        private static partial Dictionary<string, List<Setting>> LoadSettings();

        /// <summary>
        /// Validates the loaded settings dictionary against project templates, recreating if invalid.
        /// </summary>
        /// <param name="settings">Reference to the settings dictionary to validate.</param>
        private static partial void ValidateLoad(ref Dictionary<string, List<Setting>> settings);

        /// <summary>
        /// Updates the settings by saving the current in-memory settings to persistent storage.
        /// </summary>
        public partial void UpdateSettings();

        #endregion
    }
}