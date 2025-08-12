using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a customizable scene in the game.
    /// </summary>
    [Serializable]
    public class GameScene
    {
        #region Serialized

        [SerializeField, HideInInspector]
        private string _name;

        [SerializeField]
        private GameStateEnum _gameStateEnum;

        [SerializeField]
        private UI _defaultUi;

        [SerializeField]
        private UI[] _otherUis;

        [SerializeField]
        private Controller[] _controllers;

        [SerializeField]
        private Playlist _playlists;

        #endregion

        #region Properties

        public string Name => _name;

        public DynamicEnum GameStateEnum => _gameStateEnum;

        public Playlist Playlists => _playlists;

        public UI DefaultUi => _defaultUi;

        public IReadOnlyList<UI> UIs => new[] { _defaultUi }.Concat(_otherUis).ToList();

        public IReadOnlyList<Controller> Controllers => _controllers;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScene"/>.
        /// </summary>
        /// <param name="name">The name of the scene.</param>
        /// <param name="gameStates">The potential game states.</param>
        public GameScene(string name, params string[] gameStates)
        {
            _name = name;

            _gameStateEnum = new(gameStates);
        }

        #endregion
    }
}