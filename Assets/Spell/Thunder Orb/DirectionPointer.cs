
using DrawCaster.Util;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{


    void Update()
    {
        Vector3 mousePos = DrawCasterUtil.GetCurrentMousePosition();
        Vector3 dir = mousePos - transform.position;
        transform.up = dir;
    }
}
