using System.Collections.Generic;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Abstract base class for managing and executing key-based events within a scene or system.
    /// </summary>
    public abstract partial class Controller : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        protected KeyEvent[] _events;

        #endregion

        #region Properties

        protected Dictionary<string, KeyEvent> _eventsMapping;

        private KeyEvent[] _oneTimeEvents;

        private KeyEvent[] _persistentEvents;

        #endregion

        #region Methods

        /// <summary>
        /// Executes <see cref="KeyEvent"/> based on the input state.
        /// </summary>
        /// <param name="isOnTimeEvents">If true, processes one-time <see cref="KeyEvent"/> (key down); if false, processes persistent <see cref="KeyEvent"/> (key held).</param>
        private partial void Execute(bool isOnTimeEvents);

        #endregion
    }
}

