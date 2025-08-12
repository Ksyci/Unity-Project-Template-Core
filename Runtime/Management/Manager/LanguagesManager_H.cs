using UnityEngine.Events;

namespace ProjectTemplate
{
    /// <summary>
    /// Manages language settings and active language selection.
    /// </summary>
    public partial class LanguagesManager : Manager<LanguagesManager>
    {
        #region Constantes

        public const string LANGUAGE_SETTING_NAME = "Language";

        #endregion

        #region Variables

        private Setting _languageSetting;

        #endregion

        #region Properties

        public string ActiveLanguage => FindActiveLanguage();

        #endregion

        #region Methods

        /// <summary>
        /// Binds an action to the language setting change event and immediately invokes it.
        /// </summary>
        /// <param name="action">The action to bind and invoke.</param>
        public partial void BindAndApply(UnityAction action);

        /// <summary>
        /// Retrieves the currently active language from the project settings.
        /// </summary>
        /// <returns>The active language as a string.</returns>
        private partial string FindActiveLanguage();

        #endregion
    }
}