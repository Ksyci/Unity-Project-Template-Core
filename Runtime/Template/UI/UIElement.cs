using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectTemplate
{
    public abstract partial class UIElement : MonoBehaviour
    {
        #region Unity Methods

        protected virtual void Awake()
        {
            if (!TryGetComponent(out _transform))
            {
                try
                {
                    throw new Error.InvalidObjectofTypeException<UI>(gameObject);
                }
                catch (Exception e)
                {
                    Error.Throw(e);
                }
            }
        }

        #endregion
    }
}