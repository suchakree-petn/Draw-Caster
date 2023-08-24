using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Weapon weapon;

    private void Start() {
        weapon.Attack(transform.parent.GetComponentInParent<PlayerManager>().playerData);
    }
}
