using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTemplate
{
    /// <summary>
    /// Represents a backup with identifying information, including name, hash, scene, index, and creation date.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Backup
    {
        #region Interface 

        /// <summary>
        /// Represents a serializable data entry stored in a <see cref="Backup"/>.
        /// Must be cloneable and have a unique identifying name.
        /// </summary>
        public interface IData : ICloneable { Type Type { get; } }

        #endregion

        #region Properties

        [JsonProperty]
        public int Hash { get; private set; }

        [JsonProperty]
        public int Index { get; private set; }

        [JsonProperty]
        public long Time { get; private set; }

        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public string Scene { get; private set; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        private List<IData> Datas { get; set; }

        [JsonIgnore]
        public string Path => Hash == 0 ? Name : Name + '(' + Hash + ')';

        [JsonIgnore]
        public IReadOnlyList<IData> BackupDatas => Datas;

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new backup instance with the given name, hash, and index. 
        /// Initializes the data list and sets the current scene and timestamp.
        /// </summary>
        /// <param name="name">The display name of the backup.</param>
        /// <param name="hash">A unique hash identifier for the backup.</param>
        /// <param name="index">The index position of the backup in storage.</param>
        [JsonConstructor]
        public Backup(string name, int hash, int index)
        {
            Name = name;
            Hash = hash;
            Index = index;

            Datas = new();
            Time = DateTime.Now.Ticks;
            Scene = ProjectProperties.Get().FirstScene.Name;
        }

        /// <summary>
        /// Creates a new backup instance from an existing backup, copying its scene and data list, 
        /// but using a new hash and index.
        /// </summary>
        /// <param name="backup">The existing backup to duplicate.</param>
        /// <param name="hash">The new unique hash identifier.</param>
        /// <param name="index">The new index position.</param>
        public Backup(Backup backup, int hash, int index) : this(backup.Name, hash, index) 
            => Scene = backup.Scene;

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
                IData data = Datas.FirstOrDefault(d => d.Type == newData.Type);

                if (data != null)
                {
                    if (isOverrided)
                    {
                        int index = Datas.IndexOf(data);
                        Datas[index] = newData;
                    }
                }
                else
                {
                    Datas.Add(newData);
                }
            }
        }

        #endregion
    }
}