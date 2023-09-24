using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerAction playerAction;
    public Vector2 movement;

    private void Awake()
    {
        playerAction = PlayerInputSystem.Instance.playerAction;
        playerData = transform.GetComponentInParent<CharactorManager<PlayerData>>().GetCharactorData();
        playerRb = transform.GetComponentInParent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        movement = playerAction.Player.Movement.ReadValue<Vector2>();
        playerRb.MovePosition(playerRb.position + movement.normalized * playerData._moveSpeed * Time.fixedDeltaTime);
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
