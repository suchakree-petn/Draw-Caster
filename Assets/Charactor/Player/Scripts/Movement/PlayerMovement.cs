using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerAction playerAction;
    public Vector2 movement;

    private void Start()
    {
        playerData = transform.GetComponentInParent<CharactorManager<PlayerData>>().GetCharactorData();
    }


    void FixedUpdate()
    {
        movement = playerAction.Player.Movement.ReadValue<Vector2>();
        Move();
    }
    void Move()
    {
        playerRb.MovePosition(playerRb.position + movement.normalized * playerData._moveSpeed * Time.fixedDeltaTime);


    }
    void StopMove()
    {
        playerRb.velocity = Vector2.zero;
    }

    private void OnEnable()
    {
        playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.Movement.Enable();

    }
    private void OnDisable()
    {
        playerAction.Player.Movement.Disable();

    }

}
