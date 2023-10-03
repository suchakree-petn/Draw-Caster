
using UnityEngine;
using DG.Tweening;
using DrawCaster.Util;
using System.Collections.Generic;
using LeafRanger;
using System.Collections;

namespace MoltenFire
{
    public enum State
    {
        Wandering,
        Idle,
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
        public EnemyManager moltenFireManager { get; set; }
        public EnemyData moltenFireData { get; set; }
        [SerializeField] private float stunTime;
        [SerializeField] private Collider2D hitBox;


        public delegate void MoltenFireDelegate(Transform target);
        private void Awake()
        {
            moltenFireManager = transform.root.GetComponent<EnemyManager>();
            moltenFireData = moltenFireManager.GetCharactorData();
            moltenFireRb = GetComponentInParent<Rigidbody2D>();
            animator = transform.parent.GetComponentInChildren<Animator>();
            moveSpeed = moltenFireData._moveSpeed;
        }
        private void Start()
        {
            target = GameObject.FindWithTag(targetTag).transform;
        }
        void Update()
        {
            if (target != null)
            {
                switch (currentState)
                {
                    case State.Wandering:
                        OnWandering?.Invoke(target);
                        currentState = State.Idle;
                        break;
                    case State.Idle:
                        break;
                    case State.MeleeAttack:
                        OnMeleeAttack?.Invoke(target);
                        currentState = State.Idle;
                        break;
                    case State.FlamePillarAttack:
                        OnFlamePillarAttack?.Invoke(target);
                        currentState = State.Idle;
                        break;
                    case State.FireBallAttack:
                        OnFireBallAttack?.Invoke(target);
                        currentState = State.Idle;
                        break;
                    case State.WaitForNextAttack:
                        OnWaitForNextAttack?.Invoke(target);
                        currentState = State.Idle;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsWalkBackward", false);
            }
        }
        private void OnDrawGizmosSelected()
        {

            Debug.DrawLine(transform.root.position, transform.root.position + new Vector3(FlamePillarAttackWeight, 0, 0), Color.red);
            Debug.DrawLine(transform.root.position, transform.root.position + new Vector3(MeleeAttackWeight, 0.5f, 0), Color.white);
            Debug.DrawLine(transform.root.position, transform.root.position + new Vector3(FireBallAttackWeight, 1f, 0), Color.black);
        }
        ////////////////////////// Animation event ///////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Header("Wander State")]
        [SerializeField] private float wanderDistance;
        [SerializeField] private float minWanderDuration;
        [SerializeField] private float maxWanderDuration;
        [SerializeField] private float eachDuration;
        float randomDuration;
        int isForward;
        public MoltenFireDelegate OnWandering;

        private void Wandering(Transform target)
        {
            Vector3 targetPos = DrawCasterUtil.GetMidTransformOf(target).position;
            Vector3 moltenFirePos = DrawCasterUtil.GetMidTransformOf(transform.root).position;
            float distant = Vector3.Distance(targetPos, moltenFirePos);
            eachDuration = Random.Range(0.5f, eachDuration);
            randomDuration = Random.Range(minWanderDuration, maxWanderDuration);
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(randomDuration).OnComplete(() =>
            {
                sequence.Kill();
                AnimStopMoving();
                currentState = State.WaitForNextAttack;
            });
            sequence.AppendCallback(() =>
            {
                if (distant > wanderDistance)
                {
                    AnimMoveToTarget();
                }
                else
                {
                    AnimMoveOutTaget();
                }
            });

        }

        public void AnimStopMoving()
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsWalkBackward", false);
        }

        public void AnimMoveOutTaget()
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsWalkBackward", true);
            isForward = -1;
        }

