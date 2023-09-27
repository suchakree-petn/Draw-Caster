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


        public delegate void MoltenFireDelegate(Transform target);
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
                        OnFireBallAttack?.Invoke(target);
                        currentState = State.WaitForNextAttack;
                        break;
                    case State.WaitForNextAttack:
                        OnWaitForNextAttack?.Invoke(target);
                        break;
                    default:
                        break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireBallAttack();
            }
        }
        ////////////////////////// Animation event /////////////////////////
        public static MoltenFireDelegate OnChase;

        void MoveToChaseTarget()
        {
            Vector2 direction = (Vector2)target.position - moltenFireRb.position;
            moltenFireRb.DOMove(moltenFireRb.position + direction.normalized * moltenFireData._moveSpeed, 0.3f);

        }
        [Header("Melee Attack")]
        public float _baseMeleeDamageMultiplier;
        [SerializeField] private float meleeKnockbackGaugeDeal;
        [SerializeField] private Collider2D meleeHitbox;
        public static MoltenFireDelegate OnMeleeAttack;
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
                    Elemental damage = Elemental.DamageCalculation(moltenFireData.elementalType, moltenFireData, _baseMeleeDamageMultiplier, moltenFireData.targetLayer, meleeKnockbackGaugeDeal);
                    damageable.TakeDamage(damage);
                }
            }
        }

        [Header("Fire Pillar Attack")]
        public float _baseFlamePillarDamageMultiplier;
        [SerializeField] private float firePillarKnockbackGaugeDeal;
        [SerializeField] private GameObject _flamePillarPrefab;
        public AnimationClip FlamePillarClip;
        public static MoltenFireDelegate OnFlamePillarAttack;

        void FlamePillarAttack()
        {
            Transform lowerTransform = DrawCasterUtil.GetLowerTransformOf(transform.root);
            GameObject flamePillar = Instantiate(_flamePillarPrefab);
            flamePillar.transform.position = lowerTransform.position;
            flamePillar = DrawCasterUtil.AddAttackHitTo(flamePillar,
                moltenFireData.elementalType, moltenFireData,
                _baseFlamePillarDamageMultiplier,
                FlamePillarClip.length,
                moltenFireData.targetLayer,
                firePillarKnockbackGaugeDeal
                );
        }

        [Header("Fire Ball Attack")]
        public float _baseFireBallDamageMultiplier;
        [SerializeField] private float fireBallKnockbackGaugeDeal;
        [SerializeField] private float launchDuration;
        [SerializeField] private float curveDuration;
        [SerializeField] private float spawnInterval;
        [SerializeField] private GameObject fireballPrefab;
        [SerializeField] private Transform spawnPosLeft;
        [SerializeField] private Transform midCurveLeft;
        [SerializeField] private Transform spawnPosRight;
        [SerializeField] private Transform midCurveRight;
        [SerializeField] private int fireBallAmount;
        [SerializeField] private AnimationCurve speedCurve;

        public static MoltenFireDelegate OnFireBallAttack;
        void FireBallAttack()
        {
            Sequence sequence = DOTween.Sequence();

            for (int i = 0; i < fireBallAmount; i++)
            {
                Vector3 spawnPos = GetNewSpawnPos(i).position;
                Transform fireBallAttack = DrawCasterUtil.AddAttackHitTo(
                    Instantiate(fireballPrefab, spawnPos, Quaternion.identity),
                    moltenFireData.elementalType,
                    moltenFireData,
                    _baseFireBallDamageMultiplier,
                    (launchDuration + curveDuration + spawnInterval) * i + 1,
                    moltenFireData.targetLayer,
                    fireBallKnockbackGaugeDeal
                    ).transform;
                int index = i;
                sequence.AppendCallback(() =>
                {
                    Move(fireBallAttack, index);
                });
                sequence.AppendInterval(spawnInterval);
            }
        }
        void Move(Transform fireBallAttack, int i)
        {
            fireBallAttack.DOPath(
                        new Vector3[] { fireBallAttack.position, GetNewMidCurve(i).position, target.position },
                        curveDuration + launchDuration,
                        PathType.CatmullRom).SetEase(speedCurve)
                        .OnComplete(() =>
                        {
                            Destroy(fireBallAttack.gameObject);
                        });
        }
        Transform GetNewSpawnPos(int index)
        {
            if (index % 2 == 0)
            {
                return spawnPosRight;
            }
            return spawnPosLeft;
        }
        Transform GetNewMidCurve(int index)
        {
            if (index % 2 == 0)
            {
                return midCurveRight;
            }
            return midCurveLeft;
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
            OnFireBallAttack += target =>
            {
                animator.SetTrigger("FireBallAttack");
                animator.SetBool("IsWalk", false);
            };
        }
        private void OnDisable()
        {
        }
    }
}

