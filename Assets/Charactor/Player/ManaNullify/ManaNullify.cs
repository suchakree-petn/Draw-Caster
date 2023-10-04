using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;

public class ManaNullify : MonoBehaviour
{
    public static ManaNullify Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private PlayerAction playerAction;
    [SerializeField] private PlayerManager playerManager;
    public ManaNullifyData manaNullifyData;
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
    [SerializeField] private Transform markPrefab;
    [SerializeField] private bool isActive;
    [SerializeField] private Vector2 spawnOffset;
    [SerializeField] private float activeDuration;
    [SerializeField] private List<Transform> allMarkObject = new();
    [SerializeField] private List<NullifyMark> allMark = new();
    public Action<float[], Vector2> OnFinishDraw;
    public float manaGainBase;
    public DrawInput_Nullify drawInput_Nullify;
    Sequence zoomSequence;
    Sequence nullifySequence;
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
        Gizmos.DrawWireSphere(DrawCasterUtil.GetMidTransformOf(transform.root).position, detectionRange);
    }
    void Active()
    {
        // Play animation
        Debug.Log("ActiveNullify");
        nullifySequence.Kill();
        DisableInput();
        isActive = true;
        SlowIn();
        playerAction.Player.ManaNullify.Disable();


        // Draw to nullify all


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
    private Transform ShowDrawSymbol(Transform transform, Sprite markSprite)
    {
        Transform _markPrefab = Instantiate(markPrefab, transform.position + (Vector3)spawnOffset, Quaternion.identity, transform);
        _markPrefab.GetComponent<SpriteRenderer>().sprite = markSprite;
        return _markPrefab;
    }

    private void SlowIn()
    {

        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, timeScale, durationToTimeScale)
        .SetUpdate(true).SetEase(slowCurve).OnComplete(() =>
        {
            nullifySequence = DOTween.Sequence();
            nullifySequence.AppendInterval(activeDuration).SetUpdate(true)
            .OnUpdate(() =>
            {
                if (Input.GetMouseButtonUp(1) && !drawInput_Nullify.gameObject.activeInHierarchy && isActive)
                {
                    drawInput_Nullify.gameObject.SetActive(true);
                    playerAction.Player.ManaNullify.Disable();
                }
            })
            .OnComplete(() =>
            {
                BackToNormal();
                drawInput_Nullify.gameObject.SetActive(false);
                playerAction.Player.ManaNullify.Enable();
            });
        });
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
        playerAction.Player.ManaNullify.performed += (ctx) =>
        {
            // Show all nullifyable
            AttackHit[] allAttackHit = FindObjectsOfType<AttackHit>();
            List<Transform> allObjectInRange = new();
            foreach (AttackHit attackHit in allAttackHit)
            {
                if (attackHit.elementalDamage.attacker.tag != "Enemy" || attackHit.transform.root.tag == "Enemy") { continue; }
                float distance = Vector2.Distance(attackHit.transform.position, DrawCasterUtil.GetUpperTransformOf(transform.root).position);
                if (distance <= detectionRange)
                {
                    allObjectInRange.Add(attackHit.transform);
                }
            }
            if (allObjectInRange.Count == 0)
            {
                Debug.Log("))))))");
                return;
            }

            for (int i = 0; i < allObjectInRange.Count; i++)
            {
                allMark.Add(manaNullifyData.GetRandomMark());
                allMarkObject.Add(ShowDrawSymbol(allObjectInRange[i].transform, allMark[i].sprite));
            }

            Active();
        };
        playerAction.Player.ManaNullify.canceled += (ctx) =>
        {
            SlowOut();
            ResetZoom();
            drawInput_Nullify.gameObject.SetActive(false);
        };
        OnFinishDraw += (scores, finishMousePos) =>
        {
            foreach (float item in scores)
            {
                Debug.Log(item + " ===> score");
            }
            List<Transform> toRemoveGO = new();
            float max = Mathf.Max(scores);
            if (max == 0) { return; }
            int count1 = allMarkObject.Count;
            List<float> allDistance = new();
            for (int i = 0; i < count1; i++)
            {
                allDistance.Add(Vector2.Distance(finishMousePos, (Vector2)allMarkObject[i].position));
            }
            for (int i = 0; i < count1; i++)
            {
                if (scores[i] == max)
                {
                    toRemoveGO.Add(allMarkObject[i]);
                    // Gain mana
                    float manaGain = manaGainBase * (1 + scores[i]);
                    playerManager.GainMana(manaGain);
                }
            }
            int count2 = toRemoveGO.Count;
            Debug.Log(count2 + "This is count2");
            for (int i = 0; i < count2; i++)
            {
                allMarkObject.Remove(toRemoveGO[i].transform);
                Destroy(toRemoveGO[i].parent.gameObject);

            }
        };
        transform.root.GetComponent<PlayerManager>().OnPlayerKnockback += BackToNormal;
        playerAction.Player.DrawInput.canceled += (ctx) =>
        {
            List<Texture2D> texture2Ds = new();
            foreach (NullifyMark mark in allMark)
            {
                texture2Ds.Add(mark.texture);
            }
            OnFinishDraw?.Invoke(
                drawInput_Nullify.ResamplingMouseInputPos(texture2Ds.ToArray()),
                DrawCasterUtil.GetCurrentMousePosition()
                );
        };

    }


    private void BackToNormal()
    {
        isActive = false;
        SlowOut();
        ResetZoom();
        EnableInput();
        allMark.Clear();
        foreach (Transform markObj in allMarkObject)
        {
            Destroy(markObj.gameObject);
        }
        allMarkObject.Clear();
    }

    private void OnDisable()
    {
        vCam.m_Lens.OrthographicSize = originalSize;
        playerAction.Player.ManaNullify.Disable();
        DOTween.Kill(this);
    }
}
