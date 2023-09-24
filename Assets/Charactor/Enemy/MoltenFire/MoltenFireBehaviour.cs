using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DrawCaster.Util;

namespace MoltenFire
{
    public enum State
    {
        Chase,
        WaitForNextAttack,
        MeleeAttack,
        FlamePillarAttack,
        FireBallAttack
    }
    public class MoltenFireBehaviour : MonoBehaviour
    {
        public State currentState;
        public Rigidbody2D moltenFireRb;
        public string targetTag;
        public Transform target;
        private EnemyData moltenFireData;
        [SerializeField] private Collider2D meleeHitbox;
        [Header("Attack Setting")]
        public float _baseMeleeDamageMultiplier;
        public float _baseFlamePillarDamageMultiplier;
        public float _baseFireBallDamageMultiplier;
        [Header("Attack Prefab")]
        [SerializeField] private GameObject _flamePillarPrefab;


        public delegate void MoltenFireDelegate(Transform target);
        public static MoltenFireDelegate OnChase;
        public static MoltenFireDelegate OnMeleeAttack;
        public static MoltenFireDelegate OnFlamePillarAttack;
        public static MoltenFireDelegate OnWaitForNextAttack;
        private void Awake()
        {
            moltenFireData = GetComponentInParent<CharactorManager<EnemyData>>().GetCharactorData();
            moltenFireRb = GetComponentInParent<Rigidbody2D>();
            animator = transform.parent.GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (target != null)
            {
                switch (currentState)
                {
                    case State.Chase:
                        OnChase?.Invoke(target);
                        break;
                    case State.MeleeAttack:
                        OnMeleeAttack?.Invoke(target);
                        currentState = State.WaitForNextAttack;
                        break;
                    case State.FlamePillarAttack:
                        OnFlamePillarAttack?.Invoke(target);
                        currentState = State.WaitForNextAttack;
                        break;
                    case State.FireBallAttack:
                        OnMeleeAttack?.Invoke(target);
                        currentState = State.WaitForNextAttack;
                        break;
                    case State.WaitForNextAttack:
                        OnWaitForNextAttack?.Invoke(target);
                        break;
                    default:
                        break;
                }
            }

        }
        ////////////////////////// Animation event /////////////////////////
        void MoveToChaseTarget()
        {
            Vector2 direction = (Vector2)target.position - moltenFireRb.position;
            moltenFireRb.DOMove(moltenFireRb.position + direction.normalized * moltenFireData._moveSpeed, 0.3f);

        }
        void MeleeAttack()
        {
            List<Collider2D> overlappingColliders = new();
            Physics2D.OverlapCollider(meleeHitbox, new ContactFilter2D(), overlappingColliders);
            foreach (Collider2D overlapCol in overlappingColliders)
            {
                Debug.Log(overlappingColliders.Count);

                Transform parent = overlapCol.transform.root;
                IDamageable damageable = parent.GetComponent<IDamageable>();
                if (damageable != null && parent.CompareTag(targetTag))
                {
                    Elemental damage = Elemental.DamageCalculation(moltenFireData.elementalType, moltenFireData, _baseMeleeDamageMultiplier,moltenFireData.targetLayer);
                    damageable.TakeDamage(damage);
                }
            }
        }
        public AnimationClip FlamePillarClip;
        void FlamePillarAttack()
        {
            Transform lowerTransform = DrawCasterUtil.GetLowerTransformOf(transform.root);
            GameObject flamePillar = Instantiate(_flamePillarPrefab);
            flamePillar.transform.position = lowerTransform.position;
            flamePillar = DrawCasterUtil.AddAttackHitTo(flamePillar,
                moltenFireData.elementalType, moltenFireData,
                _baseFlamePillarDamageMultiplier,
                FlamePillarClip.length,
                moltenFireData.targetLayer
                );
        }
        ///////////////////////////////////////////////////////////////////

        private MoltenFireDelegate ConsiderAttackType(Transform target)
        {
            return (target) => MeleeAttack();
        }

        [SerializeField] private Animator animator;
        private void OnEnable()
        {

            OnChase += target => animator.SetBool("IsWalk", true);
            OnMeleeAttack += target =>
            {
                animator.SetTrigger("MeleeAttack");
                animator.SetBool("IsWalk", false);
            };
            OnFlamePillarAttack += target =>
            {
                animator.SetTrigger("FlamePillarAttack");
                animator.SetBool("IsWalk", false);
            };
        }
        private void OnDisable()
        {
        }
    }
}

