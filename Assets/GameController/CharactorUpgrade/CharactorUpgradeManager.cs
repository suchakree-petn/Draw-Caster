using System;
using DrawCaster.DataPersistence;
using QFSW.QC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharactorUpgradeManager : Singleton<CharactorUpgradeManager>, IDataPersistence
{
    [Header("Loaded Data")]
    public double Gold;
    public PlayerStat playerStat;

    public int upgrade_cost
    {
        get
        {
            return (int)(1.5f * 30 * playerStat._level + 30);
        }
    }

    [Header("Ref Component")]
    [SerializeField] private TextMeshProUGUI gold_text;
    [SerializeField] private TextMeshProUGUI upgrade_cost_text;
    [SerializeField] private Button back_button;

    public static Action OnSuccessUpgradeLevel;
    public static Action OnFailUpgradeLevel;

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
    }

    private void OnApplicationQuit()
    {
        DataPersistenceManager.Instance.SaveGame();

    }

    public void PlayerLevelUp()
    {
        if (Gold >= upgrade_cost)
        {
            Gold -= upgrade_cost;
            OnSuccessUpgradeLevel?.Invoke();
            DataPersistenceManager.Instance.SaveGame();

        }
        else
        {
            OnFailUpgradeLevel?.Invoke();
        }
    }

    private void PlayerStatGrowth()
    {
        playerStat._level++;
        playerStat._attackBase = 0.3f * 50 * playerStat._level + 50;
        playerStat._defenseBase = 0.3f * 100 * playerStat._level + 100;
        playerStat._hpBase = 0.1f * 2000 * playerStat._level + 2000;
        playerStat._moveSpeed = 0.03f * 5 * playerStat._level + 5;
        playerStat._manaBase = 0.05f * 300 * playerStat._level + 300;
        if (playerStat._moveSpeed > 10)
        {
            playerStat._moveSpeed = 10;
        }
    }

    public void UpdateGold()
    {
        gold_text.text = Gold.ToString();
    }
    public void UpdateUpgradeCost()
    {
        upgrade_cost_text.text = "Level Up! " + upgrade_cost.ToString() + " g.";
    }

    private void OnEnable()
    {
        DataPersistenceManager.OnLoadSuccess += UpdateGold;
        DataPersistenceManager.OnLoadSuccess += UpdateUpgradeCost;

        OnSuccessUpgradeLevel += PlayerStatGrowth;
        OnSuccessUpgradeLevel += UpdateGold;
        OnSuccessUpgradeLevel += UpdateUpgradeCost;

    }
    private void OnDisable()
    {
        DataPersistenceManager.OnLoadSuccess -= UpdateGold;
        DataPersistenceManager.OnLoadSuccess -= UpdateUpgradeCost;

        OnSuccessUpgradeLevel -= PlayerStatGrowth;
        OnSuccessUpgradeLevel -= UpdateGold;
        OnSuccessUpgradeLevel -= UpdateUpgradeCost;

    }

    
}
