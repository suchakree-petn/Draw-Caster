using System.Collections;
using System.Collections.Generic;
using DrawCaster.Util;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Fire Staff", menuName = "Item/Wand/Fire Staff")]
public class FireStaff : Weapon
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;

    [Header("Spell Setting")]
    [SerializeField] private float spreadAngle;
    [SerializeField] private int fireBallMoveSpeed;
    [SerializeField] private int fireBallAmount;
    [SerializeField] private int fireBallDuration;
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
        fireBall = DrawCasterUtil.AddAttackHitTo(fireBall,
            elementType,
            transform.GetComponent<CharactorManager<PlayerData>>().GetCharactorData(),
            _baseSkillDamageMultiplier,
            fireBallDuration,
            targetLayer,
            knockbackGaugeDeal
            );

        // Calc spread angle
        Rigidbody2D fireBallRB = fireBall.GetComponent<Rigidbody2D>();
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        float randomSpread = Random.Range(-spreadAngle, spreadAngle) * Mathf.Deg2Rad;
        //Vector2 spawnPos = new Vector2(Mathf.Cos(randomSpread), Mathf.Sin(randomSpread));

        // Shoot it
        if (fireBallRB != null)
        {
            Vector2 direction = mousePos - (Vector2)transform.position;
            fireBallRB.AddForce(
                new Vector2(direction.x + randomSpread, direction.y + randomSpread).normalized
                * fireBallMoveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse
                );
            fireBallRB.transform.right = direction;
        }
        else
        {
            Debug.Log(fireBallRB.name + " is null");
        }

    }

}
