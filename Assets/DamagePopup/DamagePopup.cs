using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private float disappearTime;
    [SerializeField] private Color textColor;

    public static void CreateTextDamage(Vector3 position, float damage, ElementalType elementalType)
    {
        Transform damagePopTransform = Instantiate(TextDamageAsset.Instance.textDamagePrefab, position, Quaternion.identity,TextDamageAsset.Instance.transform);
        DamagePopup damagePopup = damagePopTransform.GetComponent<DamagePopup>();
        damagePopup.SetDamagePopup(damage, elementalType);

        // return damagePopup;
    }

    public void SetDamagePopup(float damage, ElementalType elementalType)
    {
        switch (elementalType)
        {
            case ElementalType.Fire:
                textColor = TextDamageAsset.Instance.fire_Color;
                break;
            case ElementalType.Thunder:
                textColor = TextDamageAsset.Instance.thunder_Color;
                break;
            case ElementalType.Frost:
                textColor = TextDamageAsset.Instance.frost_Color;
                break;
            case ElementalType.Wind:
                textColor = TextDamageAsset.Instance.wind_Color;
                break;
            default:
                textColor = Color.white;
                break;
        }
        textMesh.SetText(damage.ToString("F0"));
        textMesh.color = textColor;
        // disappearTime = 1f;
    }
    void Update()
    {
        float moveYspeed = 3f;

        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;

        disappearTime -= Time.deltaTime;
        if (disappearTime < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
