using System;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;

public class ManaNullify : MonoBehaviour
{
    [SerializeField] private PlayerAction playerAction;
    [SerializeField] private ManaNullifyData manaNullifyData;
    [Header("Zoom Animation Setting")]
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private float zoomIn;
    [SerializeField] private float zoomInDuration;
    [SerializeField] private AnimationCurve zoomInCurve;
    [SerializeField] private float zoomOut;
    [SerializeField] private float zoomOutDuration;
    [SerializeField] private AnimationCurve zoomOutCurve;
    [SerializeField] private float resetZoomDuration;
    [SerializeField] private AnimationCurve resetZoomCurve;


    [Header("Active Setting")]
    [SerializeField] private float timeScale;
    [SerializeField] private float durationToTimeScale;
    [SerializeField] private AnimationCurve slowCurve;
    [SerializeField] private float detectionRange;
    [SerializeField] private Transform symbolPrefab;
    [SerializeField] private bool isActive;
    public Action<float> OnFinishDraw;

    Sequence zoomSequence;
    float originalSize;
    void ZoomAnim()
    {
        Debug.Log("Start Nullify");
        zoomSequence.Kill();
        zoomSequence = DOTween.Sequence();
        zoomSequence.Append(DOTween.To(() => vCam.m_Lens.OrthographicSize, x => vCam.m_Lens.OrthographicSize = x, originalSize + zoomIn, zoomInDuration)).SetEase(zoomInCurve);
        zoomSequence.Append(DOTween.To(() => vCam.m_Lens.OrthographicSize, x => vCam.m_Lens.OrthographicSize = x, originalSize + zoomOut, zoomOutDuration)).SetEase(zoomOutCurve).OnComplete(() => zoomSequence.Kill());

    }
    private void ResetZoom()
    {
        if (!isActive)
        {
            zoomSequence.Kill();
            zoomSequence = DOTween.Sequence();
            zoomSequence.Append(DOTween.To(() => vCam.m_Lens.OrthographicSize, x => vCam.m_Lens.OrthographicSize = x, originalSize, resetZoomDuration)).SetEase(zoomOutCurve).OnComplete(() => zoomSequence.Kill());
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(DrawCasterUtil.GetMidTransformOf(transform.root).position, detectionRange);
    }
    void Active()
    {
        isActive = true;
        Debug.Log("ActiveNullify");
        SlowIN();
        // Show all nullifyable
        AttackHit[] allAttackHit = FindObjectsOfType<AttackHit>();
        List<Transform> allObjectInRange = new();
        foreach (AttackHit attackHit in allAttackHit)
        {
            float distance = Vector2.Distance(attackHit.transform.position, DrawCasterUtil.GetUpperTransformOf(transform.root).position);
            if (distance <= detectionRange)
            {
                allObjectInRange.Add(attackHit.transform);
            }
        }
        List<Transform> allSymbol = new();
        foreach (Transform objectInRange in allObjectInRange)
        {
            allSymbol.Add(ShowDrawSymbol(objectInRange.transform));
        }

        // Draw to nullify all


        // Gain mana

        // Destroy every nullify object in order

        //Back to normal
    }
    private void DisableInput()
    {
        playerAction.Player.Movement.Disable();
        playerAction.Player.PressAttack.Disable();
        playerAction.Player.HoldAttack.Disable();
        playerAction.Player.Spell_Q.Disable();
        playerAction.Player.Spell_E.Disable();
        playerAction.Player.Spell_R.Disable();
        playerAction.Player.Spell_Shift.Disable();
        playerAction.Player.Interact.Disable();
        playerAction.Player.LeftClick.Disable();
    }
    private void EnableInput()
    {
        playerAction.Player.Movement.Enable();
        playerAction.Player.PressAttack.Enable();
        playerAction.Player.HoldAttack.Enable();
        playerAction.Player.Spell_Q.Enable();
        playerAction.Player.Spell_E.Enable();
        playerAction.Player.Spell_R.Enable();
        playerAction.Player.Spell_Shift.Enable();
        playerAction.Player.Interact.Enable();
        playerAction.Player.LeftClick.Enable();
    }
    private Transform ShowDrawSymbol(Transform transform)
    {
        return Instantiate(symbolPrefab, transform);
    }

    private void SlowIN()
    {
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, timeScale, durationToTimeScale)
        .SetUpdate(true).SetEase(slowCurve);
    }
    private void SlowOut()
    {
        if (!isActive)
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, durationToTimeScale)
            .SetUpdate(true).SetEase(slowCurve);
        }
    }

    private void OnEnable()
    {
        originalSize = vCam.m_Lens.OrthographicSize;
        playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.ManaNullify.Enable();
        playerAction.Player.ManaNullify.started += (ctx) => ZoomAnim();
        playerAction.Player.ManaNullify.performed += (ctx) => Active();
        playerAction.Player.ManaNullify.canceled += (ctx) => SlowOut();
        playerAction.Player.ManaNullify.canceled += (ctx) => ResetZoom();



    }



    private void OnDisable()
    {
        vCam.m_Lens.OrthographicSize = originalSize;
    }
}
