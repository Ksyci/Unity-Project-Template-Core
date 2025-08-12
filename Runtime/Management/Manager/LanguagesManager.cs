using System;
using System.Linq;
using UnityEngine.Events;

namespace ProjectTemplate
{
    public partial class LanguagesManager : Manager<LanguagesManager>
    {
        #region Unity Methods

        private void Start() => _languageSetting = SettingsManager.Instance[LANGUAGE_SETTING_NAME];

        #endregion

        #region Public Methods

        public partial void BindAndApply(UnityAction action)
        {
            _languageSetting?.OnChange.AddListener(action);

            action();
        }

        #endregion

        #region Private Methods

        private partial string FindActiveLanguage()
        {
            string[] languages = ProjectProperties.Get().Languages.ToArray();

            int index = _languageSetting != null ? Convert.ToInt32(_languageSetting.Value) : 0;

            try
            {
                return languages.Length != 0 ? languages[index] : string.Empty;
            }
            catch (IndexOutOfRangeException)
            {
                return languages[0];
            }
        }

        #endregion
    }
}