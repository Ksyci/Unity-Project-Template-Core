using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    #if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(TypeEnum<>), true)]
    public class TypeEnumDrawer : BasicDrawer
    {
        #region Constantes

        private const string ENUM_PROP_NAME = "_enum";
        private const string VALUE_PROP_NAME = "_value";

        #endregion

        #region Variables

        protected override int DefaultLineCount => 1;

        #endregion

        #region Methods

        protected override void DrawCustom(Rect position, SerializedProperty property, GUIContent label)
        {
            var @enum = GetProperty(property, ENUM_PROP_NAME);
            var value = GetProperty(property, VALUE_PROP_NAME);

            position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), label);

            List<string> options = new();

            for (int i = 0; i < @enum.arraySize; i++)
            {
                options.Add(@enum.GetArrayElementAtIndex(i).stringValue);
            }

            EditorGUI.BeginChangeCheck();

            int index = Mathf.Max(0, options.IndexOf(value.stringValue));
            index = EditorGUI.Popup(position, index, options.ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                value.stringValue = options[index];

                Save(property);
            }
        }

        #endregion
    }

    #endif
}