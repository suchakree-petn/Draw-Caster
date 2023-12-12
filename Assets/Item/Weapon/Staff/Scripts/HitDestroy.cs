using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDestroy : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.TryGetComponent<EnemyManager>(out EnemyManager enemyManager))
            {
                Destroy(gameObject,0.05f);
            }
    }
}
