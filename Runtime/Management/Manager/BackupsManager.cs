using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTemplate
{
    public partial class BackupsManager : Manager<BackupsManager>
    {
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            Directory.CreateDirectory(BackupsFolder);
            Directory.CreateDirectory(ScreenshotsFolder);

            OnBackupChanged = new();

            string[] paths = Directory.GetFiles(BackupsFolder, '*' + BACKUP_EXTENSION);

            _backups = new();

            foreach (string path in paths)
            {
                Backup backup = ReadBackup(path);
                _backups.Add(backup.Index, backup);
            }

            ActiveBackup = ProjectProperties.Get().BackupsConfiguration.GetActiveBackup(_backups.Values);
        }

        #endregion

        #region Static Methods

        public static partial Sprite LoadScreenshot(Backup backup)
        {
            try
            {
                string path = Path.Combine(ScreenshotsFolder, backup.Path + SCREENSHOT_EXTENSION);
                byte[] imageBytes = File.ReadAllBytes(path);

                Texture2D texture = new(default, default);

                texture.LoadImage(imageBytes);

                Sprite sprite = Sprite.Create(texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f), 100f);

                return sprite;
            }
            catch (Exception e) when (e is FileNotFoundException or NullReferenceException)
            {
                return null;
            }
        }

        private static partial void WriteBackup(Backup backup)
        {
            string path = Path.Combine(BackupsFolder, backup.Path + BACKUP_EXTENSION);

            FileStream file = File.Create(path);

            new BinaryFormatter().Serialize(file, backup);

            file.Close();
        }

        private static partial void EraseBackup(Backup backup)
        {
            string path = Path.Combine(BackupsFolder, backup.Path + BACKUP_EXTENSION);

            if (!File.Exists(path))
            {
                try
                {
                    throw new Error.FileNotFoundException(path);
                }
                catch (Exception e)
                {
                    Error.Warn(e);
                    return;
                }
            }

            File.Delete(path);
        }

        private static partial Backup ReadBackup(string path)
        {
            if (!File.Exists(path))
            {
                try
                {
                    throw new Error.FileNotFoundException(path);
                }
                catch (Exception e)
                {
                    Error.Warn(e);
                    return null;
                }
            }

            BinaryFormatter formatter = new();

            FileStream file = File.Open(path, FileMode.Open);

            Backup backup = (Backup)formatter.Deserialize(file);

            file.Close();

            return backup;
        }

        #endregion

        #region Public Methods

        public partial void MakeNew(string name)
        {
            int hash = FindHash(name);
            int index = FindIndex();

            Backup backup = new(name, hash, index);

            DatasManager.Instance.LoadDatas(backup);

            _backups.Add(backup.Index, backup);

            ChangeActiveBackup(backup);

            WriteBackup(backup);
        }

        public partial void Copy(int index)
        {
            int hash = FindHash(ActiveBackup.Name);

            Backup copiedBackup = new(ActiveBackup, hash, index);

            _backups.Add(copiedBackup.Index, copiedBackup);

            ChangeActiveBackup(copiedBackup);

            Save(copiedBackup);
        }

        public partial void Save(Backup backup)
        {
            try
            {
                WriteBackup(backup);
                CaptureScreenshot(backup);
                DatasManager.Instance.SaveDatas(backup);
            }
            catch (NullReferenceException e)
            {
                Error.Warn(e);
            }
        }

        public partial void Load(Backup backup)
        {
            try
            {
                ChangeActiveBackup(backup);
                ScenesManager.Instance.LoadScene(backup.Scene);
                DatasManager.Instance.LoadDatas(backup);
            }
            catch (NullReferenceException e)
            {
                Error.Warn(e);
            }
        }

        public partial void Delete(Backup backup)
        {
            try
            {
                string path = Path.Combine(ScreenshotsFolder, backup.Path + SCREENSHOT_EXTENSION);

                EraseBackup(backup);

                _backups.Remove(backup.Index);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                if (backup == ActiveBackup)
                {
                    ChangeActiveBackup(null);
                }
            }
            catch (NullReferenceException e)
            {
                Error.Warn(e);
            }
        }

        public partial void StartAutosave()
        {
            IsAutosaveTimerPaused = false;

            _autosave ??= StartCoroutine(Autosave());

            IEnumerator Autosave()
            {
                float autosaveDelay = ProjectProperties.Get().BackupsConfiguration.AutoSaveDelay;

                _autosaveTimer = 0.0f;

                while (true)
                {
                    while (_autosaveTimer < autosaveDelay)
                    {
                        if (!IsAutosaveTimerPaused)
                        {
                            _autosaveTimer += Time.deltaTime;
                        }

                        yield return null;
                    }

                    yield return new WaitUntil(() => !ScenesManager.Instance.IsLoading);

                    Debug.Log(AUTOSAVE_WARNING);

                    SaveActiveBackup();

                    _autosaveTimer = 0.0f;
                }
            }
        }

        public partial void StopAutosave()
        {
            try
            {
                StopCoroutine(_autosave);
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        #endregion

        #region Private Methods

        private partial void ChangeActiveBackup(Backup backup) 
            => ProjectProperties.Get().BackupsConfiguration.SetActiveBackup(ActiveBackup = backup);

        private partial void SaveActiveBackup()
        {
            try
            {
                WriteBackup(ActiveBackup);
                DatasManager.Instance.SaveDatas(ActiveBackup);
            }
            catch (NullReferenceException e)
            {
                Error.Warn(e);
            }
        }

        private partial void CaptureScreenshot(Backup backup)
        {
            StartCoroutine(Routine());

            IEnumerator Routine()
            {
                string path = Path.Combine(ScreenshotsFolder, backup.Path + SCREENSHOT_EXTENSION);

                File.Delete(path);

                UIsManager.Instance.ShowAll(false);
                yield return new WaitForSeconds(UI.TRANSITION_DURATION);

                ScreenCapture.CaptureScreenshot(path);
                yield return new WaitForSeconds(CAPTURE_DURATION);

                OnBackupChanged?.Invoke();
                yield return new WaitForEndOfFrame();

                UIsManager.Instance.ShowAll(true);
                yield return new WaitForSeconds(UI.TRANSITION_DURATION);

            }
        }

        private partial int FindHash(string name)
        {
            int hash = 0;

            HashSet<int> usedHashes = new();

            foreach (var backup in _backups)
            {
                if (backup.Value.Name == name)
                {
                    usedHashes.Add(backup.Value.Hash);
                }
            }

            while (usedHashes.Contains(hash))
            {
                hash++;
            }

            return hash;
        }

        private partial int FindIndex()
        {
            int index = 0;

            while (_backups.Any(b => b.Key == index))
            {
                index++;
            }

            return index;
        }

        #endregion
    }
}