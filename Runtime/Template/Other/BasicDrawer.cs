using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectTemplate
{
    public abstract partial class BasicDrawer : PropertyDrawer
    {
        #region Overrided Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            _position = new(position.x, position.y, position.width, LINE_HEIGHT);

            DrawCustom(position, property, label);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => GetHeight(property, label);

        #endregion

        #region Private Methods

        protected virtual partial float GetHeight(SerializedProperty property, GUIContent label)
            => DefaultLineCount * (LINE_HEIGHT + SPACING);

        protected partial SerializedProperty GetProperty(SerializedProperty property, string relativePath)
        {
            try
            {
                if (property == null)
                {
                    throw new ArgumentNullException(property.name);
                }

                var prop = property.FindPropertyRelative(relativePath);

                return prop ?? throw new Error.PropertyNotFoundException(relativePath, nameof(property));
            }
            catch (Exception e)
            {
                Error.Warn(e);

                return null;
            }
        }

        protected partial void NextLine()
        {
            float y = _position.y;
            y += LINE_HEIGHT + SPACING;
            _position = new Rect(_position.x, y, _position.width, LINE_HEIGHT);
        }

        protected partial void Save(SerializedProperty property)
        {
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        #endregion
    }
}