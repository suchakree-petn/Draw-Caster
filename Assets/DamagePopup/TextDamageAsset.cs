using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDamageAsset : MonoBehaviour
{
    public static TextDamageAsset Instance;
    private void Awake() {
        Instance = this;
    }
    public Transform textDamagePrefab;
    public Color fire_Color;
    public Color thunder_Color;
    public Color frost_Color;
    public Color wind_Color;
}
