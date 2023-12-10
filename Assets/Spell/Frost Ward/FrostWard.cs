using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;

[CreateAssetMenu(fileName = "new Frost Ward", menuName = "Spell/Frost Ward")]
public class FrostWard : Spell
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;

    [Header("Frost Ward Config")]
    [SerializeField] private float cast1_radius;
    [SerializeField] private float cast2_radius;
    [SerializeField] private float cast3_radius;
    [SerializeField] private float duration;
    [SerializeField] private float knockbackGaugeDeal;
    [SerializeField] private float hit_interval;

    [Header("Reference")]
    [SerializeField] private GameObject frostWard_prf;
    public override void BeginCooldown(GameObject player)
    {
    }
    public override void CastSpell(float score, GameObject player)
    {
        base.CastSpell(score, player);
        int castLevel = CalThreshold(score);


        CastByLevel(castLevel, player, null);


    }
    public override void Cast1(GameObject player, GameObject target)
    {

        SpawnFrostWard(player, _damageSpellLevelMultiplier1, cast1_radius);
    }

    public override void Cast2(GameObject player, GameObject target)
    {
        SpawnFrostWard(player, _damageSpellLevelMultiplier2, cast2_radius);

    }

    public override void Cast3(GameObject player, GameObject target)
    {
        SpawnFrostWard(player, _damageSpellLevelMultiplier3, cast3_radius);

    }

    private GameObject SpawnFrostWard(GameObject attacker, float multiplier, float radius)
    {
        GameObject frostward = Instantiate(frostWard_prf, DrawCasterUtil.GetLowerTransformOf(attacker.transform));
        frostward = DrawCasterUtil.AddAttackHitTo(
            frostward,
            _elementalType, attacker,
            _baseSkillDamageMultiplier * multiplier,
            duration,
            targetLayer,
            knockbackGaugeDeal,
            hit_interval
            );
        frostward.transform.DOScale(radius, 0);
        return frostward;
    }
}
