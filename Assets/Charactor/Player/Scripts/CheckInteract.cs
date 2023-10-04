using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInteract : MonoBehaviour
{
    IInteractable interactable;
    public void OnTriggerEnter2D(Collider2D other)
    {
        interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.ShowInteractUI();
            PlayerInputSystem.Instance.playerAction.Player.Interact.Enable();
            PlayerInputSystem.Instance.playerAction.Player.Interact.performed += interactable.Interact;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.HideInteractUI();
            PlayerInputSystem.Instance.playerAction.Player.Interact.Disable();
            PlayerInputSystem.Instance.playerAction.Player.Interact.performed -= interactable.Interact;
        }
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
}
