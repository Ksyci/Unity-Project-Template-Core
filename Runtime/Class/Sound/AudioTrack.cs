using System.Collections.Generic;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Manages a collection of <see cref="AudioSource"/> tied to a <see cref="Setting"/>, controlling their collective volume.
    /// </summary>
    public class AudioTrack
    {
        #region Variables

        private float _volume;

        private readonly List<AudioSource> _sources;

        #endregion

        #region Properties

        public float Volume { get => _volume; set => SetVolume(value); }

        public Setting Setting { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioTrack"/>.
        /// </summary>
        /// <param name="setting">The setting controlling this audio track.</param>
        /// <param name="sources">The initial audio sources to add to this track.</param>
        public AudioTrack(Setting setting, params AudioSource[] sources)
        {
            Setting = setting;

            _sources = new();
            _sources.AddRange(sources);
        }

        /// <summary>
        /// Adds an <see cref="AudioSource"/> to the track.
        /// </summary>
        /// <param name="source">The <see cref="AudioSource"/> to add.</param>
        public void Add(AudioSource source) => _sources.Add(source);

        /// <summary>
        /// Removes an <see cref="AudioSource"/> from the track if it exists.
        /// </summary>
        /// <param name="source">The <see cref="AudioSource"/> to remove.</param>
        public void Remove(AudioSource source)
        {
            if (_sources.Contains(source))
            {
                _sources.Remove(source);
            }
        }

        /// <summary>
        /// Sets the volume for all <see cref="AudioSource"/> in the track.
        /// </summary>
        /// <param name="volume">The volume value (clamped between 0 and 1).</param>
        public void SetVolume(float volume)
        {
            _volume = Mathf.Clamp01(volume);

            foreach (AudioSource source in _sources)
            {
                source.volume = _volume;
            }
        }

        #endregion
    }
}