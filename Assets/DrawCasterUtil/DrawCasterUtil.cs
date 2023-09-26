using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DrawCaster.Util
{
    public class DrawCasterUtil
    {

        public static GameObject AddAttackHitTo(GameObject parent, ElementalType elementalType, CharactorData attacker, float baseSkillMultiplier, float selfDestructTime, LayerMask targetLayer)
        {
            GameObject addedParent = parent;
            addedParent.AddComponent(typeof(AttackHit));
            AttackHit attackHit = addedParent.GetComponent<AttackHit>();
            attackHit.elementalDamage = Elemental.DamageCalculation(elementalType, attacker, baseSkillMultiplier, targetLayer);
            attackHit.selfDestructTime = selfDestructTime;
            return addedParent;
        }

        public static Transform GetUpperTransformOf(Transform parent)
        {
            Transform parentTransform = parent.GetChild(0);
            if (parentTransform.name != "Transforms")
            {
                Debug.LogWarning("Error name or position order!!");
                return null;
            }
            return parentTransform.GetChild(0);
        }
        public static Transform GetMidTransformOf(Transform parent)
        {
            Transform parentTransform = parent.GetChild(0);
            if (parentTransform.name != "Transforms")
            {
                Debug.LogWarning("Error name or position order!!");
                return null;
            }
            return parentTransform.GetChild(1);
        }
        public static Transform GetLowerTransformOf(Transform parent)
        {
            Transform parentTransform = parent.GetChild(0);
            if (parentTransform.name != "Transforms")
            {
                Debug.LogWarning("Error name or position order!!");
                return null;
            }
            return parentTransform.GetChild(2);
        }
        public static Vector2 RandomPosition(Vector2 center, float randomPositionRadius)
        {
            // Generate a random angle in radians
            float randomAngle = Random.Range(0f, 2 * Mathf.PI);

            // Calculate a random position within the spawn radius
            Vector3 spawnPosition = center + new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * Random.Range(0, randomPositionRadius);

            return spawnPosition;
        }
        public static Vector2 GetCurrentMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}

