using System;
using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Holds multilingual <see cref="Translation"/> and provides access by language.
    /// </summary>
    [Serializable]
    public class Translator
    {
        #region Serialized

        [SerializeField]
        private Translation[] _translations;

        #endregion

        #region Properties

        public string Default => General.FindAt(_translations, 0)?.Value;

        public string this[string language] => _translations?.FirstOrDefault(t => t.Language == language)?.Value;

        #endregion
    }
}