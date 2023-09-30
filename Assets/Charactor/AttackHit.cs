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
        IDamageable damageable = other.transform.root.GetComponent<IDamageable>();
        if (damageable != null && elementalDamage != null && other.CompareTag("Hitbox") && elementalDamage.targetLayer == (1 << other.gameObject.layer))
        {
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
