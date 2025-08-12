using System;
using UnityEngine;

namespace ProjectTemplate
{
    public abstract partial class Manager<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Unity Methods

        protected virtual void Awake()
        {
            try
            {
                if (Instance != null)
                {
                    throw new Error.InstanceNotUniqueException(typeof(T));
                }

                Instance = this as T;
            }
            catch (Exception e)
            {
                Error.Throw(e);
            }
        }

        #endregion
    }
}