using System;
using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneManager : Singleton<ResultSceneManager>
{
    public string dimension_id;
    [Header("Reward Obtain")]
    public double obtain_gold;
    public List<Spell> obtain_spells = new();
    public float play_time;

    [Header("Play time")]
    public float last_play_time;
    public float best_play_time;

    public static Action OnInitRewardSuccess;
    public static Action OnInitPlayTimeSuccess;
    public static Action OnInitDimensionIdSuccess;
    protected override void InitAfterAwake()
    {
    }
    private void Start()
    {
        DataPersistenceManager.Instance.SaveGame();
        DataPersistenceManager.Instance.LoadGame();
        InitDimensionId();
        InitPlayTime();
        InitRewardData();
    }
    private void InitPlayTime()
    {
        RewardTrackerManager rewardTrackerManager = RewardTrackerManager.Instance;
        last_play_time = rewardTrackerManager.last_play_time;
        best_play_time = rewardTrackerManager.best_play_time;

        OnInitPlayTimeSuccess?.Invoke();
    }
    private void InitRewardData()
    {
        RewardTrackerManager rewardTrackerManager = RewardTrackerManager.Instance;

        if (rewardTrackerManager != null)
        {
            obtain_gold = rewardTrackerManager.obtain_gold;
            obtain_spells = rewardTrackerManager.obtain_spell;

            OnInitRewardSuccess?.Invoke();
            Debug.Log("Init reward success");
        }
        else
        {
            Debug.LogError("Reward Tracker Manager not found");
        }
        Destroy(rewardTrackerManager.gameObject);
    }
    private void InitDimensionId()
    {
        RewardTrackerManager rewardTrackerManager = RewardTrackerManager.Instance;

        if (rewardTrackerManager != null)
        {
            dimension_id = rewardTrackerManager.dimension_id;

            OnInitDimensionIdSuccess?.Invoke();
            Debug.Log("Init dimension Id success");
        }
        else
        {
            Debug.LogError("Reward Tracker Manager not found");
        }
    }
    public void RetryDimension()
    {
        switch (dimension_id)
        {
            case "001":
                SceneManager.LoadScene(1);
                break;
            case "002":
                SceneManager.LoadScene(9);
                break;
            case "003":
                SceneManager.LoadScene(13);
                break;
            case "004":
                SceneManager.LoadScene(17);
                break;
            default:
                Debug.LogError("No dimension to load");
                break;
        }
    }

}
