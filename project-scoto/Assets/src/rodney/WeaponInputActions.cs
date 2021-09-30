// GENERATED AUTOMATICALLY FROM 'Assets/src/rodney/WeaponInputActions.inputactions'

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
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_FireWeapon = m_Player.FindAction("FireWeapon", throwIfNotFound: true);
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
    public struct PlayerActions
    {
        private @WeaponInputActions m_Wrapper;
        public PlayerActions(@WeaponInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @FireWeapon => m_Wrapper.m_Player_FireWeapon;
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
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FireWeapon.started += instance.OnFireWeapon;
                @FireWeapon.performed += instance.OnFireWeapon;
                @FireWeapon.canceled += instance.OnFireWeapon;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnFireWeapon(InputAction.CallbackContext context);
    }
}
