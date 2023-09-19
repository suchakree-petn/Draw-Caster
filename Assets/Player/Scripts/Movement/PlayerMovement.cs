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
        playerData = transform.GetComponentInParent<PlayerManager>().playerData;
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
