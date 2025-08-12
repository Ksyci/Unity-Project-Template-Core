using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents an <see cref="Enum"/> <see cref="Property"/> with a default selected index and localized type names.
    /// </summary>
    [Serializable]
    public class Enum : Property
    {
        #region Serialized

        [SerializeField]
        private Translator[] _types;

        #endregion

        #region Properties

        public IReadOnlyCollection<Translator> Types => _types;

        public override object DefaultValue => 0;

        #endregion
    }
}