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
        playerAction = PlayerInputSystem.Instance.playerAction;
        playerData = transform.GetComponentInParent<PlayerManager>().playerData;
        playerRb = transform.GetComponentInParent<Rigidbody2D>();
        playerAction.Player.Movement.Enable();

    }


    void FixedUpdate()
    {
        movement = playerAction.Player.Movement.ReadValue<Vector2>();
        playerRb.MovePosition(playerRb.position + movement.normalized * playerData._moveSpeed * Time.fixedDeltaTime);
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        playerAction.Player.Movement.Disable();
    }

}
