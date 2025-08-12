using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a backup with identifying information, including name, hash, scene, index, and creation date.
    /// </summary>
    [Serializable]
    public class Backup
    {
        #region Interface 

        /// <summary>
        /// Represents a serializable data entry stored in a <see cref="Backup"/>.
        /// Must be cloneable and have a unique identifying name.
        /// </summary>
        public interface IData : ICloneable { Type Type { get; } }

        #endregion

        #region Serialized

        [SerializeField]
        private int _hash;

        [SerializeField]
        private int _index;

        [SerializeField]
        private long _dateTime;

        [SerializeField]
        private string _name;

        [SerializeField]
        private string _scene;

        [SerializeField]
        private List<IData> _datas;

        #endregion

        #region Properties

        public int DataCount => _datas.Count;
        
        public int Hash => _hash;

        public int Index => _index;

        public string Name => _name;

        public string Path => Hash == 0 ? Name : Name + '(' + Hash + ')';

        public string Scene => _scene;

        public DateTime DateTime => new(_dateTime);

        public IData this[Type type] => _datas.FirstOrDefault(d => d.Type == type);

        public IReadOnlyList<IData> Datas => _datas;

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new backup instance with the given name, hash, and index. 
        /// Initializes the data list and sets the current scene and timestamp.
        /// </summary>
        /// <param name="name">The display name of the backup.</param>
        /// <param name="hash">A unique hash identifier for the backup.</param>
        /// <param name="index">The index position of the backup in storage.</param>
        public Backup(string name, int hash, int index)
        {
            _name = name;
            _hash = hash;
            _index = index;

            _datas = new();
            _dateTime = DateTime.Now.Ticks;
            _scene = ProjectProperties.Get().FirstScene.Name;
        }

        /// <summary>
        /// Creates a new backup instance from an existing backup, copying its scene and data list, 
        /// but using a new hash and index.
        /// </summary>
        /// <param name="backup">The existing backup to duplicate.</param>
        /// <param name="hash">The new unique hash identifier.</param>
        /// <param name="index">The new index position.</param>
        public Backup(Backup backup, int hash, int index) : this(backup.Name, hash, index) 
            => _scene = backup.Scene;

        /// <summary>
        /// Adds a new datas entry of the specified type if it is not null and not already present.
        /// </summary>
        /// <typeparam name="T">The type of data to add.</typeparam>
        /// <param name="isOverrided">True: the datas are overrided; False: They are not.</param>
        /// <param name="newDatas">The array of data object to store in the backup.</param>
        public void AddData<T>(bool isOverrided, T newData) where T : IData
        {
            if (newData != null)
            {
                IData data = this[newData.Type];

                if (data != null)
                {
                    if (isOverrided)
                    {
                        int index = _datas.IndexOf(data);
                        _datas[index] = newData;
                    }
                }
                else
                {
                    _datas.Add(newData);
                }
            }
        }

        #endregion
    }
}