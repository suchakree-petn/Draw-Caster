using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace DrawCaster.DataPersistence
{
    public class DataPersistenceManager : Singleton<DataPersistenceManager>
    {
        [Header("File Storage Config")]
        [SerializeField] private string fileName;

        [SerializeField] private GameData gameData;
        private List<IDataPersistence> dataPersistencesObjects;
        private FileDataHandler dataHandler;

        protected override void InitAfterAwake()
        {
            this.dataPersistencesObjects = FindAllDataPersistenceObjects();

        }

        private void Start()
        {
            this.dataHandler = new(Application.persistentDataPath, fileName);
            // SaveGame();
            LoadGame();

        }


        public void NewGame()
        {
            this.gameData = new GameData();
        }

        public void LoadGame()
        {
            // load saved data
            this.gameData = dataHandler.Load();

            // if no data can be loaded, init a new game data
            if (this.gameData == null)
            {
                Debug.LogWarning("No data was found, Init new game");
                NewGame();
            }

            //push loaded data to other scripts that need
            foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
            Debug.Log("Loaded Player data");

        }

        public void SaveGame()
        {
            //pass loaded data to other scripts so  they can update it
            foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }

            //save data to file using data handler
            dataHandler.Save(gameData);
            Debug.Log("Saved Player data");

        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();
            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}
