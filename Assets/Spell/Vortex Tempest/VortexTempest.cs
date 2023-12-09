using System;
using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new Vortex Tempest", menuName = "Spell/Vortex Tempest")]
public class VortexTempest : Spell
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;

    [Header("Vortex Tempest Config")]
    [SerializeField] private float cast1_size;
    [SerializeField] private float cast2_size;
    [SerializeField] private float cast3_size;
    [SerializeField] private float duration;
    [SerializeField] private float knockbackGaugeDeal;
    [SerializeField] private float hit_interval;
    [SerializeField] private float select_duration;
    private Vector2 select_position;

    [Header("Reference")]
    [SerializeField] private GameObject vortexTempest_prf;
    [SerializeField] private GameObject area_prf;
    [SerializeField] private GameObject durationBar_prf;
    public override void BeginCooldown(GameObject player)
    {
    }
    public override void CastSpell(float score, GameObject player)
    {
        base.CastSpell(score, player);
        int castLevel = CalThreshold(score);


        GameObject area = Instantiate(area_prf);
        Vector3 area_size = GetSize(castLevel) * vortexTempest_prf.transform.localScale;
        area.transform.localScale = area_size;

        GameObject durationBar = Instantiate(durationBar_prf, GameObject.Find("PlayerUI").transform);
        Slider slider = durationBar.GetComponent<Slider>();
        slider.value = 1;
        slider.DOValue(0, select_duration)
        .SetUpdate(true)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            Destroy(durationBar);
        });
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.LeftClick.Enable();
        bool isClicked = false;
        Sequence select_sequence = DOTween.Sequence();
        select_sequence.AppendInterval(select_duration).SetUpdate(true).OnUpdate(() =>
        {
            area.transform.position = DrawCasterUtil.GetCurrentMousePosition();

            if (playerAction.Player.LeftClick.IsPressed() && !isClicked)
            {
                playerAction.Player.LeftClick.Disable();
                isClicked = true;
                DOTween.Kill(durationBar);
                Destroy(durationBar);

                Destroy(area);
                select_position = DrawCasterUtil.GetCurrentMousePosition();
                CastByLevel(castLevel, player, null);

                Debug.Log(DOTween.Kill(this));
            }
        }).OnComplete(() =>
        {
            if (!isClicked)
            {
                Destroy(area);
                select_position = DrawCasterUtil.GetCurrentMousePosition();
                CastByLevel(castLevel, player, null);
            }

        });
    }
    public override void Cast1(GameObject player, GameObject target)
    {
        SpawnVortexTempest(player, _damageSpellLevelMultiplier1, cast1_size);
    }

    public override void Cast2(GameObject player, GameObject target)
    {
        SpawnVortexTempest(player, _damageSpellLevelMultiplier2, cast2_size);

    }

    public override void Cast3(GameObject player, GameObject target)
    {
        SpawnVortexTempest(player, _damageSpellLevelMultiplier3, cast3_size);

    }

    private GameObject SpawnVortexTempest(GameObject attacker, float multiplier, float size_multiplier)
    {
        GameObject vortexTempest = Instantiate(vortexTempest_prf, select_position, Quaternion.identity);
        vortexTempest = DrawCasterUtil.AddAttackHitTo(
            vortexTempest,
            _elementalType, attacker,
            _baseSkillDamageMultiplier * multiplier,
            duration,
            targetLayer,
            knockbackGaugeDeal,
            hit_interval
            );
        Vector3 orginalSize = vortexTempest.transform.localScale;
        Vector2 size = orginalSize * size_multiplier;
        vortexTempest.transform.DOScale(size, 0);
        return vortexTempest;
    }
    public float GetSize(int castLevel)
    {
        if (castLevel == 1)
        {
            return cast1_size;
        }
        else if (castLevel == 2)
        {
            return cast2_size;
        }
        return cast3_size;
    }
}
