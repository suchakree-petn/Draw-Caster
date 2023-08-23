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
        Instance = this;
    }

}
