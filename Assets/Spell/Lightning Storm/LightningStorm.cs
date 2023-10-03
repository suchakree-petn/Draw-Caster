using DG.Tweening;
using DrawCaster.Util;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "new Lightning Storm", menuName = "Spell/Lightning Storm")]
public class LightningStorm : Spell
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;

    [Header("Lighning Storm Setting")]
    public float _delayTime;
    public float knockbackGaugeDeal;
    public int _amountLevel1;
    public int _amountLevel2;
    public int _amountLevel3;
    public float selfDestructTime;
    public float spellRadius;
    public float randomPositionRadiusSuccess;
    public float randomPositionRadius;
    [SerializeField] private GameObject lightningStormPrefab;
    public override void CastSpell(float score, GameObject player)
    {
        base.CastSpell(score,player);
        int castLevel = CalThreshold(score);
        GameObject[] enemyList = GameController.Instance.AllEnemy;
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (!IsEnemyInLightningStormRadius(enemyList[i].transform, player.transform))
            {
                enemyList[i] = null;
            }
        }
        int amount = GetAmount(castLevel);

        if (enemyList.Length == 0)
        {
            enemyList = new GameObject[amount];
            for (int i = 0; i < enemyList.Length; i++)
            {
                enemyList[i] = null;
            }
        }

        Sequence sequence = DOTween.Sequence();
        GameObject[] originalEnemyList = enemyList;
        for (int i = 0; i < amount; i++)
        {
            if (i > enemyList.Length - 1)
            {
                enemyList = enemyList.ToList().Concat(originalEnemyList).ToArray();
            }
        }
        foreach (GameObject enemy in enemyList)
        {
            sequence.AppendCallback(() =>
                {
                    CastByLevel(castLevel, player, enemy);
                });
            sequence.AppendInterval(_delayTime);
        }
        sequence.Play();
    }

    public override void Cast1(GameObject player, GameObject target)
    {
        Debug.Log("Cast1");

        if (player == null) { return; }
        Vector2 spawnPos;
        if (target != null)
        {
            spawnPos = DrawCasterUtil.GetLowerTransformOf(target.transform).position;
            spawnPos = DrawCasterUtil.RandomPosition(spawnPos, randomPositionRadiusSuccess);
        }
        else
        {
            spawnPos = DrawCasterUtil.RandomPosition(player.transform.position, randomPositionRadius);
        }
        SpawnLightning(player, spawnPos, _damageSpellLevelMultiplier1);
        //CinemachineShake.Instance.Shake(target);
    }



    public override void Cast2(GameObject player, GameObject target)
    {
        if (player == null) { return; }
        Vector2 spawnPos;
        if (target != null)
        {
            spawnPos = DrawCasterUtil.GetLowerTransformOf(target.transform).position;
            spawnPos = DrawCasterUtil.RandomPosition(spawnPos, randomPositionRadiusSuccess);
        }
        else
        {
            spawnPos = DrawCasterUtil.RandomPosition(player.transform.position, randomPositionRadius);
        }
        SpawnLightning(player, spawnPos, _damageSpellLevelMultiplier2);
        //CinemachineShake.Instance.Shake(target);
    }
    public override void Cast3(GameObject player, GameObject target)
    {
        Debug.Log("Cast3");

        if (player == null) { return; }
        Vector2 spawnPos;
        if (target != null)
        {
            spawnPos = DrawCasterUtil.GetLowerTransformOf(target.transform).position;
            spawnPos = DrawCasterUtil.RandomPosition(spawnPos, randomPositionRadiusSuccess);
        }
        else
        {
            spawnPos = DrawCasterUtil.RandomPosition(player.transform.position, randomPositionRadius);
        }
        SpawnLightning(player, spawnPos, _damageSpellLevelMultiplier3);
        //CinemachineShake.Instance.Shake(target);
    }
    private GameObject SpawnLightning(GameObject player, Vector2 spawnPos, float multiplier)
    {
        GameObject lightningStorm = DrawCasterUtil.AddAttackHitTo(
            Instantiate(lightningStormPrefab, new Vector3(spawnPos.x, spawnPos.y, 0f), Quaternion.identity),
            _elementalType,
            player,
            _baseSkillDamageMultiplier * multiplier,
            selfDestructTime,
            targetLayer,
            knockbackGaugeDeal
            );
        return lightningStorm;
    }
    public int GetAmount(int castLevel)
    {
        if (castLevel == 1)
        {
            return _amountLevel1;
        }
        else if (castLevel == 2)
        {
            return _amountLevel2;
        }
        return _amountLevel3;
    }
    bool IsEnemyInLightningStormRadius(Transform enemyTransform, Transform player)
    {
        return Vector2.Distance(player.position, enemyTransform.position) <= spellRadius;
    }
    public override void BeginCooldown(GameObject player)
    {

    }

}