        public void AnimMoveToTarget()
        {
            animator.SetBool("IsWalk", true);
            animator.SetBool("IsWalkBackward", false);
            isForward = 1;
        }
        public void FacingToTarget(Transform target)
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
        public float moveSpeed { get; set; }
        void MoveToChaseTarget()
        {
            Vector2 direction = DrawCasterUtil.GetMidTransformOf(target).position - DrawCasterUtil.GetMidTransformOf(transform.root).position;
            direction *= isForward;
            moltenFireRb.DOMove(moltenFireRb.position + direction.normalized * moveSpeed, 0.3f);

        }
        [Header("Melee Attack")]
        public float _baseMeleeDamageMultiplier;
        public float attackRange;
        [SerializeField] private float meleeKnockbackGaugeDeal;
        [SerializeField] private Collider2D meleeHitbox;
        [SerializeField] private Transform checkMeleeAttack;
        public MoltenFireDelegate OnMeleeAttack;
        void MeleeAttack()
        {
            meleeHitbox.gameObject.SetActive(true);
            DrawCasterUtil.AddAttackHitTo(
                meleeHitbox.transform.gameObject,
                moltenFireData.elementalType,
                transform.root.gameObject,
                _baseMeleeDamageMultiplier,
                0,
                moltenFireData.targetLayer,
                meleeKnockbackGaugeDeal
            );
            meleeHitbox.transform.DOMoveY(meleeHitbox.transform.position.y - 0.1f, 0.1f).OnComplete(() =>
            {
                meleeHitbox.transform.DOMoveY(meleeHitbox.transform.position.y + 0.1f, 0.1f).OnComplete(() =>
                {
                    Component ath = meleeHitbox.GetComponent<AttackHit>();
                    DestroyImmediate(ath);
                    meleeHitbox.gameObject.SetActive(false);
                });
            });
        }

        [Header("Flame Pillar Attack")]
        public float _baseFlamePillarDamageMultiplier;
        [SerializeField] private float firePillarKnockbackGaugeDeal;
        [SerializeField] private GameObject _flamePillarPrefab;
        [SerializeField] private List<Transform> spawnPos;
        [SerializeField] private float timeInterval;
        public AnimationClip FlamePillarClip;
        [SerializeField] private Transform flamePillarCol;
        public MoltenFireDelegate OnFlamePillarAttack;

        void FlamePillarAttack()
        {
            Sequence sequence = DOTween.Sequence();
            List<GameObject> flamePillar = new();
            for (int i = 0; i < spawnPos.Count; i++)
            {
                int index = i;
                sequence.AppendCallback(() =>
                {
                    SpawnFlamePillar(spawnPos[index].position);
                });
                sequence.AppendInterval(timeInterval);


            }
        }

        private GameObject SpawnFlamePillar(Vector3 position)
        {
            GameObject flamePillar = Instantiate(_flamePillarPrefab);
            flamePillar.transform.position = position;
            flamePillar = DrawCasterUtil.AddAttackHitTo(flamePillar,
                moltenFireData.elementalType, transform.root.gameObject,
                _baseFlamePillarDamageMultiplier,
                FlamePillarClip.length,
                moltenFireData.targetLayer,
                firePillarKnockbackGaugeDeal
                );
            return flamePillar;
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

        public MoltenFireDelegate OnFireBallAttack;
        public void FireBallAttack()
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
        public void Move(Transform fireBallAttack, int i)
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
        public Transform GetNewSpawnPos(int index)
        {
            if (index % 2 == 0)
            {
                return spawnPosRight;
            }
            return spawnPosLeft;
        }
        public Transform GetNewMidCurve(int index)
        {
            if (index % 2 == 0)
            {
                return midCurveRight;
            }
            return midCurveLeft;
        }
        public void WaitToWandering()
        {
            sequence = DOTween.Sequence();

            sequence.AppendInterval(waitDuration).OnComplete(() =>
            {
                sequence.Kill();
                currentState = State.Wandering;
            });
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Header("Attack Check")]
        [SerializeField] private float MeleeAttackWeight;
        [SerializeField] private float FlamePillarAttackWeight;
        [SerializeField] private float FireBallAttackWeight;
        [SerializeField] private float waitDuration;
        [SerializeField] private float AttackCheckDuration;
        Sequence sequence;
        public MoltenFireDelegate OnWaitForNextAttack;

        private void RandomAttackType(Transform target)
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsWalkBackward", false);


            Vector3 targetPos = DrawCasterUtil.GetMidTransformOf(target).position;
            Vector3 moltenFirePos = DrawCasterUtil.GetMidTransformOf(transform.root).position;
            float distant = Vector3.Distance(targetPos, moltenFirePos);
            WeightedRandom<Transform> randomCheckCol = new();
            if (distant <= FlamePillarAttackWeight)
            {
                randomCheckCol.AddItem(flamePillarCol, 5);
                randomCheckCol.AddItem(checkMeleeAttack, 2);
                randomCheckCol.AddItem(fireBallCol, 3);
                Transform openCheckCol = randomCheckCol.GetRandom();
                openCheckCol.gameObject.SetActive(true);
            }
            else if (distant <= MeleeAttackWeight)
            {
                randomCheckCol.AddItem(flamePillarCol, 3);
                randomCheckCol.AddItem(checkMeleeAttack, 5);
                randomCheckCol.AddItem(fireBallCol, 2);
                Transform openCheckCol = randomCheckCol.GetRandom();
                openCheckCol.gameObject.SetActive(true);
            }
            else if (distant <= FireBallAttackWeight)
            {
                randomCheckCol.AddItem(flamePillarCol, 1);
                randomCheckCol.AddItem(checkMeleeAttack, 2);
                randomCheckCol.AddItem(fireBallCol, 7);
                Transform openCheckCol = randomCheckCol.GetRandom();
                openCheckCol.gameObject.SetActive(true);

            }
            else
            {
                checkMeleeAttack.gameObject.SetActive(true);
            }
        }



