using UnityEngine.Events;

namespace ProjectTemplate
{
    /// <summary>
    /// Abstract base class for menu-type UI elements, handling navigation and display updates.
    /// </summary>
    public abstract partial class Menu : UI
    {
        #region Properties

        protected override Transition Transition => new Fade(_display, TRANSITION_DURATION);

        #endregion

        #region Methods

        /// <summary>
        /// Returns to the previous menu in the navigation stack.
        /// </summary>
        protected static partial void Return();

        /// <summary>
        /// Navigates to a new menu by its name.
        /// </summary>
        /// <param name="nextMenu">The name of the next menu to open.</param>
        protected static partial void GoTo(string nextMenu);

        /// <summary>
        /// Updates the menu display elements (e.g., texts, visuals) when the menu is shown.
        /// </summary>
        protected virtual partial void UpdateDisplay();

        #endregion
    }
}