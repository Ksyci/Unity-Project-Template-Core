using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a <see cref="bool"/> <see cref="Property"/> with localized true and false text representations.
    /// </summary>
    [Serializable]
    public class Boolean : Property
    {
        #region Serialized

        [SerializeField]
        private bool _defaultBoolean;

        [SerializeField]
        private Translator _trueText;

        [SerializeField]
        private Translator _falseText;

        #endregion

        #region Properties

        public bool DefaultBoolean => _defaultBoolean;

        public Translator TrueText => _trueText;

        public Translator FalseText => _falseText;

        public override object DefaultValue => _defaultBoolean;

        #endregion
    }
}