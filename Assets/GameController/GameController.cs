using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


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
    //public List<GameObject> allEnemyInCamera = new List<GameObject>();
    public List<string> scene = new List<string>();
    public int currentScene = 0;
    [SerializeField] private GameObject doorToNextStage;
    [SerializeField] private GameObject tmpStageFloor;
    [SerializeField] private GameObject playerUI;

    public static Action<GameObject> OnPlayerDead;
    public static Action<GameObject, Elemental> OnPlayerTakeDamage;




    private void Update()
    {
        switch (currentState)
        {
            case GameState.InstantiateUI:
                OnInstantiateUI?.Invoke();
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
    }


    private bool IsObjectInCameraView(GameObject target)
    {
        if (target == null)
        {
            return false;
        }
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found.");
            return false;
        }

        // Get the GameObject's bounds
        Bounds bounds = target.transform.GetComponentInChildren<SpriteRenderer>().bounds;

        // Calculate the camera's orthographic size
        float cameraOrthoSize = mainCamera.orthographicSize;
        float cameraAspect = mainCamera.aspect;

        // Calculate the camera's orthographic width and height
        float cameraOrthoWidth = cameraOrthoSize * cameraAspect;
        float cameraOrthoHeight = cameraOrthoSize;

        // Calculate the bounds of the camera's view in world space
        Vector2 cameraMin = (Vector2)mainCamera.transform.position - new Vector2(cameraOrthoWidth, cameraOrthoHeight);
        Vector2 cameraMax = (Vector2)mainCamera.transform.position + new Vector2(cameraOrthoWidth, cameraOrthoHeight);

        // Check if the bounds of the GameObject intersect with the camera's view bounds
        return bounds.min.x < cameraMax.x - 10 && bounds.max.x > cameraMin.x - 10 &&
               bounds.min.y < cameraMax.y - 10 && bounds.max.y > cameraMin.y - 10;
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
    // public GameObject[] GetAllEnemyInScene()
    // {
    //     allEnemyInCamera.Clear();
    //     foreach (GameObject enemy in allEnemyInScene)
    //     {
    //         if (IsObjectInCameraView(enemy))
    //         {
    //             allEnemyInCamera.Add(enemy);
    //         }
    //     }
    //     return allEnemyInCamera.ToArray();
    // }

    public void StageClear()
    {
        if (allEnemyInScene.Count == 0 && currentState == GameState.InGame)
        {
            currentState = GameState.BeforeEnding;
        }
    }
    private void ShowStageFloor()
    {
        Scene sceneName = SceneManager.GetActiveScene();
        if (GameObject.Find("StageFloor") != null) tmpStageFloor = GameObject.Find("StageFloor");
        tmpStageFloor.GetComponent<TextMeshProUGUI>().text = sceneName.name;
    }
    public void GenerateDoor()
    {
        Debug.Log("GenerateDoor");
        float offset = 3;
        doorToNextStage.SetActive(true);
        doorToNextStage.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0, offset, 0);
    }
    private void OnEnable()
    {
        OnInstantiateUI += InstantiatePlayerUI;
        AddAllEnemyInSceneToList();
        OnBeforeStart += ShowStageFloor;
        WhileInGame += StageClear;
        OnBeforeEnding += GenerateDoor;
    }
    private void OnDisable()
    {
        OnInstantiateUI -= InstantiatePlayerUI;
        OnBeforeStart -= ShowStageFloor;
        WhileInGame -= StageClear;
        OnBeforeEnding -= GenerateDoor;
    }
}


