using System;
using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Provides utility methods for creating custom editor GUI sections, properties, lists, and foldouts with consistent styling and spacing.
    /// </summary>
    public static class Edition
    {
        #region Constantes

        public const int SPACING = 10;

        public const string NAME_PROP_NAME = "_name";

        #endregion

        #region Methods

        /// <summary>
        /// Creates a vertical section in the editor GUI with an optional title and styling depending on whether it's a subsection.
        /// </summary>
        /// <param name="title">Title of the section. Can be null or empty for no title.</param>
        /// <param name="sectionAction">Action to render the contents of the section.</param>
        /// <param name="isSubsection">If true, applies subsection styling; otherwise, main section styling.</param>
        public static void CreateSection(string title, Action sectionAction, bool isSubsection)
        {
            GUIStyle frameStyle = isSubsection ? Styles.FrameStyle : Styles.BackgroundStyle;
            GUIStyle labelStyle = isSubsection ? Styles.SubtitleStyle : Styles.TitleStyle;

            EditorGUILayout.BeginVertical(frameStyle);

            if (!string.IsNullOrEmpty(title))
            {
                EditorGUILayout.LabelField(title, labelStyle);

                GUILayout.Space(SPACING);
            }

            sectionAction();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Creates a property field in the editor GUI with an optional custom label.
        /// </summary>
        /// <param name="property">Serialized property to display.</param>
        /// <param name="name">Custom label for the property. If null, uses the default property label.</param>
        public static void CreateProperty(SerializedProperty property, string name)
        {
            CreateSection(name, Action, true);

            void Action()
            {
                try
                {
                    EditorGUI.indentLevel++;

                    if (string.IsNullOrEmpty(name))
                    {
                        EditorGUILayout.PropertyField(property, GUIContent.none, true);
                    }
                    else
                    {
                        GUIContent labelContent = new(name);

                        EditorGUILayout.PropertyField(property, labelContent, true);
                    }

                    EditorGUI.indentLevel--;
                }
                catch (NullReferenceException e)
                {
                    Error.Warn(e);
                }
            }
        }

        /// <summary>
        /// Creates property fields for each element in a serialized list property.
        /// </summary>
        /// <param name="list">Serialized list property to display.</param>
        public static void CreateList(SerializedProperty list)
        {
            for (int i = 0; i < list.arraySize; i++)
            {
                var property = list.GetArrayElementAtIndex(i);
                var name = property.FindPropertyRelative(NAME_PROP_NAME);

                CreateProperty(property, name?.stringValue);
            }
        }

        /// <summary>
        /// Creates a toggle foldout with a label and optional nested layout content.
        /// </summary>
        /// <param name="name">Label for the foldout toggle.</param>
        /// <param name="isOpen">Current state of the foldout (expanded or collapsed).</param>
        /// <param name="layout">Action to render nested content when the foldout is expanded.</param>
        /// <returns>Returns the updated foldout state after user interaction.</returns>
        public static bool CreateFoldout(string name, bool isOpen, Action layout)
        {
            isOpen = EditorGUILayout.Toggle(name, isOpen);

            if (isOpen)
            {
                EditorGUI.indentLevel++;

                layout();

                EditorGUI.indentLevel--;
            }

            return isOpen;
        }

        #endregion
    }
}