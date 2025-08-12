using System.Collections;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Abstract base class for UI transitions using a <see cref="CanvasGroup"/> to control display effects.
    /// </summary>
    public abstract class Transition
    {
        #region Variables

        protected CanvasGroup _display;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the transition with the target <see cref="CanvasGroup"/>.
        /// </summary>
        /// <param name="display">Canvas group to apply the transition effects on.</param>
        public Transition(CanvasGroup display) => _display = display;

        /// <summary>
        /// Plays the transition effect.
        /// </summary>
        /// <param name="isDisplayed">If true, shows the UI; otherwise hides it.</param>
        /// <returns>Coroutine enumerator for the transition.</returns>
        public abstract IEnumerator Play(bool isDisplayed);

        #endregion
    }
}