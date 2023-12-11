using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;

[CreateAssetMenu(fileName = "new Flame Pillars", menuName = "Spell/Flame Pillars")]
public class FlamePillars : Spell
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;

    [Header("Flame Pillar Config")]
    [SerializeField] private int cast1_amount;
    [SerializeField] private int cast2_amount;
    [SerializeField] private int cast3_amount;
    [SerializeField] private float random_radius;
    [SerializeField] private float _delayTime;
    [SerializeField] private float knockbackGaugeDeal;
    [SerializeField] private float hit_interval;
    [SerializeField] private float screenShakePower;

    [Header("Reference")]
    [SerializeField] private GameObject flamePillar_prf;
    [SerializeField] private AnimationClip flamePillar_clip;
    public override void BeginCooldown(GameObject player)
    {
    }
    public override void CastSpell(float score, GameObject player)
    {
        base.CastSpell(score, player);
        int castLevel = CalThreshold(score);

        Sequence sequence = DOTween.Sequence();

        int amount = GetAmount(castLevel);
        for (int i = 0; i < amount; i++)
        {
            sequence.AppendCallback(() =>
                 {
                    ShakeScreen();
                    CastByLevel(castLevel, player, null);
                 });
            sequence.AppendInterval(_delayTime);
        }


        sequence.Play();
    }
    public override void Cast1(GameObject player, GameObject target)
    {
        Transform player_transform = DrawCasterUtil.GetMidTransformOf(player.transform);

        Vector3 position = DrawCasterUtil.RandomPosition(player_transform.position, random_radius);
        SpawnFlamePillar(position, Quaternion.identity, player, _damageSpellLevelMultiplier1);
    }

    public override void Cast2(GameObject player, GameObject target)
    {
        Transform player_transform = DrawCasterUtil.GetMidTransformOf(player.transform);

        Vector3 position = DrawCasterUtil.RandomPosition(player_transform.position, random_radius);
        SpawnFlamePillar(position, Quaternion.identity, player, _damageSpellLevelMultiplier2);
    }

    public override void Cast3(GameObject player, GameObject target)
    {
        Transform player_transform = DrawCasterUtil.GetMidTransformOf(player.transform);

        Vector3 position = DrawCasterUtil.RandomPosition(player_transform.position, random_radius);
        SpawnFlamePillar(position, Quaternion.identity, player, _damageSpellLevelMultiplier3);
    }

    private GameObject SpawnFlamePillar(Vector3 position, Quaternion quaternion, GameObject attacker, float multiplier)
    {
        GameObject flamePillar = Instantiate(flamePillar_prf, position, quaternion);
        flamePillar = DrawCasterUtil.AddAttackHitTo(
            flamePillar,
            _elementalType, attacker,
            _baseSkillDamageMultiplier * multiplier,
            flamePillar_clip.length,
            targetLayer,
            knockbackGaugeDeal,
            hit_interval
            );
            
        return flamePillar;
    }

    private void ShakeScreen()
    {
        ScreenShakeManager.Instance.Shake(0.2f,screenShakePower);
    }
    public int GetAmount(int castLevel)
    {
        if (castLevel == 1)
        {
            return cast1_amount;
        }
        else if (castLevel == 2)
        {
            return cast2_amount;
        }
        return cast3_amount;
    }
}
