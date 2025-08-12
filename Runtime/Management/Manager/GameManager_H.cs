namespace ProjectTemplate
{
    /// <summary>
    /// Singleton manager responsible for handling the overall game state.
    /// </summary>
    public partial class GameManager : Manager<GameManager>
    {
        #region Properties

        public EnumLib.State CurrentState { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the game state based on the specified scene.
        /// </summary>
        /// <param name="scene">The scene that triggers the state change.</param>
        public partial void ChangeGameState(GameScene scene);

        /// <summary>
        /// Changes the game state to the specified state.
        /// </summary>
        /// <param name="state">The new game state to set.</param>
        public partial void ChangeGameState(EnumLib.State state);

        #endregion
    }
}