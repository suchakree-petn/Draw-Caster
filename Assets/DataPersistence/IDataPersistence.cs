using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using UnityEngine;

namespace DrawCaster.DataPersistence
{
    public interface IDataPersistence
    {
        void LoadData(GameData data);
        void SaveData(ref GameData data);
    }
}

