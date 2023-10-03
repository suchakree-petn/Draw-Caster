using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DrawCaster.Util;
using DG.Tweening;
using Cinemachine;

namespace LeafRanger
{
    public enum State
    {
        Idle,
        Chase,
        PreAttack,
        Attack,
        WaitForNextAttack
    }
    public class LeafRangerBehaviour : MonoBehaviour
    {
        public State currentState;
        public Rigidbody2D leafRangerRb;
        public string targetTag;
        public Transform target;
        private EnemyData leafRangerData;

        [SerializeField] private Animator animator;
        [SerializeField] private GameObject _arrowPrefab;

        public AnimationClip attackClip;
        public AnimationClip dropClip;

        // private delegate void LeafRangerDelegate(Transform target);
        // private static LeafRangerDelegate OnIdle;
        // private static LeafRangerDelegate OnChase;
        // private static LeafRangerDelegate OnPreAttack;
        // private static LeafRangerDelegate OnAttack;
        // private static LeafRangerDelegate OnWaitForNextAttack;

        private void Awake()
        {
            GameObject targetObj = GameObject.Find("Player");
            target = targetObj.transform;
            leafRangerData = GetComponentInParent<CharactorManager<EnemyData>>().GetCharactorData();
            leafRangerRb = GetComponentInParent<Rigidbody2D>();
            animator = transform.parent.GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            distance = GetDistance();
            if (target != null)
            {
                switch (currentState)
                {
                    case State.Idle:
                        // OnIdle?.Invoke(target);
                        Idle();
                        ChangeStateManager();
                        break;
                    case State.Chase:
                        // OnChase?.Invoke(target);
                        Chase();
                        ChangeStateManager();
                        break;
                    case State.PreAttack:
                        // OnPreAttack?.Invoke(target);
                        PreAttack();
                        break;
                    case State.Attack:
                        // OnAttack?.Invoke(target);
                        Attack();
                        currentState = State.WaitForNextAttack;
                        break;
                    case State.WaitForNextAttack:
                        // OnWaitForNextAttack?.Invoke(target);
                        WaitForNextAttack();
                        break;
                    default:
                        break;
                }
            }
        }
        [Header("Range")]
        [SerializeField] private float kiteRange;
        [SerializeField] private float attackRange;
        [SerializeField] private float visionRange;
        [SerializeField] private float distance;
        void DrowRange()
        {
            // DrowLine
        }
        void ChangeStateManager()
        {
            if (distance > visionRange)
            {
                currentState = State.Idle;
            }
            else if (distance <= visionRange && distance > attackRange)
            {
                currentState = State.Chase;
            }
            else if (distance <= attackRange)
            {
                currentState = State.PreAttack;
            }
        }
        float GetDistance()
        {
            Transform targetMidTransform = DrawCasterUtil.GetMidTransformOf(target);
            Transform leafRangerMidTransform = DrawCasterUtil.GetMidTransformOf(transform.root);
            return Vector2.Distance(targetMidTransform.position, leafRangerMidTransform.position);
        }
        Vector2 GetDirection()
        {
            Transform targetMidTransform = DrawCasterUtil.GetMidTransformOf(target);
            Transform leafRangerMidTransform = DrawCasterUtil.GetMidTransformOf(transform.root);
            return targetMidTransform.position - leafRangerMidTransform.position;
        }
        [SerializeField] private float kiteRatio;

        //------------------------------Move------------------------------

