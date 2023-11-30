using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    [SerializeField] private PlayerData defaultPlayerData;

    public GameData(){
        this.playerData = defaultPlayerData;
    }
}
