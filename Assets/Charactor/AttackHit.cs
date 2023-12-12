using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHit : MonoBehaviour
{
    public float selfDestructTime;
    public Elemental elementalDamage;
    public float iFrameTime = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.transform.root.GetComponent<IDamageable>();
        if (damageable != null && elementalDamage != null && other.CompareTag("Hitbox") && elementalDamage.targetLayer == (1 << other.gameObject.layer))
        {
            if (other.transform.root.TryGetComponent<EnemyManager>(out EnemyManager enemyManager))
            {
                enemyManager.OnEnemyTakeDamage += ShakeScreen;
                damageable.TakeDamage(elementalDamage);
                enemyManager.OnEnemyTakeDamage -= ShakeScreen;
                StartCoroutine(DelayCancelIgnoreLayer(other));
            }
            else
            {
                damageable.TakeDamage(elementalDamage);
                StartCoroutine(DelayCancelIgnoreLayer(other));
            }

        }
    }

    private void ShakeScreen(GameObject gameObject, Elemental elementalDamage)
    {
        float player_primary_damage_value = GameController.Instance.player_primary_damage_value;
        float ratio = 0.8f / player_primary_damage_value;
        ScreenShakeManager.Instance.Shake(0.1f, ratio * elementalDamage._damage);
    }

    IEnumerator DelayCancelIgnoreLayer(Collider2D other)
    {
        if (other == null) { yield break; }
        Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(iFrameTime);
        if (other == null) { yield break; }
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