        [SerializeField] private Animator animator;

        private void OnEnable()
        {

            OnWandering += target => animator.SetBool("IsWalk", true);
            OnWandering += Wandering;
            OnWandering += FacingToTarget;
            OnWaitForNextAttack += RandomAttackType;

            OnMeleeAttack += target =>
            {
                animator.SetTrigger("MeleeAttack");
                animator.SetBool("IsWalk", false);
            };
            OnMeleeAttack += FacingToTarget;
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
            moltenFireManager.OnStartKnockback += (elementalDamage) =>
            {
                StartKB();
            };
            moltenFireManager.OnEndKnockback += () =>
            {
                EndKB();
            };

            moltenFireManager.OnEnemyDead += (deadEnemy) =>
            {
                Debug.Log(transform.root.name);
                hitBox.enabled = false;
                AnimStopMoving();
                animator.SetTrigger("Death");
                DelayDestroy(moltenFireManager.enemyDeadClip.length, deadEnemy);
            };
        }
        Coroutine delayStun = null;
        private void EndKB()
        {

            if (delayStun == null)
            {
                Debug.Log("StunNull");

                delayStun = StartCoroutine(KnockbackStun(stunTime));
            }
            else
            {
                Debug.Log("Stun not null");

            }
        }

        private void StartKB()
        {
            checkMeleeAttack.gameObject.SetActive(false);
            fireBallCol.gameObject.SetActive(false);
            flamePillarCol.gameObject.SetActive(false);
            AnimStopMoving();
            animator.SetTrigger("Hit");
            Vector2 direction = DrawCasterUtil.GetDirectionFromLower(transform.root, target) * -1;
            transform.root.DOMove(
                (Vector2)transform.root.position + direction.normalized * moltenFireManager.enemyKnockbackDistance,
                moltenFireManager.enemyKnockbackClip.length);
            StartCoroutine(moltenFireManager.DelayKnockback(moltenFireManager.enemyKnockbackClip.length));

        }

        void DelayDestroy(float time, GameObject gameObject)
        {
            Destroy(gameObject, time);
        }
        IEnumerator KnockbackStun(float time)
        {
            currentState = State.Idle;
            AnimStopMoving();

            yield return new WaitForSeconds(time);
            currentState = State.Wandering;
            delayStun = null;
        }
        private void OnDisable()
        {
            OnWandering -= Wandering;
            OnWandering -= FacingToTarget;
            OnWaitForNextAttack -= RandomAttackType;
            OnMeleeAttack += FacingToTarget;

        }
    }

}

