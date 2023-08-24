using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public PlayerData playerData;

    public void TakeDamage(Elemental elementalDamage)
    {
        if (playerData.HealthPoint > 0)
        {
            // playerData.HealthPoint -= elementalDamage._damage; เปลี่ยนเป็น CalcDefense ก่อน ค่อยเอามาหักเลือด
        }
    }
}
