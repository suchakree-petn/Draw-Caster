using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public interface IInteractable
{
    public void ShowInteractUI(){}
    public void HideInteractUI(){}
    public void Interact(InputAction.CallbackContext context){}
}
