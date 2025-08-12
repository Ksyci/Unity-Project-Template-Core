using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a configurable key binding using a <see cref="KeyCode"/>.
    /// </summary>
    [Serializable]
    public class KeyBind
    {
        #region Serialized

        [SerializeField]
        private KeyCode _key;

        #endregion

        #region Properties

        public KeyCode Key { get => _key; set => _key = value; }

        #endregion
    }
}