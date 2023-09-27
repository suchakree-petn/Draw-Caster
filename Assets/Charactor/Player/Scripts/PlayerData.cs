using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New player Data", menuName = "Charactor Data/player Data")]
public class PlayerData : CharactorData
{

    public Weapon CurrentWeapon;

    public override void CheckDead(GameObject charactor, Elemental damage)
    {
        if (charactor.GetComponent<PlayerManager>().currentHp <= 0)
        {
            GameController.OnEnemyDead?.Invoke(charactor);
        }
    }

    public override void Dead(GameObject deadCharactor)
    {
        Destroy(deadCharactor);
    }
   
}
