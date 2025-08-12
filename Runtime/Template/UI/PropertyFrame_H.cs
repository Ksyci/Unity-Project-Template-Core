using TMPro;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Base UI frame for displaying and interacting with a specific <see cref="Property"/> type,
    /// binding it to a <see cref="Setting"/>.
    /// </summary>
    /// <typeparam name="TProperty">The specific <see cref="Property"/> type to display and manage.</typeparam>
    public abstract partial class PropertyFrame<TProperty> : UIElement where TProperty : Property
    {
        #region Serialized

        [Header(Format.REFERENCES_HEADER)]

        [SerializeField, ReadOnly]
        private TextMeshProUGUI _title;

        #endregion

        #region Variables

        private Setting _bindedSetting;

        #endregion

        #region Properties

        public object Value { get => _bindedSetting.Value; set => _bindedSetting.Value = value; }

        protected string Language => LanguagesManager.Instance.ActiveLanguage;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the frame with the given property and its associated setting,
        /// allowing the UI to display and modify the setting's value.
        /// </summary>
        /// <param name="property">The property to be represented by the frame.</param>
        /// <param name="setting">The setting bound to the property.</param>
        public virtual partial void Initialize(TProperty property, Setting setting);

        #endregion
    }
}