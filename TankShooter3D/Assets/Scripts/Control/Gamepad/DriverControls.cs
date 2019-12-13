// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Control/Gamepad/DriverControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class DriverControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public DriverControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DriverControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""50f59684-294d-46be-9549-d3681be391e4"",
            ""actions"": [
                {
                    ""name"": ""MoveAndSteer"",
                    ""type"": ""Button"",
                    ""id"": ""ab2988d4-0db7-4629-a738-267553b7a930"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start shoot"",
                    ""type"": ""Button"",
                    ""id"": ""011b013c-0595-4b0c-8683-a6dcf5f02979"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""1ce7b666-cbbf-4bcd-8a2b-0c3a0491e7ab"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""End shoot"",
                    ""type"": ""Button"",
                    ""id"": ""be487e1d-a27d-4294-bf34-9b5a8103d725"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""817da4ff-410c-45ed-9fec-6e76a4a2b650"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAndSteer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05ca2493-4ec1-4485-802a-9407e870d2bb"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAndSteer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02146574-22c4-4c2d-892c-5deb8193488e"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b9b5553-b17e-45d1-b51f-c3a5ed20830c"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9219de7-cd9c-40ed-9476-756ae2d0404d"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""End shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MoveAndSteer = m_Gameplay.FindAction("MoveAndSteer", throwIfNotFound: true);
        m_Gameplay_Startshoot = m_Gameplay.FindAction("Start shoot", throwIfNotFound: true);
        m_Gameplay_Aim = m_Gameplay.FindAction("Aim", throwIfNotFound: true);
        m_Gameplay_Endshoot = m_Gameplay.FindAction("End shoot", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MoveAndSteer;
    private readonly InputAction m_Gameplay_Startshoot;
    private readonly InputAction m_Gameplay_Aim;
    private readonly InputAction m_Gameplay_Endshoot;
    public struct GameplayActions
    {
        private DriverControls m_Wrapper;
        public GameplayActions(DriverControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveAndSteer => m_Wrapper.m_Gameplay_MoveAndSteer;
        public InputAction @Startshoot => m_Wrapper.m_Gameplay_Startshoot;
        public InputAction @Aim => m_Wrapper.m_Gameplay_Aim;
        public InputAction @Endshoot => m_Wrapper.m_Gameplay_Endshoot;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                MoveAndSteer.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveAndSteer;
                MoveAndSteer.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveAndSteer;
                MoveAndSteer.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveAndSteer;
                Startshoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartshoot;
                Startshoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartshoot;
                Startshoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartshoot;
                Aim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                Aim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                Aim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                Endshoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndshoot;
                Endshoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndshoot;
                Endshoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndshoot;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                MoveAndSteer.started += instance.OnMoveAndSteer;
                MoveAndSteer.performed += instance.OnMoveAndSteer;
                MoveAndSteer.canceled += instance.OnMoveAndSteer;
                Startshoot.started += instance.OnStartshoot;
                Startshoot.performed += instance.OnStartshoot;
                Startshoot.canceled += instance.OnStartshoot;
                Aim.started += instance.OnAim;
                Aim.performed += instance.OnAim;
                Aim.canceled += instance.OnAim;
                Endshoot.started += instance.OnEndshoot;
                Endshoot.performed += instance.OnEndshoot;
                Endshoot.canceled += instance.OnEndshoot;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMoveAndSteer(InputAction.CallbackContext context);
        void OnStartshoot(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnEndshoot(InputAction.CallbackContext context);
    }
}
