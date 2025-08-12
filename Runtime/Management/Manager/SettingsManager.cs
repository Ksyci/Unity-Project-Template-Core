using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    public partial class SettingsManager : Manager<SettingsManager>
    {
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            _settings = File.Exists(SettingsFilePath) ? LoadSettings() : CreateSettings();
        }

        #endregion

        #region Static Methods

        public static partial Dictionary<string, List<Setting>> CreateSettings()
        {
            Dictionary<string, List<Setting>> settings = new();

            foreach (SettingSection section in ProjectProperties.Get().SettingSections)
            {
                string category = section?.Category?.Default;

                if (!string.IsNullOrEmpty(category) && !settings.ContainsKey(category))
                {
                    settings.Add(category, new());

                    foreach (SettingTemplate template in section.SettingTemplates)
                    {
                        if (template != null)
                        {
                            string name = template.Property.Name.Default;

                            object value = template.Property.DefaultValue;

                            Setting setting = new(name, value);

                            settings[category].Add(setting);
                        }
                    }
                }
            }

            SaveSettings(settings);

            return settings;
        }

        private static partial void SaveSettings(Dictionary<string, List<Setting>> settings)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);

            File.WriteAllText(SettingsFilePath, json);
        }

        private static partial Dictionary<string, List<Setting>> LoadSettings()
        {
            string json = File.ReadAllText(SettingsFilePath);

            var settings = JsonConvert.DeserializeObject<Dictionary<string, List<Setting>>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            ValidateLoad(ref settings);

            return settings;
        }

        private static partial void ValidateLoad(ref Dictionary<string, List<Setting>> settings)
        {
            foreach (var (key, settingList) in settings)
            {
                // Trouver la catégorie correspondante
                var category = ProjectProperties.Get().SettingSections.FirstOrDefault(c => c.Category.Default == key);
                if (category == null)
                {
                    try
                    {
                        throw new Error.InvalidCategoryException(key);
                    }
                    catch (Exception e)
                    {
                        Error.Warn(e);
                        Debug.Log(JSON_RECREATE_MESSAGE);
                        settings = CreateSettings();
                        return;
                    }
                }

                // Vérifier que chaque setting est défini dans un template
                var section = ProjectProperties.Get().SettingSections.FirstOrDefault(s => s.Category.Default == key);

                foreach (var setting in settingList)
                {
                    bool exists = section.SettingTemplates.Any(template => template.Property.Name.Default == setting.Name);

                    if (!exists)
                    {
                        try
                        {
                            throw new Error.InvalidSettingException(setting.Name, key);
                        }
                        catch (Exception e)
                        {
                            Error.Warn(e);
                            Debug.Log(JSON_RECREATE_MESSAGE);
                            settings = CreateSettings();
                        }
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public partial void UpdateSettings() => SaveSettings(_settings);

        #endregion
    }
}