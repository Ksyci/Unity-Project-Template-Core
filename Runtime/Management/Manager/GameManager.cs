using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEditor.VersionControl.Asset;

namespace ProjectTemplate
{
    public partial class GameManager : Manager<GameManager>
    {
        #region Public Methods

        public partial void ChangeGameState(GameScene scene) => ChangeGameState(scene?.GameStateEnum?.Value);

        public partial void ChangeGameState(string stateName)
        {
            try
            {
                GameState[] states = ProjectProperties.Get().GameStates.ToArray();

                CurrentState = stateName;

                GameState state = states.FirstOrDefault(x => x?.Name == CurrentState);

                state?.Event?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException or NullReferenceException)
            {
                try
                {
                    throw new Error.InvalidGameStateException(stateName.ToString());
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