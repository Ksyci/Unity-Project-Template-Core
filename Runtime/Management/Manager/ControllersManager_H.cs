using System.Collections.Generic;

namespace ProjectTemplate
{
    /// <summary>
    /// Manages controllers by instantiating and organizing them per scene.
    /// </summary>
    public partial class ControllersManager : Manager<ControllersManager>
    {
        #region Variables

        private List<Controller> _controllers;

        #endregion

        #region Methods

        /// <summary>
        /// Instantiates controllers from the given scene and adds them to the manager's list.
        /// </summary>
        /// <param name="scene">The scene containing the controllers to load.</param>
        public partial void LoadController(GameScene scene);

        #endregion
    }
}