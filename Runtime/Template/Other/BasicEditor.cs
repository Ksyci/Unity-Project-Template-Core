using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace ProjectTemplate
{
    public abstract partial class BasicEditor<T> : Editor where T : Object
    {
        #region Unity Methods

        protected virtual void OnEnable()
        {
            _ref = (T)target;

            GetProperties();
        }

        protected virtual void OnDisable()
        {
            if (_ref != null)
            {
                EditorUtility.SetDirty(_ref);
                AssetDatabase.SaveAssets();
            }
        }

        #endregion

        #region Overrided Methods

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            OnGUI();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Private Methods

        protected partial void BaseInspector() => base.OnInspectorGUI();

        protected SerializedProperty Get(string name) => serializedObject.FindProperty(name);

        #endregion
    }
}