        void MoveToChaseTarget()
        {
            Vector2 direction = GetDirection();
            leafRangerRb.MovePosition(leafRangerRb.position + direction.normalized * leafRangerData._moveSpeed * Time.fixedDeltaTime);
            FlipLeafRanger(1);
        }
        void MoveCirCle(string clockDirection)
        {
            float distance = GetDistance();
            Vector2 direction = GetDirection();
            float x = direction.x;
            float y = direction.y;

            if (clockDirection == "counterClockWise")
            {
                direction.x = y;
                direction.y = x * -1;

                if (direction.x >= 0 && direction.y > 0 || direction.x <= 0 && direction.y < 0)
                {
                    if (distance <= kiteRange)
                    {
                        direction.y *= 1 / kiteRatio;
                    }
                    else if (distance > attackRange)
                    {
                        direction.y *= 1 * kiteRatio;
                    }
                    FlipLeafRanger(-1);
                }
                else if (direction.x < 0 && direction.y >= 0 || direction.x > 0 && direction.y <= 0)
                {

                    if (distance <= kiteRange)
                    {
                        direction.y *= 1 * kiteRatio;
                    }
                    else if (distance > attackRange)
                    {
                        direction.y *= 1 / kiteRatio;
                    }
                    FlipLeafRanger(1);
                }
            }
            else if (clockDirection == "clockWise")
            {
                direction.x = y * -1;
                direction.y = x;

                if (direction.x >= 0 && direction.y > 0 || direction.x <= 0 && direction.y < 0)
                {
                    if (distance <= kiteRange)
                    {
                        direction.x *= 1 / kiteRatio;
                    }
                    else if (distance > attackRange)
                    {
                        direction.x *= 1 * kiteRatio;
                    }
                    FlipLeafRanger(1);
                }
                else if (direction.x < 0 && direction.y >= 0 || direction.x > 0 && direction.y <= 0)
                {
                    if (distance <= kiteRange)
                    {
                        direction.x *= 1 * kiteRatio;
                    }
                    else if (distance > attackRange)
                    {
                        direction.x *= 1 / kiteRatio;
                    }
                    FlipLeafRanger(-1);
                }
            }

            leafRangerRb.MovePosition(leafRangerRb.position + direction.normalized * leafRangerData._moveSpeed * Time.fixedDeltaTime);
        }
        void FlipLeafRanger(float negative)
        {
            Transform targetMidTransform = DrawCasterUtil.GetMidTransformOf(target);
            Transform leafRangerMidTransform = DrawCasterUtil.GetMidTransformOf(transform.root);
            float direct = negative * (targetMidTransform.position.x - leafRangerMidTransform.position.x);
            if (direct > 0)
            {
                transform.root.DORotate(new Vector3(0, 0, 0), 0);
            }
            else if (direct < 0)
            {
                transform.root.DORotate(new Vector3(0, 180, 0), 0);
            }
        }

        //-----------------------------Attack-----------------------------

