using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Fire Staff", menuName = "Item/Weapon/Staff/Fire Staff")]
public class FireWand : Weapon
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;

    [Header("Spell Setting")]
    [SerializeField] private float spreadAngle;
    [SerializeField] private int numberOfSpreads;
    [SerializeField] private int fireBallMoveSpeed;
    [SerializeField] private int fireBallAmount;
    [SerializeField] private GameObject fireBallPrefab;

   

    // public override void Attack(GameObject attacker)
    // {

    //     base.Attack(attacker);
    //     if (fireBallPrefab != null)
    //     {
    //         ShootSpreadingFireBall(attacker.transform);
    //     }
    //     else
    //     {
    //         Debug.Log(fireBallPrefab.name + " is null");
    //     }

    // }
    // public override void HoldAttack(GameObject attacker)
    // {
    //     base.HoldAttack(attacker);

    //     for (int i = 0; i < fireBallAmount; i++)
    //     {
    //         if (fireBallPrefab != null)
    //         {
    //             GameObject fireBall = ShootSpreadingFireBall(attacker.transform);
    //             AttackHit attackHit = fireBall.GetComponent<AttackHit>();
    //             attackHit.elementalDamage = Elemental.DamageCalculation(elementType,
    //                                                                     attacker.GetComponent<PlayerManager>().playerData,
    //                                                                     _baseSkillDamageMultiplier);

    //             Rigidbody2D fireBallRB = fireBall.GetComponent<Rigidbody2D>();
    //             Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //             float randomSpread = Random.Range(-spreadAngle / 2f, spreadAngle / 2f) * Mathf.Deg2Rad;
    //             Vector2 spawnPos = new Vector2(Mathf.Cos(randomSpread), Mathf.Sin(randomSpread));
                
    //             if (fireBallRB != null)
    //             {
    //                 Vector2 direction = (mousePos - spawnPos).normalized;
    //                 fireBallRB.AddForce(direction * fireBallMoveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    //                 fireBallRB.transform.up = direction;
    //             }
    //             else
    //             {
    //                 Debug.Log(fireBallRB.name + " is null");
    //             }
    //         }
    //         else
    //         {
    //             Debug.Log(fireBallPrefab.name + " is null");
    //         }
    //     }

    // }
    // private GameObject ShootSpreadingFireBall(Transform transform)
    // {

    //     return Instantiate(fireBallPrefab, transform.position, Quaternion.identity);

    //     //Quaternion spreadRotation = Quaternion.Euler(0, 0, randomSpread);

    // }

}
