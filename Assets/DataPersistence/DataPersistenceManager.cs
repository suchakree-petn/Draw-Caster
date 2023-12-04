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
        public FileDataHandler dataHandler;

        public static Action OnLoadSuccess;

        public bool IsLoaded = false;
        
        protected override void InitAfterAwake()
        {
            this.dataHandler = new(Application.persistentDataPath, fileName);

            this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        }


        public void NewGame()
        {
            this.gameData = new GameData();
        }

        public void LoadGame()
        {
            // load saved data
            this.gameData = dataHandler.Load();
            bool isSaved = true;

            // if no data can be loaded, init a new game data
            if (this.gameData == null)
            {
                Debug.LogWarning("No data was found, Init new game");
                NewGame();
                isSaved = false;
            }

            //push loaded data to other scripts that need
            foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
            Debug.Log("Loaded Player data");
            IsLoaded = true;
            OnLoadSuccess?.Invoke();

            // Save game if file is not existed 
            if (!isSaved)
            {
                SaveGame();
            }
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
            // SaveGame();
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();
            return new List<IDataPersistence>(dataPersistenceObjects);
        }

    }
}
