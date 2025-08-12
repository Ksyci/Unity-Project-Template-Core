using System.Collections;

namespace ProjectTemplate
{
    /// <summary>
    /// Abstract base class for screen-type UI elements that can be played and stopped,
    /// typically used for timed sequences or animations.
    /// </summary>
    public abstract partial class Screen : UI
    {
        #region Properties

        protected bool IsPlayed { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Defines the sequence of operations to be performed when the screen is played.
        /// </summary>
        /// <returns>Enumerator controlling the execution over multiple frames.</returns>
        protected abstract IEnumerator Execute();

        /// <summary>
        /// Starts the screen execution by running the defined coroutine sequence.
        /// </summary>
        public partial void Play();

        /// <summary>
        /// Stops the current screen execution and resets the state.
        /// </summary>
        public partial void Stop();

        #endregion
    }
}