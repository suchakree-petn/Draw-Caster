using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ManaNullifyUI : MonoBehaviour
{
    [SerializeField] private Slider durationBar;
    [SerializeField] private ManaNullify manaNullify;

    private void OnEnable()
    {
        durationBar.value = 1;
        durationBar.DOValue(0, manaNullify.totalActiveDuration)
        .SetUpdate(true)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
        });
    }
}
