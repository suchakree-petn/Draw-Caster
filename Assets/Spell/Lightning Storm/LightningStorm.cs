using DG.Tweening;
using System.Collections.Generic;
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
    public int _amountLevel1;
    public int _amountLevel2;
    public int _amountLevel3;
    [SerializeField] private GameObject lightningStormPrefab;

    public override void CastSpell(float score, GameObject player)
    {

        int castLevel = CalThreshold(score);
        int amount = GetAmount(castLevel);
        GameObject[] enemyList = GameController.Instance.GetAllEnemyInScene();
        while (amount > enemyList.Length && enemyList.Length > 0)
        {
            enemyList = enemyList.Concat(GameController.Instance.GetAllEnemyInScene()).ToArray();
        }
        var sequence = DOTween.Sequence();
        List<GameObject> targetList = new();
        for (int i = 0; i < amount; i++)
        {
            GameObject target = null;

            if (enemyList.Length > 0)
            {
                target = enemyList[i];
            }

            targetList.Add(target);
        }
        foreach (var target in targetList)
        {
            sequence.AppendCallback(() => CastByLevel(castLevel, player, target)).AppendInterval(_delayTime);
        }
        sequence.OnComplete(() => BeginCooldown(player));
    }

    public override void Cast1(GameObject player, GameObject target)
    {
        Debug.Log("Cast1");

        if (player == null) { return; }
        Vector2 spawnPos = new Vector2();
        if (target != null)
        {
            spawnPos = target.transform.position;
        }
        else
        {
            spawnPos = RandomTransform(player.transform).transform.position;
        }
        GameObject lightningStorm = SpawnLightning(player, spawnPos);
        CinemachineShake.Instance.Shake(target);
    }



    public override void Cast2(GameObject player, GameObject target)
    {
        Debug.Log("Cast2");

        if (player == null) { return; }
        Vector2 spawnPos = new Vector2();
        if (target != null)
        {
            spawnPos = target.transform.position;
        }
        else
        {
            spawnPos = RandomTransform(player.transform).transform.position;
        }
        GameObject lightningStorm = SpawnLightning(player, spawnPos);
        CinemachineShake.Instance.Shake(target);
    }
    public override void Cast3(GameObject player, GameObject target)
    {
        Debug.Log("Cast3");

        if (player == null) { return; }
        Vector2 spawnPos = new Vector2();
        if (target != null)
        {
            spawnPos = target.transform.position;
        }
        else
        {
            spawnPos = RandomTransform(player.transform).transform.position;
        }
        GameObject lightningStorm = SpawnLightning(player, spawnPos);
        CinemachineShake.Instance.Shake(target);
    }
    private GameObject SpawnLightning(GameObject player, Vector2 spawnPos)
    {
        GameObject lightningStorm = Instantiate(lightningStormPrefab, new Vector3(spawnPos.x, spawnPos.y, 0f), Quaternion.identity);
        AttackHit attackHit = lightningStorm.transform.GetComponentInChildren<AttackHit>();
        attackHit.elementalDamage = Elemental.DamageCalculation(_elementalType,
                        player.GetComponent<PlayerManager>().playerData,
                        _baseSkillDamageMultiplier * _damageSpellLevelMultiplier1);
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
    public override void BeginCooldown(GameObject player)
    {
        
    }
    private GameObject RandomTransform(Transform center)
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, Mathf.PI * 2);

        // Calculate a random position within the spawn radius
        Vector3 spawnPosition = center.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f) * 5f;

        GameObject obj = new GameObject();
        obj.AddComponent<AttackHit>();
        obj.GetComponent<AttackHit>().selfDestructTime = 0.1f;
        GameObject randomTransform = Instantiate(obj, spawnPosition, Quaternion.identity);

        return randomTransform;
    }
}
