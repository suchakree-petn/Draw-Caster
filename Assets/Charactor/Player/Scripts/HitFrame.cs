
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HitFrame : MonoBehaviour
{
    [SerializeField] private Image red;
    [SerializeField] private Color color;
    [SerializeField] private float duration;

    private void RedFrame(GameObject player, Elemental elemental)
    {
        DOTween.Kill(red);
        red.color = color;
        red.DOFade(0, duration);
    }
    private void OnEnable()
    {
        GameController.OnPlayerTakeDamage += RedFrame;
    }
    private void OnDisable()
    {
        GameController.OnPlayerTakeDamage -= RedFrame;
    }
}
