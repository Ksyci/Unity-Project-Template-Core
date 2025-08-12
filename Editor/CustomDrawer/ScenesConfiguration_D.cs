using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    [CustomPropertyDrawer(typeof(ScenesConfiguration))]
    public class ScenesConfiguration_D : BasicDrawer
    {
        #region Constantes

        private const float MIN_DELAY = 1.0f;
        private const float MAX_DELAY = 5.0f;

        private const int EXTRA_LINE_COUNT = 3;

        private const string FIRST_SCENE_INDEX_PROP_NAME = "_firstSceneIndex";
        private const string LOADING_DELAY_PROP_NAME = "_loadingDelay";
        private const string LOADING_SCENE_INDEX_PROP_NAME = "_loadingSceneIndex";
        private const string HAS_LOADING_SCREEN_PROP_NAME = "_hasLoadingScreen";

        #endregion

        #region Variables

        protected override int DefaultLineCount => 3;

        #endregion

        #region Methods

        protected override void DrawCustom(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawFirstSceneIndex(property);

            NextLine();

            if (DrawHasLoadingScreen(property))
            {
                EditorGUI.indentLevel++;

                NextLine();

                DrawLoadingSceneIndex(property);

                NextLine();

                DrawLoadingDelay(property);

                EditorGUI.indentLevel--;
            }
        }

        protected override float GetHeight(SerializedProperty property, GUIContent label)
        {
            int lines = DefaultLineCount;

            var hasLoadingScreen = GetProperty(property, HAS_LOADING_SCREEN_PROP_NAME);

            if (hasLoadingScreen.boolValue)
            {
                lines += EXTRA_LINE_COUNT;
            }

            return lines * (LINE_HEIGHT + SPACING);
        }

        private void DrawFirstSceneIndex(SerializedProperty property)
        {
            GameScene[] scenes = ProjectProperties.Get().Scenes.ToArray();

            var firstSceneIndex = GetProperty(property, FIRST_SCENE_INDEX_PROP_NAME);

            string title = Format.Polish(nameof(firstSceneIndex));
            EditorGUI.LabelField(_position, title, Styles.BoldLabel);

            NextLine();

            GameScene scene = General.FindAt(scenes, firstSceneIndex.intValue);
            firstSceneIndex.intValue = EditorGUI.IntSlider(_position, scene?.Name, firstSceneIndex.intValue, 0, scenes.Length - 1);
        }

        private bool DrawHasLoadingScreen(SerializedProperty property)
        {
            var hasLoadingScreen = GetProperty(property, HAS_LOADING_SCREEN_PROP_NAME);

            string title = Format.Polish(nameof(hasLoadingScreen));
            hasLoadingScreen.boolValue = EditorGUI.ToggleLeft(_position, title, hasLoadingScreen.boolValue);

            return hasLoadingScreen.boolValue;
        }

        private void DrawLoadingSceneIndex(SerializedProperty property)
        {
            GameScene[] scenes = ProjectProperties.Get().Scenes.ToArray();

            var loadingSceneIndex = GetProperty(property, LOADING_SCENE_INDEX_PROP_NAME);

            string title = Format.Polish(nameof(loadingSceneIndex));
            EditorGUI.LabelField(_position, title, Styles.BoldLabel);

            NextLine();

            GameScene scene = General.FindAt(scenes, loadingSceneIndex.intValue);
            loadingSceneIndex.intValue = EditorGUI.IntSlider(_position, scene?.Name, loadingSceneIndex.intValue, 0, scenes.Length - 1);
        }

        private void DrawLoadingDelay(SerializedProperty property)
        {
            var loadingDelay = GetProperty(property, LOADING_DELAY_PROP_NAME);

            string title = Format.Polish(nameof(loadingDelay));
            loadingDelay.floatValue = EditorGUI.Slider(_position, title, loadingDelay.floatValue, MIN_DELAY, MAX_DELAY);
        }

        #endregion
    }
}