        [Header("Attack")]
        private bool attack = false;
        [SerializeField] private float _baseAttackDamageMultiplier;
        [SerializeField] private float attackKnockbackGaugeDeal;
        [SerializeField] private float speed;
        [SerializeField] private float flytime;
        [SerializeField] private AnimationCurve arrowSpeedCurve;
        [SerializeField] private float minDelayAttackTime;
        [SerializeField] private float maxDelayAttackTime;
        [SerializeField] private float arrowOnFallTime;
        void LeafRangerAttack()
        {
            Transform midTransform = DrawCasterUtil.GetMidTransformOf(transform.root);
            GameObject arrow = Instantiate(_arrowPrefab);
            arrow.transform.position = midTransform.position;
            arrow = DrawCasterUtil.AddAttackHitTo(arrow,
                leafRangerData.elementalType, transform.root.gameObject,
                _baseAttackDamageMultiplier,
                flytime + dropClip.length + arrowOnFallTime,
                leafRangerData.targetLayer,
                attackKnockbackGaugeDeal,
                1f
                );
            Transform targetMidTransform = DrawCasterUtil.GetMidTransformOf(target);
            Transform leafRangerMidTransform = DrawCasterUtil.GetMidTransformOf(transform.root);
            Vector2 direction = targetMidTransform.position - leafRangerMidTransform.position;
            Collider2D arrowCol = arrow.GetComponent<Collider2D>();
            Animator arrowAnim = arrow.GetComponentInChildren<Animator>();
            arrow.transform.right = direction;
            arrow.transform.DOMove((Vector2)arrow.transform.position + direction.normalized * speed, flytime)
            .SetEase(arrowSpeedCurve)
            .OnUpdate(() =>
            {
                RaycastHit2D[] hitCol = Physics2D.BoxCastAll(arrowCol.bounds.center, arrowCol.bounds.size, 0, direction, 0, leafRangerData.targetLayer);
                foreach (RaycastHit2D Col in hitCol)
                {
                    if (Col.collider.tag == "Hitbox" && Col.collider.transform.root.tag == targetTag)
                    {
                        Destroy(arrow, 0.1f);
                    }
                }
            })
            .OnComplete(() =>
            {
                if (arrow.transform.right.x < 0)
                {
                    arrow.transform.DORotate(new Vector3(0, 180, 0), 0);
                }
                arrow.transform.right = Vector2.zero;
                arrowAnim.SetTrigger("Drop");
                arrowCol.enabled = false;
            });
        }
        private void Idle()
        {
            animator.SetBool("IsWalk", false);
        }
        private void Chase()
        {
            animator.SetBool("IsWalk", true);
            MoveToChaseTarget();
        }
        private void PreAttack()
        {
            if (!attack) currentState = State.Attack;
        }
        private void Attack()
        {
            FlipLeafRanger(1);
            attack = true;
            animator.SetBool("IsWalk", false);
            animator.SetTrigger("Attack");
        }
        private void WaitForNextAttack()
        {
            AttackAction();
            AfterAttackAction();
        }
        private void OnEnable()
        {

            // OnIdle += target =>
            // {
            //     animator.SetBool("IsWalk", false);
            // };
            // OnChase += target =>
            // {
            //     animator.SetBool("IsWalk", true);
            //     MoveToChaseTarget();
            // };
            // OnPreAttack += target =>
            // {
            //     if (!attack) currentState = State.Attack;
            // };
            // OnAttack += target =>
            // {
            //     FlipLeafRanger(1);
            //     attack = true;
            //     animator.SetBool("IsWalk", false);
            //     animator.SetTrigger("Attack");
            // };
            // OnWaitForNextAttack += target =>
            // {
            //     AttackAction();
            //     AfterAttackAction();
            // };
        }
        private Coroutine coroutineAttackAnimation;
        private void AttackAction()
        {
            if (coroutineAttackAnimation == null)
            {
                float delayAttackTime = Random.Range(minDelayAttackTime, maxDelayAttackTime);
                coroutineAttackAnimation = StartCoroutine(DelayNextAttack(delayAttackTime, attackClip.length));
            }
        }
        [SerializeField] private float minSwapStayOrMoveTime;
        [SerializeField] private float maxSwapStayOrMoveTime;
        private void AfterAttackAction()//Move or stay action
        {
            if (!attack)
            {
                //return bool stayInWaitForNextAttack and string clockDirection every randomStayOrMoveTime
                if (coroutineStayOrMove == null)
                {
                    float randomStayOrMoveTime = Random.Range(minSwapStayOrMoveTime, maxSwapStayOrMoveTime);
                    coroutineStayOrMove = StartCoroutine(StayOrMove(randomStayOrMoveTime));
                }
                if (stayInWaitForNextAttack && distance < attackRange && distance >= kiteRange)
                {
                    //Stay
                    animator.SetBool("IsWalk", false);
                    FlipLeafRanger(1);
                }
                else
                {
                    //Move
                    MoveCirCle(clockDirection);
                    animator.SetBool("IsWalk", true);
                }
            }

        }
        private string clockDirection;
        void RandomDirection()
        {
            float randomDirection = Random.Range(0, 2);
            Debug.Log(randomDirection);
            if (randomDirection < 0.5)
            {
                clockDirection = "counterClockWise";
            }
            else if (randomDirection >= 0.5)
            {
                clockDirection = "clockWise";
            }
        }
        IEnumerator DelayNextAttack(float delayTime, float attackTime)
        {
            yield return new WaitForSeconds(attackTime);
            attack = false;
            yield return new WaitForSeconds(delayTime);
            coroutineAttackAnimation = null;
            if (currentState == State.WaitForNextAttack)
            {
                ChangeStateManager();
            }

        }
        Coroutine coroutineStayOrMove;
        private bool stayInWaitForNextAttack;

        IEnumerator StayOrMove(float delayTime)
        {
            float randomDirection = Random.Range(0, 2);
            Debug.Log(randomDirection);
            if (randomDirection < 0.7)
            {
                RandomDirection();
                stayInWaitForNextAttack = false;
            }
            else if (randomDirection >= 0.7)
            {
                stayInWaitForNextAttack = true;
            }
            yield return new WaitForSeconds(delayTime);
            coroutineStayOrMove = null;
        }
    }
}