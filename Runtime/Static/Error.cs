using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Contains custom exception types used throughout the project,
    /// and helper methods to log errors and warnings consistently.
    /// </summary>
    public static class Error
    {
        #region Classes

        public class FileNotFoundException : Exception
        {
            public FileNotFoundException(string path)
                : base($"The path '{path}' does not exist in your computer") { }
        }

        public class InstanceNotUniqueException : Exception
        {
            public InstanceNotUniqueException(Type instanceType)
                : base($"The instance of Type={instanceType.Name} should be unique.") { }
        }

        public class InvalidCategoryException : Exception
        {
            public InvalidCategoryException(string category)
                : base($"Category not recognized : Category={category}.") { }
        }

        public class InvalidEnumTypeException : Exception
        {
            public InvalidEnumTypeException(string enumType)
                : base($"Invalid enum type, require: '{enumType}'.") { }
        }

        public class InvalidGameStateException : Exception
        {
            public InvalidGameStateException(string type)
                : base($"The game state State={type} is not supported by the game.") { }
        }

        public class InvalidObjectofTypeException<T> : Exception where T : UnityEngine.Object
        {
            public InvalidObjectofTypeException(GameObject obj)
                : base($"The object {obj.name} is not of type : Type={typeof(T).Name}") { }
        }

        public class InvalidSettingException : Exception
        {
            public InvalidSettingException(string name, string category)
                : base($"Setting not recognized : Name={name}, Category={category}.") { }
        }

        public class InvalidTypesException : Exception
        {
            public InvalidTypesException(Type type, Type supposedType)
                : base($"The argument is of Type={type.Name} instead of : Type={supposedType.Name}") { }
        }

        public class MenuNotFoundException : Exception
        {
            public MenuNotFoundException(string menuName)
                : base($"The menu '{menuName}' does not exist in the current scene.") { }
        }

        public class PropertyNotFoundException : Exception
        {
            public PropertyNotFoundException(string propertyPath, string baseNameProperty)
                : base($"Property '{propertyPath}' not found in {baseNameProperty}.") { }
        }

        public class ProjectPropertyNotFoundException : Exception
        {
            public ProjectPropertyNotFoundException()
                : base("Project Properties not found in the Assets") { }
        }

        public class SceneNotFoundException : Exception
        {
            public SceneNotFoundException(string sceneName)
                : base($"The scene '{sceneName}' does not exist in the project's build settings.") { }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Logs the provided exception as an error in the Unity console with detailed information.
        /// </summary>
        /// <param name="e">The exception to log.</param>
        public static void Throw(Exception e) => Debug.LogError($"[ERROR] {e.GetType().Name} : {e.Message}\n{e.StackTrace}");

        /// <summary>
        /// Logs the provided exception as a warning in the Unity console with detailed information.
        /// </summary>
        /// <param name="e">The exception to log as a warning.</param>
        public static void Warn(Exception e) => Debug.LogWarning($"[WARNING] {e.GetType().Name} : {e.Message}\n{e.StackTrace}");

        #endregion
    }
}

