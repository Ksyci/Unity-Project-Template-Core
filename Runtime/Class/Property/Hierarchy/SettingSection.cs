using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a section grouping multiple <see cref="SettingTemplates"/> under a localized category.
    /// </summary>
    [Serializable]
    public class SettingSection
    {
        #region Serialized

        [SerializeField]
        private Translator _category;

        [SerializeReference]
        private SettingTemplate[] _settingTemplates;

        #endregion

        #region Properties

        public string NameOfSettingTemplates => nameof(_settingTemplates);

        public Translator Category => _category;

        public IReadOnlyCollection<SettingTemplate> SettingTemplates => _settingTemplates;

        #endregion
    }
}