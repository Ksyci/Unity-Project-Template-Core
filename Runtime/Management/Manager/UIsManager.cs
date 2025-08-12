using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    public partial class UIsManager : Manager<UIsManager>
    {
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _uis = new();

            _activeUis = new();
        }

        #endregion

        #region Public Methods

        public partial void LoadUIs(GameScene scene)
        {
            _uis.Clear();
            _activeUis.Clear();

            CreateFolder<Screen>(scene, SCREENS_FOLDER_NAME);
            CreateFolder<Menu>(scene, MENUS_FOLDER_NAME);
        }

        public partial void ShowAll(bool isDisplayed)
        {
            foreach (var ui in _activeUis)
            {
                if (!isDisplayed || ui.Item2)
                {
                    ui.Item1.Display(isDisplayed);
                }
            }
        }

        public partial void Add(string menuName, bool isLastDisplayed)
        {
            try
            {
                if (!isLastDisplayed && _activeUis.Count != 0)
                {
                    (UI, bool) ui = _activeUis.Pop();

                    ui.Item1.Display(false);
                    ui.Item2 = false;

                    _activeUis.Push(ui);
                }

                if (menuName != null)
                {
                    _activeUis.Push((_uis[menuName], true));
                    _activeUis.Peek().Item1.Display(true);
                }
            }
            catch (KeyNotFoundException)
            {
                try
                {
                    throw new Error.MenuNotFoundException(menuName);
                }
                catch (Exception e)
                {
                    Error.Warn(e);
                }
            }
        }

        public partial void Remove(bool isNextDisplayed)
        {
            _activeUis.Pop().Item1.Display(false);

            if (isNextDisplayed && _activeUis.Count != 0)
            {
                (UI, bool) ui = _activeUis.Pop();

                ui.Item1.Display(true);
                ui.Item2 = false;

                _activeUis.Push(ui);
            }
        }

        public partial void Replace(string menuName)
        {
            for (int i = 0; i < _activeUis.Count; i++)
            {
                _activeUis.Pop().Item1.Display(false);
            }

            Add(menuName, false);
        }

        #endregion

        #region Private Methods

        private partial void CreateFolder<T>(GameScene scene, string name) where T : UI
        {
            if (scene == null)
            {
                return;
            }

            Dictionary<string, T> uis = scene.UIs.Where(ui => ui is T).Cast<T>().ToDictionary(ui => ui.GetType().Name);

            if (uis.Count() == 0)
            {
                return;
            }

            GameObject folder = new(name);

            foreach (var pair in uis)
            {
                GameObject obj = Instantiate(pair.Value.gameObject, folder.transform);

                T ui = obj.GetComponent<T>();

                obj.name = Format.Polish(pair.Key);

                _uis[pair.Key] = ui;

                if (pair.Value == scene.DefaultUi)
                {
                    Add(pair.Key, true);
                }
            }
        }

        #endregion
    }
}