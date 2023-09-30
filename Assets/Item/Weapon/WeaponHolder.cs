using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    public Weapon weapon;
    public bool isHoldAttack;
    public bool isReadyToAttack;
    [SerializeField] private PlayerAction playerActions;

    void Update()
    {
        //Debug.DrawLine(transform.root.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

        if (isHoldAttack && isReadyToAttack)
        {
            isReadyToAttack = false;
            weapon.HoldAttack(transform.root.gameObject);
            StartCoroutine(Delay(weapon.activeRate));
        }
    }



    public void PressAttack(InputAction.CallbackContext context)
    {
        if (isReadyToAttack)
        {
            isReadyToAttack = false;
            weapon.Attack(transform.root.gameObject);
            StartCoroutine(Delay(weapon.activeRate));
        }
    }

    public void OnStartHoldAttack(InputAction.CallbackContext context)
    {
        isHoldAttack = true;
    }

    public void OnEndHoldAttack(InputAction.CallbackContext context)
    {
        isHoldAttack = false;
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isReadyToAttack = true;

    }
    private void OnEnable()
    {
        playerActions = PlayerInputSystem.Instance.playerAction;

        playerActions.Player.PressAttack.Enable();
        playerActions.Player.PressAttack.performed += PressAttack;

        playerActions.Player.HoldAttack.Enable();
        playerActions.Player.HoldAttack.performed += OnStartHoldAttack;
        playerActions.Player.HoldAttack.canceled += OnEndHoldAttack;
    }

    private void OnDisable()
    {
        playerActions.Player.PressAttack.Disable();
        playerActions.Player.PressAttack.performed -= PressAttack;

        playerActions.Player.HoldAttack.Disable();
        playerActions.Player.HoldAttack.performed -= OnStartHoldAttack;
        playerActions.Player.HoldAttack.canceled -= OnEndHoldAttack;

        playerActions = null;
    }

}
