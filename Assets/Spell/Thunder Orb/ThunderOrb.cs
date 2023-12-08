using System;
using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new Thunder Orb", menuName = "Spell/Thunder Orb")]
public class ThunderOrb : Spell
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;

    [Header("Thunder Orb Config")]
    [SerializeField] private float cast1_size;
    [SerializeField] private float cast2_size;
    [SerializeField] private float cast3_size;
    [SerializeField] private float duration;
    [SerializeField] private float knockbackGaugeDeal;
    [SerializeField] private float hit_interval_1;
    [SerializeField] private float hit_interval_2;
    [SerializeField] private float hit_interval_3;
    [SerializeField] private float select_duration;
    [SerializeField] private float travel_speed;
    private Vector2 select_position;

    [Header("Reference")]
    [SerializeField] private GameObject thunderOrb_prf;
    [SerializeField] private GameObject direction_prf;
    [SerializeField] private GameObject durationBar_prf;
    public override void BeginCooldown(GameObject player)
    {
    }
    public override void CastSpell(float score, GameObject player)
    {
        base.CastSpell(score, player);
        int castLevel = CalThreshold(score);


        Sequence select_sequence = DOTween.Sequence();
        GameObject dir_prf = Instantiate(direction_prf, DrawCasterUtil.GetMidTransformOf(player.transform));
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
        select_sequence.AppendInterval(select_duration).SetUpdate(true).OnUpdate(() =>
        {
            if (playerAction.Player.LeftClick.IsPressed() && !isClicked)
            {
                playerAction.Player.LeftClick.Disable();
                isClicked = true;
                DOTween.Kill(durationBar);
                Destroy(durationBar);

                Destroy(dir_prf);
                select_position = DrawCasterUtil.GetCurrentMousePosition();
                CastByLevel(castLevel, player, null);

                Debug.Log(DOTween.Kill(this));
            }
        }).OnComplete(() =>
        {
            if (!isClicked)
            {
                Destroy(dir_prf);
                select_position = DrawCasterUtil.GetCurrentMousePosition();
                CastByLevel(castLevel, player, null);
            }

        });
    }
    public override void Cast1(GameObject player, GameObject target)
    {
        GameObject thunderOrb = SpawnThunderOrb(player, _damageSpellLevelMultiplier1, cast1_size, hit_interval_1);
        Vector2 playerPos = DrawCasterUtil.GetMidTransformOf(player.transform).position;
        Vector3 dir = (select_position - playerPos).normalized;

        Rigidbody2D rigidbody2D = thunderOrb.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(Time.fixedDeltaTime * travel_speed * dir, ForceMode2D.Impulse);
    }

    public override void Cast2(GameObject player, GameObject target)
    {
        GameObject thunderOrb = SpawnThunderOrb(player, _damageSpellLevelMultiplier2, cast2_size, hit_interval_2);
        Vector2 playerPos = DrawCasterUtil.GetMidTransformOf(player.transform).position;
        Vector2 dir = (select_position - playerPos).normalized;

        Rigidbody2D rigidbody2D = thunderOrb.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(Time.fixedDeltaTime * travel_speed * dir, ForceMode2D.Impulse);
    }

    public override void Cast3(GameObject player, GameObject target)
    {
        GameObject thunderOrb = SpawnThunderOrb(player, _damageSpellLevelMultiplier3, cast3_size, hit_interval_3);
        Vector2 playerPos = DrawCasterUtil.GetMidTransformOf(player.transform).position;
        Vector2 dir = (select_position - playerPos).normalized;

        Rigidbody2D rigidbody2D = thunderOrb.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(Time.fixedDeltaTime * travel_speed * dir, ForceMode2D.Impulse);
    }

    private GameObject SpawnThunderOrb(GameObject attacker, float multiplier, float size_multiplier, float hit_interval)
    {
        GameObject thunderOrb = Instantiate(thunderOrb_prf, DrawCasterUtil.GetMidTransformOf(attacker.transform).position, Quaternion.identity);
        thunderOrb = DrawCasterUtil.AddAttackHitTo(
            thunderOrb,
            _elementalType, attacker,
            _baseSkillDamageMultiplier * multiplier,
            duration,
            targetLayer,
            knockbackGaugeDeal,
            hit_interval
            );
        Vector3 orginalSize = thunderOrb.transform.localScale;
        Vector2 size = orginalSize * size_multiplier;
        thunderOrb.transform.DOScale(size, 0);
        return thunderOrb;
    }
}
