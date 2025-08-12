using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Abstract base class for UI elements that require access to their RectTransform component.
    /// </summary>
    public abstract partial class UIElement : MonoBehaviour
    {
        #region Variables

        protected RectTransform _transform;

        #endregion
    }
}