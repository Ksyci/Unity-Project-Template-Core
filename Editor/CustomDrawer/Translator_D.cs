using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    [CustomPropertyDrawer(typeof(Translator))]
    public class Translator_D : BasicDrawer
    {
        #region Constantes

        private const string LANGUAGE_PROP_NAME = "_language";
        private const string TRANSLATIONS_PROP_NAME = "_translations";
        private const string VALUE_PROP_NAME = "_value";

        #endregion

        #region Variables

        private static readonly Dictionary<string, bool> _foldoutStates = new();

        protected override int DefaultLineCount => 1;

        #endregion

        #region Methods

        protected override void DrawCustom(Rect position, SerializedProperty property, GUIContent label)
        {
            Bind(property);

            ManageTranslations(property);

            DisplayName(property, label);

            if (_foldoutStates[property.propertyPath])
            {
                EditorGUI.indentLevel++;

                DisplayTranslations(property);

                EditorGUI.indentLevel--;
            }
        }

        protected override float GetHeight(SerializedProperty property, GUIContent label)
        {
            bool isPresent = _foldoutStates.ContainsKey(property.propertyPath);
            bool isOpen = isPresent && _foldoutStates[property.propertyPath];

            int lineCount = DefaultLineCount;

            if (isOpen)
            {
                lineCount += ProjectProperties.Get().Languages.Count;
            }

            return lineCount * (LINE_HEIGHT + SPACING);
        }

        private void Bind(SerializedProperty property)
        {
            if (!_foldoutStates.ContainsKey(property.propertyPath))
            {
                _foldoutStates[property.propertyPath] = true;
            }
        }

        private void ManageTranslations(SerializedProperty property)
        {
            var translations = GetProperty(property, TRANSLATIONS_PROP_NAME);

            IReadOnlyList<string> languages = ProjectProperties.Get().Languages;

            foreach (string language in languages)
            {
                if (!ContainsLanguage(translations, language))
                {
                    int newIndex = translations.arraySize;
                    translations.InsertArrayElementAtIndex(newIndex);
                    var newTranslation = translations.GetArrayElementAtIndex(newIndex);

                    var newlanguage = GetProperty(newTranslation, LANGUAGE_PROP_NAME);

                    newlanguage.stringValue = language;

                }
            }
        }

        private void DisplayName(SerializedProperty property, GUIContent label)
        {
            bool foldoutState = _foldoutStates[property.propertyPath];

            _foldoutStates[property.propertyPath] = EditorGUI.Foldout(_position, foldoutState, label);

            NextLine();
        }

        private void DisplayTranslations(SerializedProperty property)
        {
            var translations = GetProperty(property, TRANSLATIONS_PROP_NAME);

            IReadOnlyList<string> languages = ProjectProperties.Get().Languages;

            for (int i = 0; i < translations.arraySize; i++)
            {
                var translation = translations.GetArrayElementAtIndex(i);
                var language = GetProperty(translation, LANGUAGE_PROP_NAME);
                var value = GetProperty(translation, VALUE_PROP_NAME);

                if (languages.Contains(language.stringValue))
                {
                    value.stringValue = EditorGUI.TextField(_position, language.stringValue, value.stringValue);

                    NextLine();
                }
                else
                {
                    translations.DeleteArrayElementAtIndex(i);
                }
            }
        }

        private bool ContainsLanguage(SerializedProperty translations, string targetLanguage)
        {
            for (int i = 0; i < translations.arraySize; i++)
            {
                var translation = translations.GetArrayElementAtIndex(i);
                var language = GetProperty(translation, LANGUAGE_PROP_NAME);

                if (language.stringValue == targetLanguage)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}