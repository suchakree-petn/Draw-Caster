using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawCaster.Util
{
    public class DrawCasterUtil
    {

        public static GameObject AddAttackHitTo(GameObject parent, ElementalType elementalType, CharactorData attacker, float baseSkillMultiplier, float selfDestructTime,LayerMask targetLayer)
        {
            GameObject addedParent = parent;
            addedParent.AddComponent(typeof(AttackHit));
            AttackHit attackHit = addedParent.GetComponent<AttackHit>();
            attackHit.elementalDamage = Elemental.DamageCalculation(elementalType, attacker, baseSkillMultiplier,targetLayer);
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
    }
}

