using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CharactorData player;
    public CharactorData enemy;
    public List<GameObject> allEnemyInList = new List<GameObject>();
    public List<GameObject> allEnemyInScene = new List<GameObject>();
    public static GameController Instance;
    public delegate void EnemyBehavior(GameObject enemy);
    public static EnemyBehavior OnEnemyDead;
    void RemoveEnemyDead(GameObject enemy){
        allEnemyInList.Remove(enemy);
        allEnemyInScene.Remove(enemy);
    }
    void Awake(){
        Instance = this;
    }
    void Start()
    {
        Instantiate(player.CharactorPrefab);
        GameObject _enemy = Instantiate(enemy.CharactorPrefab);
        allEnemyInList.Add(_enemy);
    }

    private bool IsObjectInCameraView(GameObject target)
    {
        if(target == null){
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
    public GameObject[] GetAllEnemyInScene(){
        allEnemyInScene.Clear();
        foreach(GameObject enemy in allEnemyInList){
            if(IsObjectInCameraView(enemy)){
                allEnemyInScene.Add(enemy);
            }
        }
        return allEnemyInScene.ToArray();
    }
    private void OnEnable() {
        OnEnemyDead += RemoveEnemyDead;
    }
    private void OnDisable() {
        OnEnemyDead -= RemoveEnemyDead;
    }
}
