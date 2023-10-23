using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickLearning : MonoBehaviour
{
    private PlayerAction playerActions;
    private void OnEnable() {
        playerActions = PlayerInputSystem.Instance.playerAction;
        playerActions.Player.PressAttack.Enable();
        playerActions.Player.PressAttack.canceled += OnLeftClick;
        Time.timeScale = 0;
    }
    public void OnLeftClick(InputAction.CallbackContext context){
        Time.timeScale = 1;
        playerActions.Player.PressAttack.Disable();
        playerActions.Player.PressAttack.canceled -= OnLeftClick;
        gameObject.SetActive(false);
    }
}
