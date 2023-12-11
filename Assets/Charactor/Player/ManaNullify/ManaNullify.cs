using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DG.Tweening;
using DrawCaster.Util;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public float totalActiveDuration => activeDuration * allMarkObject.Count;
    public List<Transform> allMarkObject = new();
    [SerializeField] private List<NullifyMark> allMark = new();
    public Action<float[], Vector2> OnFinishDraw;
    public float manaGainBase;
    public DrawInput_Nullify drawInput_Nullify;
    Sequence zoomSequence;
    Sequence nullifySequence;
    float originalSize;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI textCd;
    float _cooldown;
    [SerializeField] private GameObject UI;
    public float bgFadeDuration;
    [SerializeField] private Collider2D player_hitbox;
    void Start(){
        vCam = GameController.Instance.transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();
        originalSize = vCam.m_Lens.OrthographicSize;
    }
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
    void PlaySoundTimestop(){
        gameObject.GetComponentInChildren<AudioSource>().Play();
    }
    void Active()
    {
        PlaySoundTimestop();
        _cooldown = manaNullifyData.cooldown;
        // Play animation
        Debug.Log("ActiveNullify");
        nullifySequence.Kill();
        DisableInput();
        isActive = true;
        player_hitbox.enabled = false;
        SlowIn();
        playerAction.Player.ManaNullify.Disable();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            if (icon == null)
            {
                icon = GameObject.Find("PlayerUI").transform.GetChild(3).GetChild(4).GetChild(1).GetComponent<Image>();
            }
            icon.fillAmount = 0;
            if (textCd == null)
            {
                if(GameObject.Find("textCdMN") != null)textCd = GameObject.Find("textCdMN").gameObject.GetComponent<TextMeshProUGUI>();
            }
        });
        sequence.AppendInterval(manaNullifyData.cooldown).OnUpdate(() =>
        {
            icon.fillAmount += Time.deltaTime / manaNullifyData.cooldown;
            ShowCooldown();
        }).OnComplete(() =>
        {
            icon.fillAmount = 1;
            gameObject.SetActive(true);
        });
    }
    void ShowCooldown(){
        if(_cooldown >= 1 && !textCd.enabled){
            textCd.enabled = true;
        }else if(textCd.text == "0"){
            textCd.enabled = false;
        }
        textCd.text = (_cooldown -= Time.deltaTime).ToString("0");
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
        FadeInUI();
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, timeScale, durationToTimeScale)
        .SetUpdate(true).SetEase(slowCurve).OnComplete(() =>
        {
            nullifySequence = DOTween.Sequence();
            Debug.Log(totalActiveDuration);
            nullifySequence.AppendInterval(totalActiveDuration).SetUpdate(true)
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
                Debug.Log("Finish slow in");
                BackToNormal();
                drawInput_Nullify.gameObject.SetActive(false);
                playerAction.Player.ManaNullify.Enable();

                gameObject.SetActive(false);

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

    private void Show(InputAction.CallbackContext context)
    {
        // Show all nullifyable
        AttackHit[] allAttackHit = FindObjectsOfType<AttackHit>();
        List<Transform> allObjectInRange = new();
        foreach (AttackHit attackHit in allAttackHit)
        {
            if (attackHit == null) { return; }
            if (attackHit.elementalDamage.attacker.tag != "Enemy" || attackHit.transform.root.tag == "Enemy") { continue; }
            float distance = Vector2.Distance(attackHit.transform.position, DrawCasterUtil.GetUpperTransformOf(transform.root).position);
            if (distance <= detectionRange)
            {
                allObjectInRange.Add(attackHit.transform);
            }
        }
        if (allObjectInRange.Count == 0)
        {
            return;
        }

        for (int i = 0; i < allObjectInRange.Count; i++)
        {
            allMark.Add(manaNullifyData.GetRandomMark());
            allMarkObject.Add(ShowDrawSymbol(allObjectInRange[i].transform, allMark[i].sprite));
        }

        Active();

    }
    private void DestroyMarkObject(float[] scores, Vector2 finishMousePos)
    {
        float max = Mathf.Max(scores);
        if (max == 0)
        {
            return;
        }
        int count1 = allMarkObject.Count;
        for (int i = 0; i < count1; i++)
        {
            float score = scores[i];
            Debug.Log("score: " + score + " Max: " + max);
            if (score >= max || i == count1 - 1)
            {
                if (allMarkObject[i] == null)
                {
                    break;
                }
                Destroy(allMarkObject[i].parent.gameObject);
                Debug.Log("destroy " + allMarkObject[i].name);
                allMarkObject.RemoveAt(i);

                // Gain mana
                float manaGain = manaGainBase * Mathf.Pow(1 + scores[i], 6);
                Debug.Log(manaGainBase * Mathf.Pow(1 + scores[i], 6));
                playerManager.GainMana(manaGain);
                break;
            }
        }
    }

    private void BackToNormal()
    {
        FadeOutUI();
        isActive = false;
        player_hitbox.enabled = true;
        SlowOut();
        ResetZoom();
        EnableInput();
        allMark.Clear();
        foreach (Transform markObj in allMarkObject)
        {
            if (markObj == null) { continue; }
            Destroy(markObj.gameObject);
        }
        allMarkObject.Clear();
    }

    private void FadeInUI()
    {
        if (!UI.activeInHierarchy)
        {
            UI.SetActive(true);
            UI.transform.GetChild(0).gameObject.SetActive(true);
            SpriteRenderer spriteRenderer = UI.transform.GetChild(1).GetComponent<SpriteRenderer>();
            spriteRenderer.DOFade(0.5f, bgFadeDuration).SetUpdate(true);
        }
        else
        {
            Debug.Log("bg is active");
        }
    }
    private void FadeOutUI()
    {
        if (UI.activeInHierarchy)
        {
            UI.transform.GetChild(0).gameObject.SetActive(false);
            SpriteRenderer spriteRenderer = UI.transform.GetChild(1).GetComponent<SpriteRenderer>();
            spriteRenderer.DOFade(0, bgFadeDuration).OnComplete(() =>
            {
                UI.SetActive(false);
            });
        }
        else
        {
            Debug.Log("bg is not active");
        }
    }

    private void OnEnable()
    {
        // DOTween.Kill(this);
        // vCam = GameController.Instance.transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();
        // originalSize = vCam.m_Lens.OrthographicSize;
        playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.ManaNullify.Enable();
        playerAction.Player.ManaNullify.started += (ctx) => ZoomAnim();
        playerAction.Player.ManaNullify.performed += (ctx) =>
        {
            Show(ctx);
        };

        playerAction.Player.ManaNullify.canceled += (ctx) =>
        {
            SlowOut();
            ResetZoom();
            Debug.Log("nullify canceled");
            if (!isActive && drawInput_Nullify.gameObject.activeInHierarchy)
            {
                drawInput_Nullify.gameObject.SetActive(false);
            }
        };
        OnFinishDraw += DestroyMarkObject;

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
            MouseTrail.Instance.DisableMouseTrail();
        };

    }
    private void OnDisable()
    {
        vCam.m_Lens.OrthographicSize = originalSize;
        playerAction.Player.ManaNullify.Disable();
        OnFinishDraw -= DestroyMarkObject;

    }
}
