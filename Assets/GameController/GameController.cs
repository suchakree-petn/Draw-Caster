using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
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
    public static GameStateBehaviour OnBeforeStart;
    public static GameStateBehaviour OnStart;
    public static GameStateBehaviour WhileInGame;
    public static GameStateBehaviour OnBeforeEnding;
    public static GameStateBehaviour OnEnding;



    [Header("Entity in scene")]
    public List<GameObject> allEnemyInScene = new List<GameObject>();
    public List<GameObject> allEnemyInCamera = new List<GameObject>();

    public delegate void PlayerBehavior(GameObject enemy);
    public static PlayerBehavior OnPlayerDead;
    public static PlayerBehavior OnPlayerTakeDamage;

    public delegate void EnemyBehavior(GameObject enemy);
    public static EnemyBehavior OnEnemyDead;
    public static EnemyBehavior OnEnemyTakeDamage;

    private void Update()
    {
        switch (currentState)
        {
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
                break;
            case GameState.Ending:
                OnEnding?.Invoke();
                break;
        }
    }
    void RemoveEnemyDead(GameObject enemy)
    {
        allEnemyInScene.Remove(enemy);
        allEnemyInCamera.Remove(enemy);
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
        return bounds.min.x < cameraMax.x && bounds.max.x > cameraMin.x &&
               bounds.min.y < cameraMax.y && bounds.max.y > cameraMin.y;
    }

    void AddAllEnemyInSceneToList()
    {
        GameObject[] allEnemyInScene = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemyInScene)
        {
            this.allEnemyInScene.Add(enemy);
        }
    }
    public GameObject[] GetAllEnemyInScene()
    {
        allEnemyInCamera.Clear();
        foreach (GameObject enemy in allEnemyInScene)
        {
            if (IsObjectInCameraView(enemy))
            {
                allEnemyInCamera.Add(enemy);
            }
        }
        return allEnemyInCamera.ToArray();
    }
    private void OnEnable()
    {
        OnBeforeStart += AddAllEnemyInSceneToList;
        OnEnemyDead += RemoveEnemyDead;
    }
    private void OnDisable()
    {
        OnBeforeStart -= AddAllEnemyInSceneToList;
        OnEnemyDead -= RemoveEnemyDead;
    }
}


