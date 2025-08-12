using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Manager responsible for handling audio tracks, playback, and volume control.
    /// </summary>
    public partial class AudioManager : Manager<AudioManager>
    {
        #region Constantes

        public const string MUSIC_SETTING_NAME = "Music";
        public const string SOUNDS_SETTING_NAME = "Sounds";
        public const string VOICES_SETTING_NAME = "Voices";

        private const float MUTE_DELAY = 0.5f;
        private const float MAX_VOLUME = 100.0f;

        #endregion

        #region Variables

        private Dictionary<string, AudioTrack> _audioTracks;

        private AudioSource _jukebox;

        private bool _isMuted;

        private Coroutine _soundtrackRoutine;

        #endregion

        #region Properties

        public AudioTrack this[string name] => General.FindWith(_audioTracks, name);

        #endregion

        #region Methods

        /// <summary>
        /// Adds an AudioSource to a specified audio track by name.
        /// </summary>
        /// <param name="source">The AudioSource to add.</param>
        /// <param name="trackName">The name of the <see cref="AudioTrack"/>.</param>
        public partial void AddSource(AudioSource source, string trackName);

        /// <summary>
        /// Changes the volume of an <see cref="AudioTrack"/> based on the given setting.
        /// </summary>
        /// <param name="setting">The setting that contains the new volume value.</param>
        private partial void ChangeVolume(Setting setting);

        /// <summary>
        /// Helper to create a persistent <see cref="AudioSource"/> <see cref="GameObject"/>.
        /// </summary>
        /// <param name="name">Name for the new GameObject.</param>
        /// <returns>The created <see cref="AudioSource"/> component.</returns>
        private partial AudioSource CreateAudioSource(string name);

        /// <summary>
        /// Coroutine that fades the jukebox audio in or out over time.
        /// </summary>
        /// <param name="isfading">True to fade out, false to fade in.</param>
        /// <returns>An IEnumerator for coroutine handling.</returns>
        private partial IEnumerator Fade(bool isfading);

        /// <summary>
        /// Instantiates an <see cref="AudioTrack"/> and binds volume changes to a <see cref="Setting"/>.
        /// </summary>
        /// <param name="settingName">The name of associated <see cref="Setting"/>.</param>
        /// <param name="sources">The multiple <see cref="AudioSource"/> to add to the <see cref="AudioTrack"/>.</param>
        private partial void InstantiateTrack(string settingName, params AudioSource[] sources);

        /// <summary>
        /// Loads and starts managing soundtracks from the given scene's playlist.
        /// </summary>
        /// <param name="scene">The scene containing the playlist to load.</param>
        public partial void LoadSoundtracks(GameScene scene);

        /// <summary>
        /// Coroutine that manages playback of the given playlist.
        /// </summary>
        /// <param name="playlist">The playlist to manage and play.</param>
        /// <returns>An IEnumerator for coroutine handling.</returns>
        private partial IEnumerator ManageSoundtrack(Playlist playlist);

        /// <summary>
        /// Removes an AudioSource from a specified audio track by name.
        /// </summary>
        /// <param name="source">The AudioSource to remove.</param>
        /// <param name="trackName">The name of the audio track.</param>
        public partial void RemoveSource(AudioSource source, string trackName);

        #endregion
    }
}