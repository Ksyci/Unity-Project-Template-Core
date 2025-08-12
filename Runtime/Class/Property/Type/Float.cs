using System;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a <see cref="float"/> <see cref="Property"/> with a default value, integer flag, and minimum/maximum range.
    /// </summary>
    [Serializable]
    public class Float : Property
    {
        #region Serialized

        [SerializeField]
        private Vector2 _minAndMax;

        [SerializeField]
        private bool _isDefaultAtMax;

        [SerializeField]
        private bool _isInteger;

        #endregion

        #region Properties

        public bool IsInteger => _isInteger;

        public float DefaultFloat => _isDefaultAtMax ? Max : Min;

        public float Min => Mathf.Min(_minAndMax.x, _minAndMax.y);

        public float Max => Mathf.Max(_minAndMax.x, _minAndMax.y);

        public override object DefaultValue => DefaultFloat;

        #endregion
    }
}