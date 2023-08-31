using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Fire Wand", menuName = "Item/Wand/Fire Wand")]
public class FireWand : Weapon
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;

    [Header("Spell Setting")]
    [SerializeField] private float spreadAngle;
    [SerializeField] private int fireBallMoveSpeed;
    [SerializeField] private int fireBallAmount;
    [SerializeField] private GameObject fireBallPrefab;

    public override void Attack(GameObject attacker)
    {

        for (int i = 0; i < fireBallAmount; i++)
        {
            if (fireBallPrefab != null)
            {
                ShootSpreadingFireBall(attacker.transform);
            }
            else
            {
                Debug.Log(fireBallPrefab.name + " is null");
            }
        }
    }
    public override void HoldAttack(GameObject attacker)
    {
        base.HoldAttack(attacker);

        for (int i = 0; i < fireBallAmount; i++)
        {
            if (fireBallPrefab != null)
            {
                ShootSpreadingFireBall(attacker.transform);

            }
            else
            {
                Debug.Log(fireBallPrefab.name + " is null");
            }
        }

    }
    private void ShootSpreadingFireBall(Transform transform)
    {
        // Spawn and init dmg
        GameObject fireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
        AttackHit attackHit = fireBall.transform.GetComponentInChildren<AttackHit>();
        attackHit.elementalDamage = Elemental.DamageCalculation(elementType,
                                                                transform.GetComponent<PlayerManager>().playerData,
                                                                _baseSkillDamageMultiplier);

        // Calc spread angle
        Rigidbody2D fireBallRB = fireBall.GetComponent<Rigidbody2D>();
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float randomSpread = Random.Range(0, spreadAngle) * Mathf.Deg2Rad;
        Vector2 spawnPos = new Vector2(Mathf.Cos(randomSpread), Mathf.Sin(randomSpread));

        // Shoot it
        if (fireBallRB != null)
        {
            Vector2 direction = (mousePos.normalized - (spawnPos + (Vector2)transform.position));
            fireBallRB.AddForce(direction.normalized * fireBallMoveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            fireBallRB.transform.up = direction;
        }
        else
        {
            Debug.Log(fireBallRB.name + " is null");
        }

    }

}
