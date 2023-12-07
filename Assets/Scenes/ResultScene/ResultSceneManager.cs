using System;
using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using UnityEngine;

public class ResultSceneManager : Singleton<ResultSceneManager>
{
    [Header("Reward Obtain")]
    public double obtain_gold;
    public List<Spell> obtain_spells = new();
    public float play_time;

    [Header("Play time")]
    public float last_play_time;
    public float best_play_time;

    public static Action OnInitRewardSuccess;
    public static Action OnInitPlayTimeSuccess;
    protected override void InitAfterAwake()
    {
    }
    private void Start()
    {
        DataPersistenceManager.Instance.LoadGame();
        InitRewardData();
        InitPlayTime();
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
}
