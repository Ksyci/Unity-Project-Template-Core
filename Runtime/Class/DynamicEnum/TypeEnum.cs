using System;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a <see cref="DynamicEnum"/> of types derived from the generic type <typeparamref name="T"/>.
    /// It automatically gathers all non-abstract subclasses of <typeparamref name="T"/> available in the current AppDomain.
    /// </summary>
    /// <typeparam name="T">The base class type to find derived types from.</typeparam>
    [Serializable]
    public class TypeEnum<T> : DynamicEnum
    {
        #region Static

        private static readonly string[] _types = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a => !a.IsDynamic)
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T)))
            .Select(t => t.Name)
            .ToArray();

        #endregion

        #region Serialized

        [SerializeField]
        private bool _hasChanged;

        #endregion

        #region Properties

        public Type Type => General.GetType(_value);

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeEnum{T}"/> class,
        /// populating the enumerations with the names of all derived types of <typeparamref name="T"/>.
        /// </summary>
        public TypeEnum() : base(_types) { }

        /// <summary>
        /// Refreshes the list of available types to reflect any changes in loaded assemblies.
        /// </summary>
        public void Update() => _enum = _types;

        #endregion
    }
}