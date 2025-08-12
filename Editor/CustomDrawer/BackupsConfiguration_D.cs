using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    #if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(BackupsConfiguration))]
    public class BackupsConfiguration_D : BasicDrawer
    {
        #region Constantes

        private const float MAX_DELAY = 900.0f;
        private const float MIN_DELAY = 60.0f;

        private const int MAX_BACKUP = 50;

        private const string AUTO_SAVE_DELAY_PROP_NAME = "_autoSaveDelay";
        private const string MAX_BACKUP_PROP_NAME = "_maxBackup";

        #endregion

        #region Variables

        protected override int DefaultLineCount => 2;

        #endregion

        #region Methods

        protected override void DrawCustom(Rect position, SerializedProperty property, GUIContent label)
        {
            var autoSaveDelay = GetProperty(property, AUTO_SAVE_DELAY_PROP_NAME);
            var maxBackup = GetProperty(property, MAX_BACKUP_PROP_NAME);

            string title = Format.Polish(AUTO_SAVE_DELAY_PROP_NAME);
            autoSaveDelay.floatValue = EditorGUI.Slider(_position, title, autoSaveDelay.floatValue, MIN_DELAY, MAX_DELAY);

            NextLine();

            title = Format.Polish(MAX_BACKUP_PROP_NAME);
            maxBackup.intValue = EditorGUI.IntSlider(_position, title, maxBackup.intValue, 1, MAX_BACKUP);
        }

        #endregion
    }

    #endif
}