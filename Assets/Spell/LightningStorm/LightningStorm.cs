using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_LightningStorm", menuName = "Spell/Lightningstorm")]
public class LightningStorm : Spell
{
    [Header("Damage Multiplier")]
    [SerializeField] private float _baseSkillDamageMultiplier;
    [SerializeField] private float _damageSpellLevelMultiplier1;
    [SerializeField] private float _damageSpellLevelMultiplier2;
    [SerializeField] private float _damageSpellLevelMultiplier3;
    [SerializeField] private GameObject _lightningStormPrefab;
    public override void Cast1(GameObject player, GameObject target)
    {
        base.Cast1(player, target);
        Vector2 spawnPos = target.transform.position;
        float offSet = 2f;
        GameObject lightningStorm = Instantiate(_lightningStormPrefab, new Vector3(spawnPos.x, spawnPos.y + offSet, 0f), Quaternion.identity);
        AttackHit attackHit = lightningStorm.transform.GetComponentInChildren<AttackHit>();
        attackHit.elementalDamage =
        Elemental.DamageCalculation(_elementalType,
                        player.GetComponent<PlayerManager>().playerData,
                        _baseSkillDamageMultiplier*_damageSpellLevelMultiplier1);
        Debug.Log(attackHit.elementalDamage._damage + ", " + attackHit.elementalDamage._elementalType);
        Debug.Log("Cast1");
        // เล่นsound
    }
    public override void Cast2(GameObject player, GameObject target)
    {
        base.Cast2(player, target);
        Vector2 spawnPos = target.transform.position;
        float offSet = 2f;
        GameObject lightningStorm = Instantiate(_lightningStormPrefab, new Vector3(spawnPos.x, spawnPos.y + offSet, 0f), Quaternion.identity);
        AttackHit attackHit = lightningStorm.transform.GetComponentInChildren<AttackHit>();
        attackHit.elementalDamage =
        Elemental.DamageCalculation(_elementalType,
                        player.GetComponent<PlayerManager>().playerData,
                        _baseSkillDamageMultiplier*_damageSpellLevelMultiplier2);
        Debug.Log(attackHit.elementalDamage._damage + ", " + attackHit.elementalDamage._elementalType);
        Debug.Log("Cast2");
        // เล่นsound
    }
    public override void Cast3(GameObject player, GameObject target)
    {
        base.Cast3(player, target);
        Vector2 spawnPos = target.transform.position;
        float offSet = 2f;
        GameObject lightningStorm = Instantiate(_lightningStormPrefab, new Vector3(spawnPos.x, spawnPos.y + offSet, 0f), Quaternion.identity);
        AttackHit attackHit = lightningStorm.transform.GetComponentInChildren<AttackHit>();
        attackHit.elementalDamage =
        Elemental.DamageCalculation(_elementalType,
                        player.GetComponent<PlayerManager>().playerData,
                        _baseSkillDamageMultiplier*_damageSpellLevelMultiplier3);
        Debug.Log(attackHit.elementalDamage._damage + ", " + attackHit.elementalDamage._elementalType);
        Debug.Log("Cast3");
        // เล่นsound
    }
    public override void BeginCooldown(GameObject gameObject)
    {
        base.BeginCooldown(gameObject);
    }


}