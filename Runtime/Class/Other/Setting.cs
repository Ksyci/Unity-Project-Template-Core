using Newtonsoft.Json;
using System.ComponentModel;
using UnityEngine.Events;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a configurable setting serializable in a Json format.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Setting
    {
        #region Variables

        private object _value;

        #endregion

        #region Properties

        [JsonProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        public object Value { get => _value; set => Set(value); }

        [JsonIgnore]
        public UnityEvent OnChange { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a <see cref="Setting"/> with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the <see cref="Setting"/>.</param>
        /// <param name="value">The initial value of the <see cref="Setting"/>.</param>
        [JsonConstructor]
        public Setting(string name, object value)
        {
            Name = name;
            _value = value;
            OnChange = new();
        }

        /// <summary>
        /// Initializes a new <see cref="Setting"/> with only the specified value.
        /// </summary>
        /// <param name="value">The initial value of the <see cref="Setting"/>.</param>
        public Setting(object value) : this(default, value) { }

        /// <summary>
        /// Adds a listener to the <see cref="OnChange"/> event and immediately invokes it.
        /// </summary>
        /// <param name="action">The action to bind and execute on value change.</param>
        public void BindAndExecute(UnityAction action)
        {
            OnChange?.AddListener(action);
            action();
        }

        /// <summary>
        /// Sets the <see cref="Setting"/>'s value and invokes the <see cref="OnChange"/> event.
        /// Also updates the settings manager if available.
        /// </summary>
        /// <param name="value">The new value to set.</param>
        private void Set(object value)
        {
            _value = value;

            OnChange?.Invoke();

            if (SettingsManager.Instance != null)
            {
                SettingsManager.Instance.UpdateSettings();
            }
        }

        #endregion
    }
}