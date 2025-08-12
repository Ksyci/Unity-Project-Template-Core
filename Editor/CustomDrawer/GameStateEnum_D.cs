using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    [CustomPropertyDrawer(typeof(GameStateEnum))]
    public class GameStateEnum_D : BasicDrawer
    {
        #region Constantes

        private const string VALUE_PROP_NAME = "_value";

        #endregion

        #region Variables

        protected override int DefaultLineCount => 1;

        #endregion

        #region Methods

        protected override void DrawCustom(Rect position, SerializedProperty property, GUIContent label)
        {
            var value = GetProperty(property, VALUE_PROP_NAME);

            _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), label);

            List<string> options = ProjectProperties.Get().GameStates.Select(x => x.Name).ToList();

            EditorGUI.BeginChangeCheck();

            int index = Mathf.Max(0, options.IndexOf(value.stringValue));
            index = EditorGUI.Popup(_position, index, options.ToArray());

            if (EditorGUI.EndChangeCheck() || value.stringValue == null)
            {
                value.stringValue = options[index];

                Save(property);
            }
        }

        #endregion
    }
}