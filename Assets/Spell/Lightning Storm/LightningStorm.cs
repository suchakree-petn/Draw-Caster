using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightningStorm : Spell
{
    [SerializeField] private LightningStormObj lightningStormObj;

    private void Awake()
    {
        spellObj = lightningStormObj;
    }
    public override void CastSpell(float score)
    {
        
        int castLevel = CalThreshold(score);
        float delay = lightningStormObj._delayTime;
        int amount = GetAmount(castLevel);

        GameObject[] enemyList = GameController.Instance.GetAllEnemyInScene();
        StartCoroutine(RepeatCast(castLevel, delay, amount, enemyList));

        _isReadyToCast = false;
        StartCoroutine(Cooldown(lightningStormObj));
    }
    
    public override void Cast1(GameObject player, GameObject target)
    {
        if (player == null && target == null) { return; }

        Vector2 spawnPos = target.transform.position;
        float offSet = 0f;
        GameObject lightningStorm = Instantiate(lightningStormObj._lightningStormPrefab, new Vector3(spawnPos.x, spawnPos.y + offSet, 0f), Quaternion.identity);
        AttackHit attackHit = lightningStorm.transform.GetComponentInChildren<AttackHit>();
        attackHit.elementalDamage =
        Elemental.DamageCalculation(lightningStormObj._elementalType,
                        player.GetComponent<PlayerManager>().playerData,
                        lightningStormObj._baseSkillDamageMultiplier * lightningStormObj._damageSpellLevelMultiplier1);
        Debug.Log(attackHit.elementalDamage._damage + ", " + attackHit.elementalDamage._elementalType);
        Debug.Log("Cast1");
        CinemachineShake.Instance.Shake(target);
        // เล่นsound
    }
    public override void Cast2(GameObject player, GameObject target)
    {
        if (player == null && target == null) { return; }

        Vector2 spawnPos = target.transform.position;
        float offSet = 0f;
        GameObject lightningStorm = Instantiate(lightningStormObj._lightningStormPrefab, new Vector3(spawnPos.x, spawnPos.y + offSet, 0f), Quaternion.identity);
        AttackHit attackHit = lightningStorm.transform.GetComponentInChildren<AttackHit>();
        attackHit.elementalDamage =
        Elemental.DamageCalculation(lightningStormObj._elementalType,
                        player.GetComponent<PlayerManager>().playerData,
                        lightningStormObj._baseSkillDamageMultiplier * lightningStormObj._damageSpellLevelMultiplier1);
        Debug.Log(attackHit.elementalDamage._damage + ", " + attackHit.elementalDamage._elementalType);
        Debug.Log("Cast2");
        CinemachineShake.Instance.Shake(target);
        // เล่นsound
    }
    public override void Cast3(GameObject player, GameObject target)
    {
        if (player == null && target == null) { return; }

        Vector2 spawnPos = target.transform.position;
        float offSet = 0f;
        GameObject lightningStorm = Instantiate(lightningStormObj._lightningStormPrefab, new Vector3(spawnPos.x, spawnPos.y + offSet, 0f), Quaternion.identity);
        AttackHit attackHit = lightningStorm.transform.GetComponentInChildren<AttackHit>();
        attackHit.elementalDamage =
        Elemental.DamageCalculation(lightningStormObj._elementalType,
                        player.GetComponent<PlayerManager>().playerData,
                        lightningStormObj._baseSkillDamageMultiplier * lightningStormObj._damageSpellLevelMultiplier1);
        Debug.Log(attackHit.elementalDamage._damage + ", " + attackHit.elementalDamage._elementalType);
        Debug.Log("Cast3");
        CinemachineShake.Instance.Shake(target);
        // เล่นsound
    }
    public int GetAmount(int castLevel)
    {
        if (castLevel == 1)
        {
            return lightningStormObj._amountLevel1;
        }
        else if (castLevel == 2)
        {
            return lightningStormObj._amountLevel2;
        }
        return lightningStormObj._amountLevel3;
    }
    public override void BeginCooldown(GameObject gameObject)
    {
        base.BeginCooldown(gameObject);
    }
    private GameObject RandomTransform()
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, Mathf.PI * 2);

        // Calculate a random position within the spawn radius
        Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f) * 5f;

        GameObject obj = new GameObject();
        obj.AddComponent<AttackHit>();
        obj.GetComponent<AttackHit>().selfDestructTime = 0.1f;
        GameObject randomTransform = Instantiate(obj, spawnPosition, Quaternion.identity);

        return randomTransform;
    }
    IEnumerator RepeatCast(int castLevel, float delay, int amount, GameObject[] enemyList)
    {
        if (amount == 0)
        {
            yield break;
        }
        GameObject[] _enemyList = enemyList;
        GameObject enemyTarget = null;
        int _amount = amount;
        if (GameController.Instance.GetAllEnemyInScene().Length == 0)
        {
            enemyTarget = RandomTransform();
        }
        else if (enemyList.Length == 0)
        {
            _enemyList = GameController.Instance.GetAllEnemyInScene();
            enemyTarget = _enemyList[Random.Range(0, _enemyList.Length)];
        }
        else
        {
            enemyTarget = _enemyList[Random.Range(0, _enemyList.Length)];
        }

        switch (castLevel)
        {
            case 1:
                Cast1(transform.root.gameObject, enemyTarget);
                break;
            case 2:
                Cast2(transform.root.gameObject, enemyTarget);
                break;
            case 3:
                Cast3(transform.root.gameObject, enemyTarget);
                break;
            default:
                Debug.Log("ERROR!! Cast Level More Than 3");
                break;
        }

        _amount--;
        List<GameObject> temp = _enemyList.ToList();
        temp.Remove(enemyTarget);
        _enemyList = temp.ToArray();
        BeginCooldown(transform.root.gameObject);
        yield return new WaitForSeconds(delay);
        StartCoroutine(RepeatCast(castLevel, delay, _amount, _enemyList));
    }
}
