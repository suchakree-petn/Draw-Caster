using System.Collections.Generic;
using DrawCaster.DataPersistence;
using TMPro;
using UnityEngine;

public class StageTimer : MonoBehaviour, IDataPersistence
{
    public float stageTimer;
    public GameObject timerUIPrefab;
    [SerializeField] private TextMeshProUGUI timer;
    private void OnEnable()
    {
        GameController.OnInstantiateUI += InitUI;
        GameController.WhileInGame += UpdateTimer;
    }
    private void OnDisable()
    {
        GameController.OnInstantiateUI -= InitUI;
        GameController.WhileInGame -= UpdateTimer;

    }

    private void UpdateTimer()
    {
        stageTimer += Time.deltaTime;
        timer.text = SecToMin();
    }

    private void InitUI()
    {
        if (timer == null)
        {
            timer = Instantiate(timerUIPrefab, GameObject.Find("PlayerUI").transform).GetComponent<TextMeshProUGUI>();
            timer.text = "0:00";
        }
    }

    private string SecToMin()
    {
        int minutes = Mathf.FloorToInt(stageTimer / 60);
        int seconds = Mathf.FloorToInt(stageTimer % 60);
        string temp = string.Format("{0:00}:{1:00}", minutes, seconds);
        return temp;
    }

    public void LoadData(GameData data)
    {
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Stage timer save");
        data.last_play_time = stageTimer;
        string dimension_id = RewardTrackerManager.Instance.dimension_id;
        if (data.dimensionData.Count == 0)
        {
            Debug.Log("null dimension data");

            data.dimensionData.Add(new DimensionData(dimension_id, stageTimer));
            return;
        }
        Debug.Log("has dimension data");

        List<DimensionData> dimensionDatas = data.dimensionData;
        for (int i = 0; i < dimensionDatas.Count; i++)
        {
            int index = i;
            if (dimensionDatas[index].dimension_id == dimension_id)
            {

                if (stageTimer < dimensionDatas[i].best_play_time)
                {

                    data.dimensionData.RemoveAt(index);
                    data.dimensionData.Add(new DimensionData(dimension_id, stageTimer));
                    Debug.Log("New best time recorded!");
                    return;
                }
                else
                {
                    return;
                }
            }
        }

        data.dimensionData.Add(new DimensionData(dimension_id, stageTimer));


    }
}
