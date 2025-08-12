using System.Collections.Generic;
using System.Linq;

namespace ProjectTemplate
{
    /// <summary>
    /// Provides centralized definitions for enumerations and their mappings to project-specific objects.
    /// </summary>
    /// <remarks>
    /// This class is used by the <see cref="GameManager"/> to populate and retrieve values from mapping dictionaries.
    /// It is the responsibility of the user to set up the enumeration values and ensure that the corresponding
    /// dictionary entries are correctly configured.
    /// </remarks>
    public static class EnumLib
    {
        #region Enumerations

        /// <summary>
        /// Defines the different game states used for flow control.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description><c>INIT</c> = Initial state before any gameplay starts (associated with the <see cref="SplashScreen"/>).</description></item>
        /// <item><description><c>START</c> = Startup sequence (during the <see cref="MainMenu"/>).</description></item>
        /// <item><description><c>LOADING</c> = Loading state for scenes or assets.</description></item>
        /// <item><description><c>IN_GAME</c> = Active gameplay state.</description></item>
        /// <item><description><c>PAUSED</c> = Paused state where gameplay is halted.</description></item>
        /// </list>
        /// </remarks>
        public enum State { INIT, START, LOADING, IN_GAME, PAUSED }

        /// <summary>
        /// Defines the available scenes by identifier.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description><c>MAIN_MENU</c> = The main menu scene of the game.</description></item>
        /// </list>
        /// </remarks>
        public enum Scene { MAIN_MENU }

        #endregion

        #region Properties

        private static IList<GameState> States => ProjectProperties.Get().GameStates.ToList();

        private static IList<GameScene> Scenes => ProjectProperties.Get().Scenes.ToList();

        #endregion

        #region Variables

        public static readonly Dictionary<string, State> NamesMapping = new()
        {
            { General.FindAt(States, 0)?.Name, State.INIT },
            { General.FindAt(States, 1)?.Name, State.START },
            { General.FindAt(States, 2)?.Name, State.LOADING },
            { General.FindAt(States, 3)?.Name, State.IN_GAME },
            { General.FindAt(States, 4)?.Name, State.PAUSED },
        };


        public static readonly Dictionary<State, GameState> StatesMapping = new()
        {
            { State.INIT, General.FindAt(States, 0) },
            { State.START, General.FindAt(States, 1) },
            { State.LOADING, General.FindAt(States, 2) },
            { State.IN_GAME, General.FindAt(States, 3) },
            { State.PAUSED, General.FindAt(States, 4) },
        };

        public static readonly Dictionary<Scene, GameScene> ScenesMapping = new()
        {
            { Scene.MAIN_MENU, General.FindAt(Scenes, 2) },
        };

        #endregion
    }
}