using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a localized <see cref="string"/> value associated with a specific language.
    /// </summary>
    [Serializable]
    public class Translation
    {
        #region Serialized

        [SerializeField]
        private string _language;

        [SerializeField]
        private string _value;

        #endregion

        #region Properties

        public string Language => _language;

        public string Value => _value;

        #endregion
    }
}