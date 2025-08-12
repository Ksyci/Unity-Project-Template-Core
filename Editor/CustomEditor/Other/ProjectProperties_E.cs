using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTemplate
{
    #if UNITY_EDITOR

    [CustomEditor(typeof(ProjectProperties))]
    public class ProjectProperties_E : BasicEditor<ProjectProperties>
    {
        #region Constantes

        private const string GLOBAL_SECTION_NAME = "Global Properties";
        private const string SCENES_SECTION_NAME = "Scenes Properties";
        private const string SETTINGS_SECTION_NAME = "Settings Properties";
        private const string PAGES_SECTION_NAME = "Page";
        private const string WARNING_NO_SCENES = "No Scenes are present in the build setting of the game.";

        #endregion

        #region Variables

        private List<UnityAction> _menus;

        private SerializedProperty _backupsConfiguration;
        private SerializedProperty _gameStates;
        private SerializedProperty _languages;
        private SerializedProperty _pageIndex;
        private SerializedProperty _persistentObjects;
        private SerializedProperty _scenes;
        private SerializedProperty _sceneConfiguration;
        private SerializedProperty _settingSections;

        #endregion

        #region Editor Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _menus = new()
        {
            () => Edition.CreateSection(GLOBAL_SECTION_NAME, GlobalMenu, false),
            () => Edition.CreateSection(SCENES_SECTION_NAME, ScenesMenu, false),
            () => Edition.CreateSection(SETTINGS_SECTION_NAME, SettingsMenu, false),
        };

            _ref.UpdatePropertyTypeEnum();
        }

        protected override void OnGUI()
        {
            Edition.CreateSection(PAGES_SECTION_NAME + ' ' + (_pageIndex.intValue + 1), PagesManager, false);

            _menus[_pageIndex.intValue].Invoke();
        }

        protected override void GetProperties()
        {
            _backupsConfiguration = Get(nameof(_backupsConfiguration));
            _gameStates = Get(nameof(_gameStates));
            _languages = Get(nameof(_languages));
            _pageIndex = Get(nameof(_pageIndex));
            _persistentObjects = Get(nameof(_persistentObjects));
            _scenes = Get(nameof(_scenes));
            _sceneConfiguration = Get(nameof(_sceneConfiguration));
            _settingSections = Get(nameof(_settingSections));
        }

        #endregion

        #region Other Methods

        private void GlobalMenu()
        {
            string title = Format.Polish(nameof(_ref.Languages));
            Edition.CreateProperty(_languages, title);

            title = Format.Polish(nameof(_ref.GameStates));
            Edition.CreateProperty(_gameStates, title);

            title = Format.Polish(nameof(_ref.PersistentObjects));
            Edition.CreateProperty(_persistentObjects, title);

            title = Format.Polish(nameof(_ref.BackupsConfiguration));
            Edition.CreateProperty(_backupsConfiguration, title);
        }

        private void ScenesMenu()
        {
            if (_ref.Scenes.Count > 0)
            {
                string title = Format.Polish(nameof(_ref.ScenesConfiguration));
                Edition.CreateProperty(_sceneConfiguration, title);

                title = Format.Polish(nameof(_ref.Scenes));
                void createScenes() => Edition.CreateList(_scenes);
                Edition.CreateSection(title, createScenes, false);
            }
            else
            {
                EditorGUILayout.HelpBox(WARNING_NO_SCENES, MessageType.Warning);
            }
        }

        private void SettingsMenu()
        {
            UpdateSettings();

            EditorGUI.BeginChangeCheck();

            string title = Format.Polish(nameof(_ref.SettingSections));
            Edition.CreateProperty(_settingSections, title);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                EditorUtility.SetDirty(_ref);

                SettingsManager.CreateSettings();
            }
        }

        private void PagesManager()
        {
            _gameStates.serializedObject.Update();

            EditorGUILayout.BeginHorizontal();

            GUI.enabled = _pageIndex.intValue > 0;
            if (GUILayout.Button("-"))
            {
                _pageIndex.intValue--;
            }

            GUI.enabled = _pageIndex.intValue < _menus.Count - 1;
            if (GUILayout.Button("+"))
            {
                _pageIndex.intValue++;
            }

            GUI.enabled = true;

            EditorGUILayout.EndHorizontal();
        }

        private void UpdateSettings()
        {
            for (int i = 0; i < _settingSections.arraySize; i++)
            {
                SerializedProperty settingSection = _settingSections.GetArrayElementAtIndex(i);

                settingSection.managedReferenceValue ??= new SettingSection();

                SettingSection section = settingSection.managedReferenceValue as SettingSection;

                SerializedProperty settingTemplates = settingSection.FindPropertyRelative(section.NameOfSettingTemplates);

                for (int j = 0; j < settingTemplates.arraySize; j++)
                {
                    SerializedProperty settingTemplate = settingTemplates.GetArrayElementAtIndex(j);

                    settingTemplate.managedReferenceValue ??= new SettingTemplate();

                    SettingTemplate template = settingTemplate.managedReferenceValue as SettingTemplate;

                    SerializedProperty property = settingTemplate.FindPropertyRelative(template.NameOfProperty);

                    if (template.HasChanged)
                    {
                        property.managedReferenceValue = Activator.CreateInstance(template.Cast);
                    }
                }
            }
        }

        #endregion
    }

    #endif
}