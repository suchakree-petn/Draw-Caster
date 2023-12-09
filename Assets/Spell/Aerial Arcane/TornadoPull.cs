using DrawCaster.Util;
using UnityEngine;

public class TornadoPull : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float pullPower;

    [Header("Reference")]
    [SerializeField] private Rigidbody2D parentRB;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == null) return;
        if (other.CompareTag("Hitbox") && targetLayer == (1 << other.gameObject.layer))
        {

            Transform parent = other.transform.root;

            Vector2 dir = transform.root.position - parent.position;
            parent.position = Vector3.Lerp(parent.position, transform.position, pullPower * Time.fixedDeltaTime);

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null) return;
        if (other.CompareTag("Hitbox") && targetLayer == (1 << other.gameObject.layer))
        {
            other.attachedRigidbody.velocity = Vector2.zero;
        }

    }
}
