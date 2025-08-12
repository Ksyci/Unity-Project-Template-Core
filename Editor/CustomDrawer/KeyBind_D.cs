using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    [CustomPropertyDrawer(typeof(KeyBind))]
    public class KeyBind_D : BasicDrawer
    {
        #region Constantes

        private const string BUTTON_TEXT = "Waiting for key...";
        private const string KEY_PROP_NAME = "_key";

        #endregion

        #region Variables

        private static readonly Dictionary<string, bool> _waitingStates = new();

        protected override int DefaultLineCount => 1;

        #endregion

        #region Methods

        #endregion

        protected override void DrawCustom(Rect position, SerializedProperty property, GUIContent label)
        {
            Bind(property.propertyPath);

            Rect labelRect = new(_position.x, _position.y, EditorGUIUtility.labelWidth, _position.height);

            EditorGUI.LabelField(labelRect, label);

            bool isWaiting = _waitingStates[property.propertyPath];

            DrawBinder(property, isWaiting);
        }

        private void Bind(string path)
        {
            if (!_waitingStates.ContainsKey(path))
            {
                _waitingStates[path] = false;
            }
        }

        private void DrawBinder(SerializedProperty property, bool isWaiting)
        {
            SerializedProperty key = GetProperty(property, KEY_PROP_NAME);

            Rect buttonRect = new(_position.x + EditorGUIUtility.labelWidth, _position.y, _position.width - EditorGUIUtility.labelWidth, _position.height);

            if (isWaiting)
            {
                GUI.Button(buttonRect, BUTTON_TEXT);

                if (Event.current.type == EventType.KeyDown)
                {
                    key.intValue = (int)Event.current.keyCode;
                    _waitingStates[property.propertyPath] = false;

                    GUI.changed = true;
                    Event.current.Use();
                }
            }
            else
            {
                if (GUI.Button(buttonRect, '[' + key.enumDisplayNames[key.enumValueIndex] + ']'))
                {
                    _waitingStates[property.propertyPath] = true;
                }
            }
        }
    }
}