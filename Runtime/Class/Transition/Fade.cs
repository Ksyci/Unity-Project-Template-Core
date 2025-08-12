using System.Collections;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// A transition that fades a <see cref="CanvasGroup"/> in or out over a specified duration.
    /// </summary>
    public class Fade : Transition
    {
        #region Variables

        private readonly float _duration;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a fade transition with the specified canvas group and duration.
        /// </summary>
        /// <param name="display">The canvas group to fade.</param>
        /// <param name="duration">The duration of the fade in seconds.</param>
        public Fade(CanvasGroup display, float duration) : base(display) => _duration = duration;

        /// <summary>
        /// Plays the fade transition coroutine.
        /// </summary>
        /// <param name="isDisplayed">True to fade in, false to fade out.</param>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        public override IEnumerator Play(bool isDisplayed)
        {
            _display.blocksRaycasts = isDisplayed;

            float timer = 0.0f;
            float from = isDisplayed ? 0.0f : 1.0f;
            float to = isDisplayed ? 1.0f : 0.0f;

            _display.alpha = from;

            while (timer < _duration)
            {
                _display.alpha = Mathf.Lerp(from, to, timer / _duration);

                timer += Time.unscaledDeltaTime;

                yield return null;
            }

            _display.alpha = to;
        }

        #endregion
    }
}