using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    public static PlayerInputSystem Instance;
    public PlayerAction playerAction;
    void Awake()
    {
        playerAction = new PlayerAction();
        if (Instance != null && Instance != this)
            {
                DestroyImmediate(gameObject);
            }else{
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
    }
}
