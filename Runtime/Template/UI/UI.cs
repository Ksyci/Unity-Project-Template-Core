using System;
using UnityEngine;

namespace ProjectTemplate
{
    public abstract partial class UI : MonoBehaviour
    {
        #region Unity Methods

        protected virtual void Awake()
        {
            if (!TryGetComponent(out _display))
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

            StartCoroutine(new Appear(_display).Play(false));
        }

        public virtual partial void Display(bool isDislpayed)
        {
            if (_display.alpha != (isDislpayed ? 1.0f : 0.0f))
            {
                StartCoroutine(Transition.Play(isDislpayed));
            }
        }

        #endregion
    }
}