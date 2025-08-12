using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    #if UNITY_EDITOR

    /// <summary>
    /// Base class for custom property drawers providing common drawing utilities and layout management.
    /// </summary>
    public abstract partial class BasicDrawer : PropertyDrawer
    {
        #region Constantes

        protected float LINE_HEIGHT = EditorGUIUtility.singleLineHeight;
        protected float SPACING = EditorGUIUtility.standardVerticalSpacing;

        #endregion

        #region Variables

        protected Rect _position;

        #endregion

        #region Properties

        protected abstract int DefaultLineCount { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the custom GUI elements for the property inside the specified position.
        /// </summary>
        /// <param name="position">The rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The serialized property to make the custom GUI for.</param>
        /// <param name="label">The label of the property.</param>
        protected abstract void DrawCustom(Rect position, SerializedProperty property, GUIContent label);

        /// <summary>
        /// Calculates the height required to display the property, including all custom lines and spacing.
        /// </summary>
        /// <param name="property">The serialized property being drawn.</param>
        /// <param name="label">The label of the property.</param>
        /// <returns>The total height in pixels needed to draw the property.</returns>
        protected virtual partial float GetHeight(SerializedProperty property, GUIContent label);

        /// <summary>
        /// Retrieves a relative serialized property by name, from the given serialized property.
        /// </summary>
        /// <param name="property">The parent serialized property.</param>
        /// <param name="relativePath">The relative path to the target property.</param>
        /// <returns>The found serialized property or throws if not found.</returns>
        protected partial SerializedProperty GetProperty(SerializedProperty property, string relativePath);

        /// <summary>
        /// Advances the internal drawing position to the next line, accounting for line height and spacing.
        /// </summary>
        protected partial void NextLine();

        /// <summary>
        /// Saves or applies changes made to the given serialized property.
        /// </summary>
        /// <param name="property">The serialized property to save.</param>
        protected partial void Save(SerializedProperty property);

        #endregion
    }

    #endif
}