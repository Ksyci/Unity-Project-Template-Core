namespace ProjectTemplate
{
    /// <summary>
    /// Singleton manager responsible for handling the overall game state.
    /// </summary>
    public partial class GameManager : Manager<GameManager>
    {
        #region Properties

        public string CurrentState { get; private set; }

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
        /// <param name="stateName">The new game state name.</param>
        public partial void ChangeGameState(string stateName);

        #endregion
    }
}