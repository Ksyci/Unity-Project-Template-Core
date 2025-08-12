using System.Linq;
using UnityEngine;

namespace ProjectTemplate
{
    public abstract partial class Controller : MonoBehaviour
    {
        #region Unity Methods

        private void Awake()  
        {
            _eventsMapping = _events
                .Where(keyEvent => keyEvent != null && !string.IsNullOrEmpty(keyEvent.Name))
                .ToDictionary(keyEvent => keyEvent.Name, keyEvent => keyEvent);

            _oneTimeEvents = _events?.Where(e => e.IsOneTimeEvent).ToArray();
            _persistentEvents = _events?.Where(e => !e.IsOneTimeEvent).ToArray();
        }

        protected virtual void Update()
        {
            if (Input.anyKeyDown)
            {
                Execute(true);
            }
            if (Input.anyKey)
            {
                Execute(false);
            }
        }

        #endregion

        #region Private Methods

        private partial void Execute(bool isOnTimeEvents)
        {
            KeyEvent[] events = isOnTimeEvents ? _oneTimeEvents : _persistentEvents;

            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (isOnTimeEvents ? Input.GetKeyDown(keyCode) : Input.GetKey(keyCode))
                {
                    foreach (KeyEvent keyEvent in events)
                    {
                        if (keyEvent.Key == keyCode)
                        {
                            keyEvent.OnKey?.Invoke();
                        }
                    }
                }
            }
        }

        #endregion
    }
}