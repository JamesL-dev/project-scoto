// GENERATED AUTOMATICALLY FROM 'Assets/prefabs/hayden/FlashlightInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @FlashlightInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @FlashlightInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""FlashlightInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""8678eb30-e476-477c-a1fb-119f5e5734af"",
            ""actions"": [
                {
                    ""name"": ""FocusFlashlight"",
                    ""type"": ""Button"",
                    ""id"": ""99a98d67-6474-4a14-8c0f-7f8831e6572f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NormalFlashlight"",
                    ""type"": ""Button"",
                    ""id"": ""f598b6ee-5ec6-4aa5-8850-5a551c47e346"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleFlashlight"",
                    ""type"": ""Button"",
                    ""id"": ""a0c94b44-c9ee-4d98-8d4d-e44b69e41283"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ecb19792-29c9-43cb-a2e4-b5ac3a17f268"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FocusFlashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6e44471-c2d7-442c-814a-4328d1ff2b9f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalFlashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60cf76c5-6f46-45d5-b121-26e279645e55"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleFlashlight"",
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
        m_Player_FocusFlashlight = m_Player.FindAction("FocusFlashlight", throwIfNotFound: true);
        m_Player_NormalFlashlight = m_Player.FindAction("NormalFlashlight", throwIfNotFound: true);
        m_Player_ToggleFlashlight = m_Player.FindAction("ToggleFlashlight", throwIfNotFound: true);
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
    private readonly InputAction m_Player_FocusFlashlight;
    private readonly InputAction m_Player_NormalFlashlight;
    private readonly InputAction m_Player_ToggleFlashlight;
    public struct PlayerActions
    {
        private @FlashlightInputActions m_Wrapper;
        public PlayerActions(@FlashlightInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @FocusFlashlight => m_Wrapper.m_Player_FocusFlashlight;
        public InputAction @NormalFlashlight => m_Wrapper.m_Player_NormalFlashlight;
        public InputAction @ToggleFlashlight => m_Wrapper.m_Player_ToggleFlashlight;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @FocusFlashlight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFocusFlashlight;
                @FocusFlashlight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFocusFlashlight;
                @FocusFlashlight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFocusFlashlight;
                @NormalFlashlight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNormalFlashlight;
                @NormalFlashlight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNormalFlashlight;
                @NormalFlashlight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNormalFlashlight;
                @ToggleFlashlight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFlashlight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFlashlight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FocusFlashlight.started += instance.OnFocusFlashlight;
                @FocusFlashlight.performed += instance.OnFocusFlashlight;
                @FocusFlashlight.canceled += instance.OnFocusFlashlight;
                @NormalFlashlight.started += instance.OnNormalFlashlight;
                @NormalFlashlight.performed += instance.OnNormalFlashlight;
                @NormalFlashlight.canceled += instance.OnNormalFlashlight;
                @ToggleFlashlight.started += instance.OnToggleFlashlight;
                @ToggleFlashlight.performed += instance.OnToggleFlashlight;
                @ToggleFlashlight.canceled += instance.OnToggleFlashlight;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnFocusFlashlight(InputAction.CallbackContext context);
        void OnNormalFlashlight(InputAction.CallbackContext context);
        void OnToggleFlashlight(InputAction.CallbackContext context);
    }
}
