using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter: " + other.gameObject.name);
        ICollectable collectable = other.transform.GetComponentInParent<ICollectable>();
        if (collectable != null)
        {
            Debug.Log("Collected: " + other.gameObject.name);
            collectable.Collect(other.GetComponentInParent<ItemData>().item);
            Destroy(other.transform.parent.gameObject);
        }
    }
}
