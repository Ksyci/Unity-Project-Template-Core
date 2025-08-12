using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a template for a <see cref="Setting"/>, linking a <see cref="TypeEnum{T}"/> of <see cref="global::Property"/> to an instance of that <see cref="global::Property"/>.
    /// </summary>
    [Serializable]
    public class SettingTemplate
    {
        #region Serialized

        [SerializeField]
        private TypeEnum<Property> _enum;

        [SerializeReference]
        private Property _property;

        #endregion

        #region Properties

        public bool HasChanged => _enum.Value != _property.GetType().Name;

        public string NameOfProperty => nameof(_property);

        public Property Property => _property;

        public Type Cast => _enum.Type;

        public TypeEnum<Property> Type => _enum;

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new instance of <see cref="SettingTemplate"/> by initializing the internal type 
        /// and instantiating the associated <see cref="ProjectTemplate.Property"/>.
        /// </summary>
        public SettingTemplate()
        {
            _enum = new();

            _property = Activator.CreateInstance(Cast) as Property;
        }

        #endregion
    }

}