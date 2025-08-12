using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    #if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : BasicDrawer
    {
        #region Variables

        protected override int DefaultLineCount => 1;

        #endregion

        #region Methods

        protected override void DrawCustom(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }

        protected override float GetHeight(SerializedProperty property, GUIContent label)
            => DefaultLineCount * LINE_HEIGHT;

        #endregion
    }

    #endif
}