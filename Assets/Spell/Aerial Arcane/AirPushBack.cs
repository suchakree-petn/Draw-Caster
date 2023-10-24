using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class AirPushBack : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float pushPower;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.transform.root.GetComponent<IDamageable>();
        if (damageable != null && other.CompareTag("Hitbox") && targetLayer == (1 << other.gameObject.layer))
        {
            Transform parent = other.transform.root;
            Rigidbody2D targetRB = parent.GetComponent<Rigidbody2D>();
            targetRB.DOMove((parent.position - transform.position).normalized * pushPower,1f);
        }
    }
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     IDamageable damageable = other.transform.root.GetComponent<IDamageable>();
    //     if (damageable != null && other.CompareTag("Hitbox") && targetLayer == (1 << other.gameObject.layer))
    //     {
    //         Transform parent = other.transform.root;
    //         Rigidbody2D targetRB = parent.GetComponent<Rigidbody2D>();
    //         targetRB.velocity = Vector2.zero;
    //     }
    // }
}
