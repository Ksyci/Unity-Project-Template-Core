using System;
using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a dynamic enumeration based on a <see cref="string"/> array, allowing selection of a valid <see cref="string"/> value from predefined options.
    /// </summary>
    [Serializable]
    public abstract class DynamicEnum
    {
        #region Serialized

        [SerializeField]
        protected string _value;

        [SerializeField]
        protected string[] _enum;

        #endregion

        #region Properties

        public string Value { get => _value; set => _value = _enum.Contains(value) ? value : _enum[0]; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicEnum"/> class with a 
        /// list of possible <see cref="string"/> values.  
        /// The initial value is set to the first <see cref="string"/> in the list.
        /// </summary>
        /// <param name="types">The list of <see cref="string"/> values defining the enumeration options.</param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public DynamicEnum(params string[] types)
        {
            try
            {
                _enum = types;
                _value = _enum[0];
            }
            catch (Exception e) when (e is NullReferenceException or IndexOutOfRangeException)
            {
                Error.Throw(e);
            }
        }

        #endregion
    }
}
