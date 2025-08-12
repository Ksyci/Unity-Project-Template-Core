using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a <see cref="KeyCode"/> <see cref="Property"/> with a default key binding and a set of valid key bindings.
    /// </summary>
    [Serializable]
    public class Key : Property
    {
        #region Serialized

        [SerializeField]
        private KeyBind _defaultBind;

        [SerializeField]
        private KeyBind[] _validBind;

        #endregion

        #region Properties

        public KeyCode DefaultKey => _defaultBind.Key;

        public IReadOnlyCollection<KeyCode> ValidKeys => _validBind.Select(x => x.Key).ToList();

        public override object DefaultValue => (int)_defaultBind.Key;

        #endregion
    }
}