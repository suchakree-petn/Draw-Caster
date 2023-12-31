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
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Spell_Q"",
                    ""type"": ""Button"",
                    ""id"": ""7f2eda01-205f-4374-8f3c-6807301ee2f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Spell_E"",
                    ""type"": ""Button"",
                    ""id"": ""cd72ba59-7c1b-495b-9872-86fd1a2bc372"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Spell_R"",
                    ""type"": ""Button"",
                    ""id"": ""106c03a1-cbe4-4d48-8e99-32a719cd2abd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Spell_Shift"",
                    ""type"": ""Button"",
                    ""id"": ""6773be5f-3267-46a0-8894-a2d31228795d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""eade938d-7365-46ee-ab38-4f198d3f9cab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""528028ec-fac6-410a-9ddb-54051ee56857"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ManaNullify"",
                    ""type"": ""Value"",
                    ""id"": ""fabd6ada-459e-4ad9-9995-93e75491bb8a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1,pressPoint=0.1)"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""5cb337bd-e993-4ac4-ba23-5475e407d2d9"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spell_Q"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffe5122d-467b-4c6d-9da4-7838252fae46"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spell_E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d16edf5b-7871-4784-8730-b656df099ec7"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spell_R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c77b9cd-d232-4629-abc7-17328dc55612"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spell_Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ba444ea-d93e-4e7b-bf16-70eabda26a33"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71220ec3-faf2-4105-be12-c2f657b4ad60"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c39a3ee-af97-42f9-bb74-143f7fdf545d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ManaNullify"",
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
        m_Player_Spell_Q = m_Player.FindAction("Spell_Q", throwIfNotFound: true);
        m_Player_Spell_E = m_Player.FindAction("Spell_E", throwIfNotFound: true);
        m_Player_Spell_R = m_Player.FindAction("Spell_R", throwIfNotFound: true);
        m_Player_Spell_Shift = m_Player.FindAction("Spell_Shift", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_LeftClick = m_Player.FindAction("LeftClick", throwIfNotFound: true);
        m_Player_ManaNullify = m_Player.FindAction("ManaNullify", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Spell_Q;
    private readonly InputAction m_Player_Spell_E;
    private readonly InputAction m_Player_Spell_R;
    private readonly InputAction m_Player_Spell_Shift;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_LeftClick;
    private readonly InputAction m_Player_ManaNullify;
    public struct PlayerActions
    {
        private @PlayerAction m_Wrapper;
        public PlayerActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @PressAttack => m_Wrapper.m_Player_PressAttack;
        public InputAction @HoldAttack => m_Wrapper.m_Player_HoldAttack;
        public InputAction @DrawInput => m_Wrapper.m_Player_DrawInput;
        public InputAction @Spell_Q => m_Wrapper.m_Player_Spell_Q;
        public InputAction @Spell_E => m_Wrapper.m_Player_Spell_E;
        public InputAction @Spell_R => m_Wrapper.m_Player_Spell_R;
        public InputAction @Spell_Shift => m_Wrapper.m_Player_Spell_Shift;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @LeftClick => m_Wrapper.m_Player_LeftClick;
        public InputAction @ManaNullify => m_Wrapper.m_Player_ManaNullify;
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
                @Spell_Q.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_Q;
                @Spell_Q.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_Q;
                @Spell_Q.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_Q;
                @Spell_E.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_E;
                @Spell_E.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_E;
                @Spell_E.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_E;
                @Spell_R.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_R;
                @Spell_R.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_R;
                @Spell_R.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_R;
                @Spell_Shift.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_Shift;
                @Spell_Shift.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_Shift;
                @Spell_Shift.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpell_Shift;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @LeftClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @ManaNullify.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnManaNullify;
                @ManaNullify.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnManaNullify;
                @ManaNullify.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnManaNullify;
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
                @Spell_Q.started += instance.OnSpell_Q;
                @Spell_Q.performed += instance.OnSpell_Q;
                @Spell_Q.canceled += instance.OnSpell_Q;
                @Spell_E.started += instance.OnSpell_E;
                @Spell_E.performed += instance.OnSpell_E;
                @Spell_E.canceled += instance.OnSpell_E;
                @Spell_R.started += instance.OnSpell_R;
                @Spell_R.performed += instance.OnSpell_R;
                @Spell_R.canceled += instance.OnSpell_R;
                @Spell_Shift.started += instance.OnSpell_Shift;
                @Spell_Shift.performed += instance.OnSpell_Shift;
                @Spell_Shift.canceled += instance.OnSpell_Shift;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @ManaNullify.started += instance.OnManaNullify;
                @ManaNullify.performed += instance.OnManaNullify;
                @ManaNullify.canceled += instance.OnManaNullify;
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
        void OnSpell_Q(InputAction.CallbackContext context);
        void OnSpell_E(InputAction.CallbackContext context);
        void OnSpell_R(InputAction.CallbackContext context);
        void OnSpell_Shift(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnManaNullify(InputAction.CallbackContext context);
    }
}
