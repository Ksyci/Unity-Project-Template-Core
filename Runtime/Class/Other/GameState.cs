using System;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a game state and is associated <see cref="UnityEvent"/>.
    /// </summary>
    [Serializable]
    public class GameState
    {
        #region Serialized

        [SerializeField]
        private string _name;

        [SerializeField]
        private UnityEvent _event;

        #endregion

        #region Properties

        public string Name => _name;

        public UnityEvent Event => _event;

        #endregion
    }
}