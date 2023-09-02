using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    [SerializeField] private float selfDestructTime;
    public Elemental elementalDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.transform.GetComponentInParent<IDamageable>();
        {
            if (damageable != null)
            {
                damageable.TakeDamage(elementalDamage);
            }
        }
    }

    private void Start() {
        Destroy(transform.parent.gameObject,selfDestructTime);
    }
}
