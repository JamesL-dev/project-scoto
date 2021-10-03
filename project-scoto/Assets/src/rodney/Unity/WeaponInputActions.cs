// GENERATED AUTOMATICALLY FROM 'Assets/src/rodney/Unity/WeaponInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @WeaponInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @WeaponInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""WeaponInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""ff3e015a-7bf9-4eba-9c70-009f98e97ee6"",
            ""actions"": [
                {
                    ""name"": ""FireWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""9e538d25-1eef-44af-9dec-49dfdf9535fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""3db17be7-9f98-4d2c-b171-352158c95334"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""one"",
                    ""type"": ""Button"",
                    ""id"": ""979e847e-8f9b-49a3-8daa-fec58c325373"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""two"",
                    ""type"": ""Button"",
                    ""id"": ""f50224e3-7db7-4242-a0d2-25db239b4df5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""three"",
                    ""type"": ""Button"",
                    ""id"": ""9cff9bc0-d50b-4bc3-a448-e3b66e2cbf56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""four"",
                    ""type"": ""Button"",
                    ""id"": ""cc602b2c-dfc9-4a52-8427-c6552139f0cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DEBUGGING"",
                    ""type"": ""Button"",
                    ""id"": ""d9837a73-6adf-4a2b-8fe8-eb383bde259b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""15d3c645-ce53-4e1b-8627-cc0c2892aca2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44751eee-802c-4a0e-a325-bc3839360911"",
                    ""path"": ""*/{ScrollVertical}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""709ddf3b-833a-4eb1-8943-86e7cf3bde5a"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""one"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20b23d93-1ee3-4f41-8a7f-fcedadfb6675"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""two"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""712c755c-dc31-45ee-8f84-918363ab77c4"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""three"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b33ff137-d4a2-493a-9e98-c8957679710a"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""four"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1e49321-fd84-4646-bbe7-a92a027ff307"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DEBUGGING"",
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
        m_Player_FireWeapon = m_Player.FindAction("FireWeapon", throwIfNotFound: true);
        m_Player_ChangeWeapon = m_Player.FindAction("ChangeWeapon", throwIfNotFound: true);
        m_Player_one = m_Player.FindAction("one", throwIfNotFound: true);
        m_Player_two = m_Player.FindAction("two", throwIfNotFound: true);
        m_Player_three = m_Player.FindAction("three", throwIfNotFound: true);
        m_Player_four = m_Player.FindAction("four", throwIfNotFound: true);
        m_Player_DEBUGGING = m_Player.FindAction("DEBUGGING", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_FireWeapon;
    private readonly InputAction m_Player_ChangeWeapon;
    private readonly InputAction m_Player_one;
    private readonly InputAction m_Player_two;
    private readonly InputAction m_Player_three;
    private readonly InputAction m_Player_four;
    private readonly InputAction m_Player_DEBUGGING;
    public struct PlayerActions
    {
        private @WeaponInputActions m_Wrapper;
        public PlayerActions(@WeaponInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @FireWeapon => m_Wrapper.m_Player_FireWeapon;
        public InputAction @ChangeWeapon => m_Wrapper.m_Player_ChangeWeapon;
        public InputAction @one => m_Wrapper.m_Player_one;
        public InputAction @two => m_Wrapper.m_Player_two;
        public InputAction @three => m_Wrapper.m_Player_three;
        public InputAction @four => m_Wrapper.m_Player_four;
        public InputAction @DEBUGGING => m_Wrapper.m_Player_DEBUGGING;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @FireWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireWeapon;
                @FireWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireWeapon;
                @FireWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireWeapon;
                @ChangeWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChangeWeapon;
                @ChangeWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChangeWeapon;
                @ChangeWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChangeWeapon;
                @one.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOne;
                @one.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOne;
                @one.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOne;
                @two.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTwo;
                @two.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTwo;
                @two.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTwo;
                @three.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThree;
                @three.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThree;
                @three.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThree;
                @four.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFour;
                @four.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFour;
                @four.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFour;
                @DEBUGGING.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDEBUGGING;
                @DEBUGGING.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDEBUGGING;
                @DEBUGGING.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDEBUGGING;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FireWeapon.started += instance.OnFireWeapon;
                @FireWeapon.performed += instance.OnFireWeapon;
                @FireWeapon.canceled += instance.OnFireWeapon;
                @ChangeWeapon.started += instance.OnChangeWeapon;
                @ChangeWeapon.performed += instance.OnChangeWeapon;
                @ChangeWeapon.canceled += instance.OnChangeWeapon;
                @one.started += instance.OnOne;
                @one.performed += instance.OnOne;
                @one.canceled += instance.OnOne;
                @two.started += instance.OnTwo;
                @two.performed += instance.OnTwo;
                @two.canceled += instance.OnTwo;
                @three.started += instance.OnThree;
                @three.performed += instance.OnThree;
                @three.canceled += instance.OnThree;
                @four.started += instance.OnFour;
                @four.performed += instance.OnFour;
                @four.canceled += instance.OnFour;
                @DEBUGGING.started += instance.OnDEBUGGING;
                @DEBUGGING.performed += instance.OnDEBUGGING;
                @DEBUGGING.canceled += instance.OnDEBUGGING;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnFireWeapon(InputAction.CallbackContext context);
        void OnChangeWeapon(InputAction.CallbackContext context);
        void OnOne(InputAction.CallbackContext context);
        void OnTwo(InputAction.CallbackContext context);
        void OnThree(InputAction.CallbackContext context);
        void OnFour(InputAction.CallbackContext context);
        void OnDEBUGGING(InputAction.CallbackContext context);
    }
}
