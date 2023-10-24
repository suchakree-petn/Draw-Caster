using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using DG.Tweening;
using DrawCaster.Util;
using TMPro;

public class TextDamageAsset : MonoBehaviour
{
    public static TextDamageAsset Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private Transform textDamagePrefab;
    [SerializeField] private float randomPositionRadius;
    [SerializeField] private float popUpBounceScale;
    [SerializeField] private float moveY;
    [SerializeField] private float moveYDuration;
    [SerializeField] private float popUpSpeedToDefalault;
    [SerializeField] private float popUpDuration;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private Color fire_Color;
    [SerializeField] private Color thunder_Color;
    [SerializeField] private Color frost_Color;
    [SerializeField] private Color wind_Color;

    public void CreateTextDamage(Vector3 position, float damage, ElementalType elementalType)
    {
        Vector2 randomPosition = DrawCasterUtil.RandomPosition(position, randomPositionRadius);
        Transform damagePopTransform = Instantiate(textDamagePrefab, randomPosition, Quaternion.identity, transform);
        TextMeshPro textMesh = damagePopTransform.GetComponent<TextMeshPro>();
        textMesh.DOFade(0, 0);
        damagePopTransform.DOScale(popUpBounceScale, 0);
        SetDamagePopup(damage, elementalType, textMesh);
        Sequence sequence = DOTween.Sequence();
        textMesh.DOFade(1, fadeInDuration);
        sequence.Append(damagePopTransform.DOScale(1, popUpSpeedToDefalault).SetEase(Ease.InOutSine));
        sequence.AppendInterval(popUpDuration - moveYDuration);
        sequence.Append(damagePopTransform.DOMoveY(damagePopTransform.position.y + moveY, moveYDuration));
        sequence.Append(textMesh.DOFade(0, fadeOutDuration).OnComplete(() => Destroy(damagePopTransform.gameObject)));
    }

    public void SetDamagePopup(float damage, ElementalType elementalType, TextMeshPro textMesh)
    {
        Color textColor;
        switch (elementalType)
        {
            case ElementalType.Fire:
                textColor = fire_Color;
                break;
            case ElementalType.Thunder:
                textColor = thunder_Color;
                break;
            case ElementalType.Frost:
                textColor = frost_Color;
                break;
            case ElementalType.Wind:
                textColor = wind_Color;
                break;
            default:
                textColor = Color.white;
                break;
        }
        textMesh.SetText(damage.ToString("F0"));
        textMesh.color = textColor;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TextDamageAsset))]
    class TextDamageAssetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var textDamageAsset = (TextDamageAsset)target;
            if (textDamageAsset == null) return;

            if (GUILayout.Button("Generate Text Damage Popup"))
            {
                textDamageAsset.CreateTextDamage(Vector3.zero, 100, ElementalType.Fire);
            }
            if (GUILayout.Button("Clear"))
            {
                foreach (Transform child in textDamageAsset.transform)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }
    }
#endif
}
