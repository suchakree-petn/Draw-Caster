using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DrawCaster.Util
{
    public class DrawCasterUtil
    {
        public static GameObject AddAttackHitTo(GameObject parent,
            ElementalType elementalType,
            GameObject attacker,
            float baseSkillMultiplier,
            float selfDestructTime,
            LayerMask targetLayer,
            float knockbackGaugeDeal,
            float iFrameTime
            )
        {
            GameObject addedParent = parent;
            addedParent.AddComponent(typeof(AttackHit));
            AttackHit attackHit = addedParent.GetComponent<AttackHit>();
            attackHit.elementalDamage = Elemental.DamageCalculation(elementalType, attacker, baseSkillMultiplier, targetLayer, knockbackGaugeDeal);
            attackHit.selfDestructTime = selfDestructTime;
            attackHit.iFrameTime = iFrameTime;
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
        public static Vector2 GetDirectionFromUpper(Transform origin, Transform target)
        {
            return GetUpperTransformOf(target).position - GetUpperTransformOf(origin).position;
        }
        public static Vector2 GetDirectionFromMid(Transform origin, Transform target)
        {
            return GetMidTransformOf(target).position - GetMidTransformOf(origin).position;
        }
        public static Vector2 GetDirectionFromLower(Transform origin, Transform target)
        {
            return GetLowerTransformOf(target).position - GetLowerTransformOf(origin).position;
        }
    }
    public class Timer
    {
        public float startTime;
        public float endTime;
        public Timer(float startTime, float endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            Sequence timer = DOTween.Sequence();
            timer.AppendInterval(endTime - startTime).SetUpdate(true)
            .OnUpdate(() =>
            {
                this.startTime += Time.deltaTime;
            })
            .OnComplete(() => timer.Kill());
        }
        public bool IsFinished()
        {
            return startTime >= endTime;
        }
    }
    public class WeightedRandom<T>
    {
        private List<WeightedItem<T>> weightedItems = new List<WeightedItem<T>>();

        public void AddItem(T item, float weight)
        {
            weightedItems.Add(new WeightedItem<T> { Item = item, Weight = weight });
        }

        public T GetRandom()
        {
            float totalWeight = 0;
            foreach (var item in weightedItems)
            {
                totalWeight += item.Weight;
            }

            float randomValue = Random.Range(0, totalWeight);

            foreach (var item in weightedItems)
            {
                if (randomValue < item.Weight)
                {
                    return item.Item;
                }
                randomValue -= item.Weight;
            }

            // Fallback: If something went wrong, return the last item.
            return weightedItems[weightedItems.Count - 1].Item;
        }
    }

    public struct WeightedItem<T>
    {
        public T Item { get; set; }
        public float Weight { get; set; }
    }
}


