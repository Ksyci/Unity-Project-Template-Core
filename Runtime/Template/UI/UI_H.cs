using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Abstract base class for all UI, providing basic display control and transition behavior.
    /// </summary>
    public abstract partial class UI : MonoBehaviour
    {
        #region Constantes

        public const float TRANSITION_DURATION = 0.25f;

        #endregion

        #region Variables

        protected CanvasGroup _display;

        #endregion

        #region Properties

        protected abstract Transition Transition { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Displays or hides the UI with a transition effect.
        /// </summary>
        /// <param name="isDislpayed">If true, shows the UI; otherwise, hides it.</param>
        public virtual partial void Display(bool isDislpayed);

        #endregion
    }
}