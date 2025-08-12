using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a <see cref="string"/> <see cref="Property"/> with formatting, placeholder text, and maximum character limit.
    /// </summary>
    [Serializable]
    public class String : Property
    {
        #region Constantes

        private const int MAX_CHAR = 1000;

        #endregion

        #region Serialized

        [SerializeField]
        private Translator _placeHolder;

        [SerializeField]
        private Format.Type _formatType;

        [SerializeField, Range(1, MAX_CHAR)]
        private int _maxCharacters;

        #endregion

        #region Properties

        public Translator PlaceHolder => _placeHolder;

        public Format.Type FormatType => _formatType;

        public int MaxCharacters => _maxCharacters;

        public override object DefaultValue => string.Empty;

        #endregion
    }
}