
using UnityEngine;
using DG.Tweening;
using DrawCaster.Util;

namespace MoltenFire
{
    public enum State
    {
        Wandering,
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
                    case State.Wandering:
                        OnWandering?.Invoke(target);
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
        [Header("Wander State")]
        [SerializeField] private float wanderDistance;
        int isForward;
        private void Wandering(Transform target)
        {
            Vector3 targetPos = DrawCasterUtil.GetMidTransformOf(target).position;
            Vector3 moltenFirePos = DrawCasterUtil.GetMidTransformOf(transform.root).position;
            float distant = Vector3.Distance(targetPos, moltenFirePos);
            if (distant > wanderDistance)
            {
                isForward = 1;
            }
            else
            {
                isForward = -1;
            }
        }
        public static MoltenFireDelegate OnWandering;

        void MoveToChaseTarget()
        {
            Vector2 direction = DrawCasterUtil.GetMidTransformOf(target).position - DrawCasterUtil.GetMidTransformOf(transform.root).position;
            direction *= isForward;
            moltenFireRb.DOMove(moltenFireRb.position + direction.normalized * moltenFireData._moveSpeed, 0.3f);

        }
        [Header("Melee Attack")]
        public float _baseMeleeDamageMultiplier;
        public float attackRange;
        [SerializeField] private float meleeKnockbackGaugeDeal;
        [SerializeField] private Collider2D meleeHitbox;
        public static MoltenFireDelegate OnMeleeAttack;
        void MeleeAttack()
        {
            RaycastHit2D[] overlappingColliders = Physics2D.BoxCastAll(
                meleeHitbox.bounds.center,
                meleeHitbox.bounds.size,
                0f,
                -moltenFireRb.transform.right,
                attackRange, moltenFireData.targetLayer
                );
            foreach (RaycastHit2D overlapCol in overlappingColliders)
            {
                if (overlapCol.collider.tag == "Hitbox")
                {
                    Transform parent = overlapCol.transform.root;
                    IDamageable damageable = parent.GetComponent<IDamageable>();
                    if (damageable != null && overlapCol.transform.root.CompareTag(targetTag))
                    {
                        Elemental damage = Elemental.DamageCalculation(moltenFireData.elementalType, transform.root.gameObject, _baseMeleeDamageMultiplier, moltenFireData.targetLayer, meleeKnockbackGaugeDeal);
                        damageable.TakeDamage(damage);
                    }
                }
            }
        }

        [Header("Flame Pillar Attack")]
        public float _baseFlamePillarDamageMultiplier;
        [SerializeField] private float firePillarKnockbackGaugeDeal;
        [SerializeField] private GameObject _flamePillarPrefab;
        public AnimationClip FlamePillarClip;
        [SerializeField] private Transform flamePillarCol;
        public static MoltenFireDelegate OnFlamePillarAttack;

        void FlamePillarAttack()
        {
            Transform lowerTransform = DrawCasterUtil.GetLowerTransformOf(transform.root);
            GameObject flamePillar = Instantiate(_flamePillarPrefab);
            flamePillar.transform.position = lowerTransform.position;
            flamePillar = DrawCasterUtil.AddAttackHitTo(flamePillar,
                moltenFireData.elementalType, transform.root.gameObject,
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
        [SerializeField] private Transform fireBallCol;

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
                    transform.root.gameObject,
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

        [Header("Attack Check")]
        [SerializeField] private float MeleeAttackWeight;
        [SerializeField] private float FlamePillarAttackWeight;
        [SerializeField] private float FireBallAttackWeight;
        private void RandomAttackType(Transform target)
        {
            WeightedRandom<Transform> randomCheckCol = new();
            randomCheckCol.AddItem(meleeHitbox.transform, MeleeAttackWeight);
            randomCheckCol.AddItem(flamePillarCol, FlamePillarAttackWeight);
            randomCheckCol.AddItem(fireBallCol, FireBallAttackWeight);
            randomCheckCol.GetRandom().gameObject.SetActive(true);
        }
        


        [SerializeField] private Animator animator;
        private void OnEnable()
        {

            OnWandering += target => animator.SetBool("IsWalk", true);
            OnWandering += Wandering;
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
            //OnWaitForNextAttack += ;
        }
        private void OnDisable()
        {
        }
    }
}

