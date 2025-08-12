using System.Collections;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// A transition that makes a <see cref="CanvasGroup"/> appear or disappear instantly.
    /// </summary>
    public class Appear : Transition
    {
        #region Methods

        /// <summary>
        /// Creates an instant-appearance transition.
        /// </summary>
        /// <param name="display">The canvas group to control.</param>
        public Appear(CanvasGroup display) : base(display) { }

        /// <summary>
        /// Plays the instant show/hide transition.
        /// </summary>
        /// <param name="isDisplayed">True to show, false to hide.</param>
        public override IEnumerator Play(bool isDisplayed)
        {
            _display.blocksRaycasts = isDisplayed;
            _display.alpha = isDisplayed ? 1.0f : 0.0f;

            yield return null;
        }

        #endregion
    }
}