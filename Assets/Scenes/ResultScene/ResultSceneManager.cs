using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : Singleton<ResultSceneManager>
{
    [Header("Reward Obtain")]
    public double obtain_gold;
    public List<Spell> obtain_spells = new();

    public static Action OnInitRewardSuccess;
    protected override void InitAfterAwake()
    {
    }
    private void Start()
    {
        InitRewardData();

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
