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
        data.last_play_time = stageTimer;
        if (stageTimer < data.best_play_time)
        {
            data.best_play_time = stageTimer;
            Debug.Log("New best time recorded!");
        }
    }
}
