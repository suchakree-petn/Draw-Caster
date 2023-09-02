//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSystem/PlayerAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""893352c2-ec85-4ca5-8e5a-74e1d7eff144"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f8aa1d96-bdfc-4fad-92aa-e1bf70bc016e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PressAttack"",
                    ""type"": ""Value"",
                    ""id"": ""0095340f-5efe-45c3-8007-3a73296fc224"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""HoldAttack"",
                    ""type"": ""Value"",
                    ""id"": ""61199413-6838-49bb-862a-00e8640f6673"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DrawInput"",
                    ""type"": ""Value"",
                    ""id"": ""b356e6ee-d978-4ecd-ac76-ed04e4745baa"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9e3ec70f-7e0b-481f-b820-b31b63420273"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0c8b1d67-c5f3-44f8-9073-e0d7d42b1c5d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""61a4e086-86c9-4554-8345-6a207dc3e4af"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""710482cc-0769-4c73-8e36-6e00191b81ef"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""734fe8bc-04eb-4202-a302-85c50f38cbe5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""64885308-9f28-4f9a-9c7c-480f6676d7bf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""beca9ec1-a4d4-496c-ab74-b4e7fe90fa1e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HoldAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f45b215b-ff34-4c35-bbb5-d27178b4c5a6"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrawInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_PressAttack = m_Player.FindAction("PressAttack", throwIfNotFound: true);
        m_Player_HoldAttack = m_Player.FindAction("HoldAttack", throwIfNotFound: true);
        m_Player_DrawInput = m_Player.FindAction("DrawInput", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_PressAttack;
    private readonly InputAction m_Player_HoldAttack;
    private readonly InputAction m_Player_DrawInput;
    public struct PlayerActions
    {
        private @PlayerAction m_Wrapper;
        public PlayerActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @PressAttack => m_Wrapper.m_Player_PressAttack;
        public InputAction @HoldAttack => m_Wrapper.m_Player_HoldAttack;
        public InputAction @DrawInput => m_Wrapper.m_Player_DrawInput;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @PressAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPressAttack;
                @PressAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPressAttack;
                @PressAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPressAttack;
                @HoldAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldAttack;
                @HoldAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldAttack;
                @HoldAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldAttack;
                @DrawInput.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrawInput;
                @DrawInput.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrawInput;
                @DrawInput.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrawInput;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @PressAttack.started += instance.OnPressAttack;
                @PressAttack.performed += instance.OnPressAttack;
                @PressAttack.canceled += instance.OnPressAttack;
                @HoldAttack.started += instance.OnHoldAttack;
                @HoldAttack.performed += instance.OnHoldAttack;
                @HoldAttack.canceled += instance.OnHoldAttack;
                @DrawInput.started += instance.OnDrawInput;
                @DrawInput.performed += instance.OnDrawInput;
                @DrawInput.canceled += instance.OnDrawInput;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnPressAttack(InputAction.CallbackContext context);
        void OnHoldAttack(InputAction.CallbackContext context);
        void OnDrawInput(InputAction.CallbackContext context);
    }
}
