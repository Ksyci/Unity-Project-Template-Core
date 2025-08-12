using System.Collections.Generic;

namespace ProjectTemplate
{
    /// <summary>
    /// Manages UI elements by loading, showing, adding, removing, and replacing UI screens and menus.
    /// </summary>
    public partial class UIsManager : Manager<UIsManager>
    {
        #region Constantes

        private const string SCREENS_FOLDER_NAME = "Screens";
        private const string MENUS_FOLDER_NAME = "Menus";

        #endregion

        #region Variables

        private Dictionary<string, UI> _uis;

        private Stack<(UI, bool)> _activeUis;

        #endregion

        #region Methods

        /// <summary>
        /// Loads UI elements (screens and menus) for the given scene.
        /// </summary>
        /// <param name="scene">The scene whose UIs are to be loaded.</param>
        public partial void LoadUIs(GameScene scene);

        /// <summary>
        /// Displays or hides all active UIs depending on the given flag.
        /// </summary>
        /// <param name="isDisplayed">True to display UIs, false to hide.</param>
        public partial void ShowAll(bool isDisplayed);

        /// <summary>
        /// Adds a menu UI by name, optionally hiding the last displayed menu.
        /// </summary>
        /// <param name="menuName">The name of the menu to add.</param>
        /// <param name="isLastDisplayed">Whether the last displayed menu remains visible.</param>
        public partial void Add(string menuName, bool isLastDisplayed);

        /// <summary>
        /// Removes the current active UI and optionally displays the next one in the stack.
        /// </summary>
        /// <param name="isNextDisplayed">True to display the next UI after removal, false otherwise.</param>
        public partial void Remove(bool isNextDisplayed);

        /// <summary>
        /// Replaces all active UIs with a new menu specified by name.
        /// </summary>
        /// <param name="menuName">The name of the menu to display.</param>
        public partial void Replace(string menuName);

        /// <summary>
        /// Creates a folder GameObject and instantiates UI elements of type T for the given scene.
        /// </summary>
        /// <typeparam name="T">Type of UI elements to create (Screen or Menu).</typeparam>
        /// <param name="scene">The scene containing the UIs.</param>
        /// <param name="name">The folder name for the instantiated UI GameObjects.</param>
        private partial void CreateFolder<T>(GameScene scene, string name) where T : UI;

        #endregion
    }
}