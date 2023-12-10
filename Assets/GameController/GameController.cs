using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using UnityEngine.UI;
using DrawCaster.DataPersistence;
using QFSW.QC;


public enum GameState
{
    InstantiateUI,
    BeforeStart,
    Start,
    InGame,
    BeforeEnding,
    Ending
}
public class GameController : MonoBehaviour
{
    public static GameController Instance;


    public GameState currentState;
    public delegate void GameStateBehaviour();
    public static GameStateBehaviour OnInstantiateUI;
    public static GameStateBehaviour OnBeforeStart;
    public static GameStateBehaviour OnStart;
    public static GameStateBehaviour WhileInGame;
    public static GameStateBehaviour OnBeforeEnding;
    public static GameStateBehaviour OnEnding;



    [Header("Entity in scene")]
    [SerializeField] private List<GameObject> allEnemyInScene = new List<GameObject>();
    public GameObject[] AllEnemy => allEnemyInScene.ToArray();
    public List<string> scene = new List<string>();
    public int currentScene = 0;
    public GameObject doorToNextStage;
    [SerializeField] private GameObject tmpStageFloor;
    [SerializeField] private GameObject playerUI;
    public float player_primary_damage_value;

    public static Action<GameObject> OnPlayerDead;
    public static Action<GameObject, Elemental> OnPlayerTakeDamage;
    public static Action OnGoToNextFloor;


