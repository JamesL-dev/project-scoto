// GENERATED AUTOMATICALLY FROM 'Assets/src/hayden/FlashlightInputActions.inputactions'

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
            ""id"": ""2aa555b4-4c3a-4906-bd88-5f05366396cb"",
            ""actions"": [
                {
                    ""name"": ""ToggleFlashlight"",
                    ""type"": ""Button"",
                    ""id"": ""637d47c9-b33c-4cf2-9ffc-850a01c91f1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleFocus"",
                    ""type"": ""Button"",
                    ""id"": ""b3cff011-0c41-467f-b827-13ba4bd3aab8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b38c56c9-b31e-4ac8-b04c-ec3eb2d3269c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleFlashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c41b56e3-a24d-4b3a-9169-f848ea3fb5c9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleFocus"",
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
        m_Player_ToggleFlashlight = m_Player.FindAction("ToggleFlashlight", throwIfNotFound: true);
        m_Player_ToggleFocus = m_Player.FindAction("ToggleFocus", throwIfNotFound: true);
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
    private readonly InputAction m_Player_ToggleFlashlight;
    private readonly InputAction m_Player_ToggleFocus;
    public struct PlayerActions
    {
        private @FlashlightInputActions m_Wrapper;
        public PlayerActions(@FlashlightInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleFlashlight => m_Wrapper.m_Player_ToggleFlashlight;
        public InputAction @ToggleFocus => m_Wrapper.m_Player_ToggleFocus;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @ToggleFlashlight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFlashlight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFlashlight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFocus.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFocus;
                @ToggleFocus.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFocus;
                @ToggleFocus.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFocus;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleFlashlight.started += instance.OnToggleFlashlight;
                @ToggleFlashlight.performed += instance.OnToggleFlashlight;
                @ToggleFlashlight.canceled += instance.OnToggleFlashlight;
                @ToggleFocus.started += instance.OnToggleFocus;
                @ToggleFocus.performed += instance.OnToggleFocus;
                @ToggleFocus.canceled += instance.OnToggleFocus;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnToggleFlashlight(InputAction.CallbackContext context);
        void OnToggleFocus(InputAction.CallbackContext context);
    }
}
