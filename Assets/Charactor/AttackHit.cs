using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHit : MonoBehaviour
{
    public float selfDestructTime;
    public Elemental elementalDamage;
    public float iFrameTime;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.transform.root.GetComponent<IDamageable>();
        if (damageable != null && elementalDamage != null && other.CompareTag("Hitbox") && elementalDamage.targetLayer == (1 << other.gameObject.layer))
        {
            damageable.TakeDamage(elementalDamage);
            StartCoroutine(DelayCancelIgnoreLayer(other));
        }
    }
    IEnumerator DelayCancelIgnoreLayer(Collider2D other)
    {
        Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(iFrameTime);
        Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), false);

    }
    private void Start()
    {
        if (selfDestructTime != 0)
        {
            Destroy(gameObject, selfDestructTime);
        }
    }
}
