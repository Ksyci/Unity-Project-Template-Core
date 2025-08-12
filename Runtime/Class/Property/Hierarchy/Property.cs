using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Abstract base class representing a property with a localized name and a default value.
    /// </summary>
    [Serializable]
    public abstract class Property
    {
        #region Serialized

        [SerializeField]
        private Translator _name;

        #endregion

        #region Properties

        public abstract object DefaultValue { get; }

        public Translator Name => _name;

        #endregion
    }
}