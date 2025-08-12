using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Base class for custom Unity Editors targeting objects of type <typeparamref name="T"/>.
    /// Provides common setup and utility methods for inspector customization.
    /// </summary>
    /// <typeparam name="T">The type of Unity Object this editor targets.</typeparam>
    public abstract partial class BasicEditor<T> : Editor where T : Object
    {
        #region Variables

        protected T _ref;

        #endregion

        #region Methods

        /// <summary>
        /// Draws the custom GUI for the inspector. Must be implemented by subclasses.
        /// </summary>
        protected abstract void OnGUI();

        /// <summary>
        /// Retrieves serialized properties from the target object. Can be overridden by subclasses.
        /// </summary>
        protected abstract void GetProperties();

        /// <summary>
        /// Draws the default inspector GUI provided by Unity's base Editor class.
        /// </summary>
        protected partial void BaseInspector();

        #endregion
    }
}