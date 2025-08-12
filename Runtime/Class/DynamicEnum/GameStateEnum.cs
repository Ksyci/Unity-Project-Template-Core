using System;

namespace ProjectTemplate
{
    /// <summary>
    /// <see cref="DynamicEnum"/> that represents a <see cref="GameState"/>.
    /// </summary>
    /// <remarks>
    /// Most of the logic for it is handled in its associated <see cref="BasicDrawer"/>.
    /// </remarks>
    [Serializable]
    public class GameStateEnum : DynamicEnum
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="GameStateEnum"/> class.
        /// </summary>
        public GameStateEnum(params string[] gameStates) : base(gameStates) { }

        #endregion
    }
}