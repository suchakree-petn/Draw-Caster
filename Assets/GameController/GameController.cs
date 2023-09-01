using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CharactorData player;
    public List<GameObject> allEnemyInList = new List<GameObject>();
    public static GameController Instance;
    void Awake(){
        Instance = this;
    }
    void Start()
    {
        Instantiate(player.CharactorPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
