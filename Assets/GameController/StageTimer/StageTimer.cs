using TMPro;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float stageTimer;
    public GameObject timerUIPrefab;
    [SerializeField] private  TextMeshProUGUI timer;
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
}
