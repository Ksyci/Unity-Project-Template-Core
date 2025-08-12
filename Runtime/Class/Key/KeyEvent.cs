using System;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTemplate
{
    /// <summary>
    /// Encapsulates a <see cref="KeyBind"/> and its associated <see cref="UnityEvent"/>.
    /// </summary>
    [Serializable]
    public class KeyEvent
    {
        #region Serialized

        [SerializeField, HideInInspector]
        private string _name;

        [SerializeField]
        private bool _isOnlyOnKeyDown;

        [SerializeField]
        private KeyBind _bind;

        [SerializeField]
        private UnityEvent _onKey;

        #endregion

        #region Properties

        public string Name { get => _name; set => _name = value; }

        public KeyCode Key { get => _bind.Key; set => _bind.Key = value; }

        public bool IsOneTimeEvent => _isOnlyOnKeyDown;

        public UnityEvent OnKey => _onKey;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEvent"/> class with the specified key name.
        /// </summary>
        /// <param name="name">The name identifying the <see cref="KeyEvent"/>.</param>
        public KeyEvent(string name)
        {
            _name = name;
            _onKey = new();
        }

        #endregion
    }
}