using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Generic singleton base class for Unity managers, providing a globally accessible instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the manager inheriting from this class.</typeparam>
    public abstract partial class Manager<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Properties

        public static T Instance { get; private set; }

        #endregion
    }
}