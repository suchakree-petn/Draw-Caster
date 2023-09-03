using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    public Weapon weapon;
    public bool isReady = true;
    [SerializeField] private PlayerAction playerActions;

    private void Start()
    {
        playerActions = PlayerInputSystem.Instance.playerAction;
        
        playerActions.Player.PressAttack.Enable();
        playerActions.Player.PressAttack.performed += PressAttack;

        playerActions.Player.HoldAttack.Enable();
        playerActions.Player.HoldAttack.performed += OnStartAttack;
        playerActions.Player.HoldAttack.canceled += OnEndAttack;
    }
    void Update()
    {
        Debug.DrawLine(transform.root.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }
    private void OnEnable()
    {
        


    }

    private void OnDisable()
    {
        playerActions.Player.PressAttack.Disable();
        playerActions.Player.PressAttack.performed -= PressAttack;

        playerActions.Player.HoldAttack.Disable();
        playerActions.Player.HoldAttack.performed -= OnStartAttack;
        playerActions.Player.HoldAttack.canceled -= OnEndAttack;
    }


    public void PressAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weapon.Attack(transform.parent.gameObject);
        }
    }

    public void OnStartAttack(InputAction.CallbackContext context)
    {
        if (!isReady)
        {
            isReady = true;
            Fire();
        }
    }

    public void OnEndAttack(InputAction.CallbackContext context)
    {
        isReady = false;
    }

    public void Fire()
    {
        if (isReady)
        {
            weapon.HoldAttack(transform.parent.gameObject);
            Invoke(nameof(Fire), weapon.activeRate);
        }
    }

}