    private void Start()
    {
        DataPersistenceManager.Instance.LoadGame();

    }
    private void Update()
    {
        switch (currentState)
        {
            case GameState.InstantiateUI:
                OnInstantiateUI?.Invoke();
                Debug.LogWarning("Instance UI");
                currentState = GameState.BeforeStart;
                break;
            case GameState.BeforeStart:
                OnBeforeStart?.Invoke();
                currentState = GameState.Start;
                break;
            case GameState.Start:
                OnStart?.Invoke();
                currentState = GameState.InGame;
                break;
            case GameState.InGame:
                WhileInGame?.Invoke();
                break;
            case GameState.BeforeEnding:
                OnBeforeEnding?.Invoke();
                currentState = GameState.Ending;
                break;
            case GameState.Ending:
                OnEnding?.Invoke();
                break;
        }
    }
    void InstantiatePlayerUI()
    {
        GameObject playUIInScene = GameObject.Find("PlayerUI");
        if (playUIInScene == null)
        {
            Instantiate(playerUI);
        }
    }
    public void RemoveEnemyDead(GameObject enemy)
    {
        allEnemyInScene.Remove(enemy);
    }
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(transform.root);
    }



    void AddAllEnemyInSceneToList()
    {
        Debug.Log("AddAll");
        GameObject[] allEnemyInScene = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemyInScene)
        {
            this.allEnemyInScene.Add(enemy);
        }
    }

    public string GetSceneName()
    {
        Scene sceneName = SceneManager.GetActiveScene();
        return sceneName.name;
    }
    public void StageClear()
    {
        if (allEnemyInScene.Count == 0 && currentState == GameState.InGame)
        {
            currentState = GameState.BeforeEnding;
        }
    }
    private void ShowStageFloor()
    {
        if (GameObject.Find("StageFloor") != null) tmpStageFloor = GameObject.Find("StageFloor");
        tmpStageFloor.GetComponent<TextMeshProUGUI>().text = GetSceneName();
    }
    public void GenerateDoor()
    {
        Debug.Log("GenerateDoor");
        Instance.doorToNextStage.SetActive(true);
        // float offset = 3;
        // doorToNextStage.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0, offset, 0);
    }
    void DisableTipsButton()
    {
        if (GameObject.Find("Canvas_UI_Learning") != null)
        {
            GameObject.Find("Canvas_UI_Learning").transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
            Debug.Log("DisableTipsButton");
            SetUILearningToDefault();
        }
    }
    void SetUILearningToDefault()
    {
        Transform canvasUILearning = GameObject.Find("Canvas_UI_Learning").transform;
        canvasUILearning.GetChild(0).gameObject.SetActive(false);
        canvasUILearning.GetChild(1).gameObject.SetActive(false);
        canvasUILearning.GetChild(2).gameObject.SetActive(false);
    }
    void LearningStage()
    {
        if (GameObject.Find("Canvas_UI_Learning") != null)
        {
            string currentSceneName = GetSceneName();
            GameObject spellSlots = GameObject.FindWithTag("Player").transform.GetChild(4).gameObject;
            GameObject manaNullify = GameObject.FindWithTag("Player").transform.GetChild(6).gameObject;
            GameObject canvasUILearning = GameObject.Find("Canvas_UI_Learning");
            GameObject tips_1 = canvasUILearning.transform.GetChild(0).gameObject;
            GameObject tips_2 = canvasUILearning.transform.GetChild(1).gameObject;
            GameObject tips_3 = canvasUILearning.transform.GetChild(2).gameObject;
            GameObject buttonTips = canvasUILearning.transform.GetChild(3).gameObject;
            tips_1.SetActive(false);
            tips_2.SetActive(false);
            tips_3.SetActive(false);
            buttonTips.SetActive(false);
            spellSlots.SetActive(true);
            manaNullify.SetActive(true);
            if (currentSceneName == "Stage_1_Learning")
            {
                tips_1.SetActive(true);
                buttonTips.SetActive(true);
                spellSlots.SetActive(false);
                manaNullify.SetActive(false);
            }
            else if (currentSceneName == "Stage_2_Learning")
            {
                tips_2.SetActive(true);
                buttonTips.SetActive(true);
                manaNullify.SetActive(false);
            }
            else if (currentSceneName == "Stage_3_Learning")
            {
                tips_3.SetActive(true);
                buttonTips.SetActive(true);
            }
        }
    }

    public void ToResultScene()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);

        // Save game
        DataPersistenceManager.Instance.SaveGame();

        // Result Scene
        SceneManager.LoadScene(6);
        Destroy(gameObject);
    }

    private void InitPlayerDamageValue()
    {
        FireStaff fireStaff = Resources.Load<FireStaff>("Weapon/Fire Staff");
        PlayerData playerData = GameObject.FindWithTag("Player").GetComponent<PlayerManager>().GetCharactorData();
        player_primary_damage_value = fireStaff._baseSkillDamageMultiplier * playerData._attackBase;
    }
    private void OnEnable()
    {
        OnInstantiateUI += InstantiatePlayerUI;
        OnBeforeStart += () =>
        {

            if (scene[scene.Count - 1] == SceneManager.GetActiveScene().name)
            {
                Destroy(PlayerInputSystem.Instance.transform.root.gameObject);
                Destroy(GameObject.FindWithTag("Player").transform.root.gameObject);
                Destroy(Instance.transform.root.gameObject);
                return;
            }
            AddAllEnemyInSceneToList();
            ShowStageFloor();
            LearningStage();
            DontDestroyOnLoad(ManaNullify.Instance.transform.root);
            Instance.transform.GetChild(2).GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").transform;
            Instance.transform.GetChild(2).GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.FindWithTag("BorderMap").GetComponent<Collider2D>();
        };
        OnBeforeStart += InitPlayerDamageValue;
        WhileInGame += StageClear;
        OnBeforeEnding += GenerateDoor;
        OnBeforeEnding += DisableTipsButton;


    }
    private void OnDisable()
    {
        OnInstantiateUI -= InstantiatePlayerUI;
        // OnBeforeStart -= ShowStageFloor;
        WhileInGame -= StageClear;
        OnBeforeEnding -= GenerateDoor;
        OnBeforeEnding -= DisableTipsButton;
        OnBeforeStart -= InitPlayerDamageValue;


    }
    [Command]
    public void ClearEnemy()
    {
        foreach (GameObject enemy in allEnemyInScene)
        {
            Destroy(enemy);
        }
        allEnemyInScene.Clear();
        StageClear();
    }
}


