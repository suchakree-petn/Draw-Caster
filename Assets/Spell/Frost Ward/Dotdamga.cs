using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dotdamga : MonoBehaviour
{
    Collider2D collider2D;
    Coroutine coroutine;
    [SerializeField] private float _timedelay;
    void Start()
    {
        collider2D = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        if(coroutine==null)coroutine = StartCoroutine(Delay(_timedelay));
    }
    IEnumerator Delay(float timedelay){
        yield return new WaitForSeconds(timedelay);
        collider2D.enabled = false;
        yield return new WaitForSeconds(0.01f);
        collider2D.enabled = true;
        coroutine = null;
    }
}
