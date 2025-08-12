using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a playlist of <see cref="AudioClip"/> that can be played in sequence or randomized order.
    /// </summary>
    [Serializable]
    public class Playlist
    {
        #region Serialized

        [SerializeField]
        private bool _isRandomised;

        [SerializeField]
        private AudioClip[] _soundtracks;

        #endregion

        #region Variables

        private int _index;

        private int[] _order;

        #endregion

        #region Properties

        public bool IsRandomised => _isRandomised;

        public IReadOnlyList<AudioClip> Soundtracks => _soundtracks.ToList();

        #endregion

        #region Methods

        /// <summary>
        /// Generates the playback order for the <see cref="Playlist"/>, shuffling if randomization is enabled.
        /// </summary>
        public void GenerateOrder()
        {
            _index = -1;

            if (_soundtracks == null || _soundtracks.Length == 0)
            {
                _order = Array.Empty<int>();
                return;
            }

            _order = Enumerable.Range(0, _soundtracks.Length).ToArray();

            if (_isRandomised)
            {
                General.Shuffle(_order);
            }
        }

        /// <summary>
        /// Gets the next <see cref="AudioClip"/> in the <see cref="Playlist"/> based on the current order.
        /// </summary>
        /// <returns>The next <see cref="AudioClip"/>, or <c>null</c> if the <see cref="Playlist"/> is empty.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public AudioClip GetNext()
        {
            try
            {
                _index++;
                _index %= _soundtracks.Length;
                return _soundtracks[_order[_index]];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        #endregion
    }
}