using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private CharactorData playerData;
    [SerializeField] private PlayerAction playerAction;
    public Vector2 movement;

    private void Awake()
    {
        playerAction = PlayerInputSystem.Instance.playerAction;
    }


    void FixedUpdate()
    {
        movement = playerAction.Player.Movement.ReadValue<Vector2>();
        playerRb.MovePosition(playerRb.position + movement.normalized * playerData.MovementSpeed * Time.fixedDeltaTime);
    }
    private void OnEnable()
    {
        playerAction.Player.Movement.Enable();
    }
    private void OnDisable()
    {
        playerAction.Player.Movement.Disable();
    }

}
