using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHit : MonoBehaviour
{
    public float selfDestructTime;
    public Elemental elementalDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(elementalDamage.targetLayer);
        IDamageable damageable = other.transform.root.GetComponent<IDamageable>();
        if (damageable != null && elementalDamage != null && other.CompareTag("Hitbox") && elementalDamage.targetLayer == (1 << other.gameObject.layer))
        {
            Debug.Log("Damage to: " + other.name);
            damageable.TakeDamage(elementalDamage);
        }
    }

    private void Start()
    {
        if (selfDestructTime != 0)
        {
            Destroy(gameObject, selfDestructTime);
        }
    }
}
