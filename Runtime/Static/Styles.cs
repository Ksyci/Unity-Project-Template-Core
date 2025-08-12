using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Provides predefined <see cref="GUIStyle"/> instances for consistent styling of editor UI elements.
    /// </summary>
    public static class Styles
    {
        #region Constantes

        private const int TITLE_FONT_SIZE = 20;
        private const int SUBTITLE_FONT_SIZE = 14;

        #endregion

        #region Properties

        public static GUIStyle BoldLabel { get; private set; } = new(EditorStyles.label)
        {
            fontStyle = FontStyle.Bold
        };

        public static GUIStyle TitleStyle { get; private set; } = new()
        {
            fontSize = TITLE_FONT_SIZE,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.gray }
        };

        public static GUIStyle SubtitleStyle { get; private set; } = new()
        {
            fontSize = SUBTITLE_FONT_SIZE,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.gray }
        };

        public static GUIStyle BackgroundStyle { get; private set; } = new(GUI.skin.box)
        {
            padding = new RectOffset(10, 10, 10, 10),
            margin = new RectOffset(5, 5, 5, 5)
        };

        public static GUIStyle FrameStyle { get; private set; } = new(GUI.skin.textArea)
        {
            padding = new RectOffset(10, 10, 10, 10),
            margin = new RectOffset(5, 5, 5, 5)
        };

        #endregion
    }
}