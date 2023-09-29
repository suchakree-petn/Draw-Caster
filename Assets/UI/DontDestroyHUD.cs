using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyHUD : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
