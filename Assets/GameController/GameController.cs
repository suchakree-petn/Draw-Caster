using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CharactorData player;
    void Start()
    {
        Instantiate(player.CharactorPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
