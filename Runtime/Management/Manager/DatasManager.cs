using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTemplate
{
    public partial class DatasManager : Manager<DatasManager>
    {
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            _datasMapping ??= new();
        }

        #endregion

        #region Public Methods

        public partial void LoadDatas(Backup backup)
        {
            _datasMapping.Clear();

            foreach (Backup.IData data in backup.BackupDatas)
            {
                Backup.IData copy = data.Clone() as Backup.IData;
                _datasMapping.Add(copy.Type, copy);
            }
        }

        public partial void SaveDatas(Backup backup)
        {
            foreach (Backup.IData data in _datasMapping.Values)
            {
                Backup.IData copy = data.Clone() as Backup.IData;
                backup.AddData(true, copy);
            }
        }

        public partial bool TryFindData<T>(out T data) where T : Backup.IData
        {
            try
            {
                data = (T)_datasMapping[typeof(T)];
                return true;
            }
            catch (Exception ex) when (ex is InvalidCastException or KeyNotFoundException)
            {
                data = default;
                return false;
            }
        }

        public partial void AddData<T>(T data) where T : Backup.IData
        {
            if (!_datasMapping.ContainsKey(data.Type))
            {
                _datasMapping.Add(data.Type, data);
            }
        }

        #endregion
    }
}