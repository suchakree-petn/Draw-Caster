using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Animator animator;
    private PlayerData playerData;
    private PlayerAction playerAction;
    public Vector2 movement;
    [SerializeField] private float defaultMovementSpeed;
    [Range(0, 1f)]
    [SerializeField] private float movementSpeedPenalty;
    [SerializeField] private float currentMovementSpeed;
    public float CurrentMovementSpeed => currentMovementSpeed;

    private void Start()
    {
        playerData = transform.GetComponentInParent<CharactorManager<PlayerData>>().GetCharactorData();
        defaultMovementSpeed = playerData._moveSpeed;
    }


    void FixedUpdate()
    {
        movement = playerAction.Player.Movement.ReadValue<Vector2>();
        Move();
        CheckFacing();
    }
    void Move()
    {
        playerRb.MovePosition(playerRb.position + currentMovementSpeed * Time.fixedDeltaTime * movement.normalized);
    }

    void CheckFacing()
    {
        float mousePosX = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
        float playerPosX = animator.rootPosition.x;
        SpriteRenderer spriteRenderer = animator.GetComponent<SpriteRenderer>();
        if (mousePosX < playerPosX)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        if (movement != Vector2.zero)
        {
            if (movement.x >= 0 && mousePosX < playerPosX)
            {
                WalkBack();
            }
            else if (movement.x <= 0 && mousePosX > playerPosX)
            {
                WalkBack();
            }
            else
            {
                WalkFront();
            }

        }
        else
        {
            WalkFront();
        }

    }

    void WalkFront()
    {
        SetToDefaultSpeed();
        animator.SetBool("IsWalkBack", false);
    }
    void WalkBack()
    {
        SetSpeed(movementSpeedPenalty);
        animator.SetBool("IsWalkBack", true);
    }

    public float SetSpeed(float percentage)
    {
        currentMovementSpeed = (1 - percentage) * defaultMovementSpeed;
        return currentMovementSpeed;
    }
    public float SetToDefaultSpeed()
    {
        currentMovementSpeed = defaultMovementSpeed;
        return currentMovementSpeed;
    }
    private void OnEnable()
    {
        Debug.Log("Enable");
        playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.Movement.Enable();

    }
    private void OnDisable()
    {
        Debug.Log("Disable");
        playerAction.Player.Movement.Disable();

    }

}
