using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTemplate
{
    public partial class AudioManager : Manager<AudioManager>
    {
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _audioTracks = new();
            _jukebox = CreateAudioSource(nameof(_jukebox));

            InstantiateTrack(MUSIC_SETTING_NAME, _jukebox);
            InstantiateTrack(SOUNDS_SETTING_NAME);
            InstantiateTrack(VOICES_SETTING_NAME);
        }

        #endregion

        #region Public Methods

        public partial void LoadSoundtracks(GameScene scene)
        {
            StartCoroutine(Fade(true));

            if (scene == null || scene.Playlists.Soundtracks.Count == 0)
            {
                return;
            }

            scene.Playlists.GenerateOrder();

            if (_soundtrackRoutine != null)
            {
                StopCoroutine(_soundtrackRoutine);
                _soundtrackRoutine = null;
            }

            _soundtrackRoutine = StartCoroutine(ManageSoundtrack(scene.Playlists));
        }

        public partial void AddSource(AudioSource source, string trackName)
        {
            try
            {
                _audioTracks[trackName].Add(source);
            }
            catch (KeyNotFoundException e)
            {
                Error.Warn(e);
            }
        }

        public partial void RemoveSource(AudioSource source, string trackName)
        {
            try
            {
                _audioTracks[trackName].Remove(source);
            }
            catch (KeyNotFoundException e)
            {
                Error.Warn(e);
            }
        }

        #endregion

        #region Private Methods

        private partial AudioSource CreateAudioSource(string name)
        {
            GameObject go = new(Format.Polish(name));
            AudioSource source = go.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            DontDestroyOnLoad(go);
            return source;
        }

        private partial void InstantiateTrack(string settingName, params AudioSource[] sources)
        {
            Setting setting = SettingsManager.Instance[settingName];
            AudioTrack track = new(setting, sources);
            _audioTracks.Add(settingName, track);

            setting?.BindAndExecute(() => ChangeVolume(setting));
        }

        private partial IEnumerator ManageSoundtrack(Playlist playlist)
        {
            yield return new WaitUntil(() => _isMuted);

            if (_jukebox.isPlaying)
            {
                _jukebox.Stop();
            }

            StartCoroutine(Fade(false));

            yield return new WaitUntil(() => !_isMuted);

            while (true)
            {
                _jukebox.clip = playlist.GetNext();

                if (_jukebox.clip == null)
                {
                    yield break;
                }

                _jukebox.Play();

                yield return new WaitUntil(() => !_jukebox.isPlaying);
            }
        }

        private partial IEnumerator Fade(bool isfading)
        {
            if (_isMuted == isfading)
            {
                yield break;
            }

            float trackvolume = _audioTracks[MUSIC_SETTING_NAME].Volume;

            float fromFactor = isfading ? 1.0f : 0.0f;
            float toFactor = isfading ? 0.0f : 1.0f;

            float from = fromFactor * trackvolume;
            float to = toFactor * trackvolume;

            float elapsedTime = 0.0f;

            while (elapsedTime < MUTE_DELAY)
            {
                elapsedTime += Time.deltaTime;

                _jukebox.volume = Mathf.Lerp(from, to, elapsedTime / MUTE_DELAY);

                yield return null;
            }

            _jukebox.volume = to;

            _isMuted = isfading;
        }

        private partial void ChangeVolume(Setting setting)
        {
            try
            {
                if (!_isMuted)
                {
                    float volume = Convert.ToSingle(setting.Value);
                    volume = Mathf.InverseLerp(0.0f, MAX_VOLUME, volume);
                    _audioTracks[setting.Name].SetVolume(volume);
                }
            }
            catch (NullReferenceException e)
            {
                Error.Warn(e);
            }
        }

        #endregion
    }
}