using System;
using System.Collections.Generic;

namespace ProjectTemplate
{
    public partial class GameManager : Manager<GameManager>
    {
        #region Public Methods

        public partial void ChangeGameState(GameScene scene)
        {
            try
            {
                if (scene == null)
                {
                    return;
                }

                EnumLib.State state = EnumLib.NamesMapping[scene.GameStateEnum?.Value];

                ChangeGameState(state);
            }
            catch (Exception ex) when (ex is KeyNotFoundException or ArgumentOutOfRangeException or NullReferenceException)
            {
                try
                {
                    throw new Error.InvalidGameStateException(scene.GameStateEnum?.Value);
                }
                catch (Exception e)
                {
                    Error.Warn(e);
                }
            }
        }

        public partial void ChangeGameState(EnumLib.State state)
        {
            try
            {
                GameState gameState = EnumLib.StatesMapping[CurrentState = state];

                gameState.Event?.Invoke();
            }
            catch (Exception ex) when (ex is KeyNotFoundException or ArgumentOutOfRangeException or NullReferenceException)
            {
                try
                {
                    throw new Error.InvalidGameStateException(state.ToString());
                }
                catch (Exception e)
                {
                    Error.Warn(e);
                }
            }

        }

        #endregion
    }
}