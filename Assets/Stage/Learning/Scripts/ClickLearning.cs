using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ClickLearning : MonoBehaviour
{
    private PlayerAction playerActions;
    private void Awake()
    {
        playerActions = PlayerInputSystem.Instance.playerAction;

    }
    private void OnEnable()
    {
        DisableAbililyWhenOpenUI();
        playerActions.Player.PressAttack.Enable();
        playerActions.Player.PressAttack.canceled += OnLeftClick;
        Time.timeScale = 0;
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        Time.timeScale = 1;
        playerActions.Player.PressAttack.Disable();
        playerActions.Player.PressAttack.canceled -= OnLeftClick;
        EnableAbililyWhenCloseUI();
        gameObject.SetActive(false);
    }
    void DisableAbililyWhenOpenUI()
    {
        WeaponHolder weaponHolder = GameObject.FindWithTag("Player").transform.GetChild(1).gameObject.GetComponent<WeaponHolder>();
        GameObject spellSlots = GameObject.FindWithTag("Player").transform.GetChild(4).gameObject;
        GameObject manaNullify = GameObject.FindWithTag("Player").transform.GetChild(6).gameObject;
        weaponHolder.enabled = false;
        spellSlots.SetActive(false);
        manaNullify.SetActive(false);
        playerActions.Player.Movement.Disable();
    }
    void EnableAbililyWhenCloseUI()
    {
        playerActions.Player.Movement.Enable();
        string currentSceneName = SceneManager.GetActiveScene().name;
        WeaponHolder weaponHolder = GameObject.FindWithTag("Player").transform.GetChild(1).gameObject.GetComponent<WeaponHolder>();
        GameObject spellSlots = GameObject.FindWithTag("Player").transform.GetChild(4).gameObject;
        GameObject manaNullify = GameObject.FindWithTag("Player").transform.GetChild(6).gameObject;
        if (currentSceneName == "Stage_1_Learning")
        {
            weaponHolder.enabled = true;
        }
        else if (currentSceneName == "Stage_2_Learning")
        {
            weaponHolder.enabled = true;
            spellSlots.SetActive(true);
        }
        else if (currentSceneName == "Stage_3_Learning")
        {
            weaponHolder.enabled = true;
            spellSlots.SetActive(true);
            manaNullify.SetActive(true);
        }
    }
}
