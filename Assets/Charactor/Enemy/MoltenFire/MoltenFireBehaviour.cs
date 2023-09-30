
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
            }else
            {
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsWalkBackward", false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireBallAttack();
            }
        }
        ////////////////////////// Animation event /////////////////////////
        [Header("Wander State")]
        [SerializeField] private float wanderDistance;
        [SerializeField] private float minWanderDuration;
        [SerializeField] private float maxWanderDuration;
        [SerializeField] private float timerWalkBack = 0;
        [SerializeField] private float timerWandering = 0;
        float randomBackDuration;
        float randomDuration;

        int isForward;
        public static MoltenFireDelegate OnWandering;

        private void Wandering(Transform target)
        {
            Vector3 targetPos = DrawCasterUtil.GetMidTransformOf(target).position;
            Vector3 moltenFirePos = DrawCasterUtil.GetMidTransformOf(transform.root).position;
            float distant = Vector3.Distance(targetPos, moltenFirePos);

            timerWalkBack += Time.deltaTime;
            if (timerWalkBack < randomBackDuration)
            {
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsWalkBackward", true);
                isForward = -1;
            }
            else
            {
                timerWandering += Time.deltaTime;
                if (timerWandering < randomDuration)
                {
                    if (distant > wanderDistance)
                    {
                        animator.SetBool("IsWalk", true);
                        animator.SetBool("IsWalkBackward", false);
                        isForward = 1;
                    }
                    else
                    {
                        animator.SetBool("IsWalk", false);
                        animator.SetBool("IsWalkBackward", true);
                        isForward = -1;
                    }
                }
                else
                {
                    timerWalkBack = 0;
                    timerWandering = 0;
                    randomBackDuration = Random.Range(0, minWanderDuration);
                    randomDuration = Random.Range(minWanderDuration, maxWanderDuration);
                    currentState = State.WaitForNextAttack;
                }
            }
        }
        void DisableAllCheckAttackCol()
        {
            meleeHitbox.gameObject.SetActive(false);
            flamePillarCol.gameObject.SetActive(false);
            fireBallCol.gameObject.SetActive(false);
        }
        void FacingToTarget(Transform target)
        {
            Vector2 moltenFireLowerTransform = DrawCasterUtil.GetLowerTransformOf(transform.root).position;
            Vector2 targetLowerTransform = DrawCasterUtil.GetLowerTransformOf(target).position;
            if (moltenFireLowerTransform.x < targetLowerTransform.x)
            {
                transform.root.DORotate(new Vector3(0, 180, 0), 0);
            }
            else
            {
                transform.root.DORotate(Vector3.zero, 0);
            }
        }
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
                Debug.Log(overlapCol.collider.name);
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
        [SerializeField] private float randomness;
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
            Vector3 targetPos = DrawCasterUtil.GetMidTransformOf(target).position;
            targetPos = DrawCasterUtil.RandomPosition(targetPos, randomness);

            fireBallAttack.DOPath(
                        new Vector3[] { fireBallAttack.position, GetNewMidCurve(i).position, targetPos },
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
        [SerializeField] private float AttackCheckDuration;
        Sequence sequence;
        private void RandomAttackType(Transform target)
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsWalkBackward", false);

            if (sequence == null)
            {
                WeightedRandom<Transform> randomCheckCol = new();
                randomCheckCol.AddItem(meleeHitbox.transform, MeleeAttackWeight);
                randomCheckCol.AddItem(flamePillarCol, FlamePillarAttackWeight);
                randomCheckCol.AddItem(fireBallCol, FireBallAttackWeight);
                randomCheckCol.GetRandom().gameObject.SetActive(true);
                Debug.Log(randomCheckCol.GetRandom().name);

                sequence = DOTween.Sequence();
                sequence.AppendInterval(AttackCheckDuration).OnUpdate(() =>
                {
                    if (currentState != State.WaitForNextAttack)
                    {
                        sequence.Kill();
                    }
                }).OnComplete(() =>
                {
                    currentState = State.Wandering;
                    sequence = null;
                });
            }

        }



        [SerializeField] private Animator animator;
        private void OnEnable()
        {

            OnWandering += target => animator.SetBool("IsWalk", true);
            OnWandering += Wandering;
            OnWandering += target => DisableAllCheckAttackCol();
            OnWandering += FacingToTarget;
            OnWaitForNextAttack += RandomAttackType;

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
            OnWandering -= Wandering;
            OnWandering -= FacingToTarget;
            OnWaitForNextAttack -= RandomAttackType;
        }
    }
}

