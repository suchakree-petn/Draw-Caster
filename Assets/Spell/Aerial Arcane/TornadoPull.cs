using DrawCaster.Util;
using UnityEngine;

public class TornadoPull : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float pullPower;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == null) return;
        if (other.CompareTag("Hitbox") && targetLayer == (1 << other.gameObject.layer))
        {

            Transform parent = other.transform.root;

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
