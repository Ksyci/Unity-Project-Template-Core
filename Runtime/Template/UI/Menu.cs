using UnityEngine.Events;

namespace ProjectTemplate
{
    public abstract partial class Menu : UI
    {
        #region Overrided Methods

        public override void Display(bool isDislpayed)
        {
            base.Display(isDislpayed);

            UpdateDisplay();
        }

        #endregion

        #region Private Methods

        protected static partial void Return() => UIsManager.Instance.Remove(true);

        protected static partial void GoTo(string nextMenu) => UIsManager.Instance.Add(nextMenu, false);

        protected virtual partial void UpdateDisplay() { }

        #endregion
    }
}