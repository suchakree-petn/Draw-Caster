using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelCollider : MonoBehaviour
{
    Coroutine coroutine;
    Collider2D collider2D;
    [SerializeField] private float delayTime;
    void Start()
    {
        collider2D = gameObject.GetComponent<Collider2D>();
    }
    void Update()
    {
        if (collider2D.enabled == true)
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(DelayCancel(delayTime));
            }
        }
    }
    IEnumerator DelayCancel(float time)
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
        coroutine = null;
    }
}
