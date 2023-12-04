using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using UnityEngine;
using UnityEngine.UI;

public class CharactorUpgradeManager : Singleton<CharactorUpgradeManager>, IDataPersistence
{
    public double Gold;
    public PlayerStat playerStat;

    public void LoadData(GameData data)
    {
        Gold = data.Gold;
        playerStat = data.playerStat;
    }

    public void SaveData(ref GameData data)
    {
        data.Gold = Gold;
        data.playerStat = playerStat;
    }

    protected override void InitAfterAwake()
    {


    }
    private void Start()
    {
        DataPersistenceManager.Instance.LoadGame();
        DataPersistenceManager.Instance.SaveGame();
    }

    private void OnApplicationQuit()
    {
        DataPersistenceManager.Instance.SaveGame();

    }

    

    private void OnEnable()
    {
    }
    private void OnDisable()
    {

    }
}
