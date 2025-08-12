using System;
using System.Collections.Generic;

namespace ProjectTemplate
{
    /// <summary>
    /// Manages the mapping and retrieval of data objects used for <see cref="Backup"/>.
    /// </summary>
    public partial class DatasManager : Manager<DatasManager>
    {
        #region Variables

        private Dictionary<Type, Backup.IData> _datasMapping;

        #endregion

        #region Methods

        /// <summary>
        /// Loads all data objects from the given backup.
        /// </summary>
        /// <param name="backup">The backup to load data from.</param>
        public partial void LoadDatas(Backup backup);

        /// <summary>
        /// Saves all data objects into the given backup.
        /// </summary>
        /// <param name="backup">The backup to save data into.</param>
        public partial void SaveDatas(Backup backup);

        /// <summary>
        /// Attempts to find a data object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object to find.</typeparam>
        /// <param name="data">When this method returns, contains the found data object if successful; otherwise, the default value for the type.</param>
        /// <returns><c>true</c> if the data object was found; otherwise, <c>false</c>.</returns>
        public partial bool TryFindData<T>(out T data) where T : Backup.IData;

        /// <summary>
        /// Add a data to the current active datas.
        /// </summary>
        /// <param name="data">The data to add.</param>
        public partial void AddData<T>(T data) where T : Backup.IData;

        #endregion
    }
}