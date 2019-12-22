// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Control/Gamepad/MasterControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class MasterControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public MasterControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MasterControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""a90845e3-035b-42c6-a6f0-551a5e93a3b7"",
            ""actions"": [
                {
                    ""name"": ""MoveAndSteer"",
                    ""type"": ""Button"",
                    ""id"": ""fdaf041a-6993-4f53-a982-87458662c125"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""41e23456-a6ed-44c4-b16d-66a807a387d4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockTarget"",
                    ""type"": ""Button"",
                    ""id"": ""466cf53c-10ee-4a35-a895-537c89c69825"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootGun"",
                    ""type"": ""Button"",
                    ""id"": ""ac16d76f-1cba-4d75-8921-e34e57a3df9c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomIn"",
                    ""type"": ""Button"",
                    ""id"": ""1a618f54-11ef-4b4e-bb0f-b17d9eb8f75c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomOut"",
                    ""type"": ""Button"",
                    ""id"": ""0233200c-27b6-4326-83a2-97086fa7a3b2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchCamera"",
                    ""type"": ""Button"",
                    ""id"": ""8197ed59-5f71-4138-8f07-baeb7d80328b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6f304450-b2d5-4ab0-84ee-0746b2cbbd9c"",
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
                    ""id"": ""6100cce1-c6c6-4a54-a1b5-f44e2990f4a0"",
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
                    ""id"": ""480477df-dacf-41e3-b7ed-24162b831316"",
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
                    ""id"": ""ca146781-eb4d-49c5-bfef-7c32f71f55d9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a59fbbaf-4ce9-427c-a11f-91b01e61ef5f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootGun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7702dc1-423b-48b4-a4f0-3e0504befef7"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a994151-33a7-4aca-b782-86d62a038c18"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""106c8c8b-83ca-4f80-adf3-bbacac028493"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchCamera"",
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
        m_Gameplay_Aim = m_Gameplay.FindAction("Aim", throwIfNotFound: true);
        m_Gameplay_LockTarget = m_Gameplay.FindAction("LockTarget", throwIfNotFound: true);
        m_Gameplay_ShootGun = m_Gameplay.FindAction("ShootGun", throwIfNotFound: true);
        m_Gameplay_ZoomIn = m_Gameplay.FindAction("ZoomIn", throwIfNotFound: true);
        m_Gameplay_ZoomOut = m_Gameplay.FindAction("ZoomOut", throwIfNotFound: true);
        m_Gameplay_SwitchCamera = m_Gameplay.FindAction("SwitchCamera", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_Aim;
    private readonly InputAction m_Gameplay_LockTarget;
    private readonly InputAction m_Gameplay_ShootGun;
    private readonly InputAction m_Gameplay_ZoomIn;
    private readonly InputAction m_Gameplay_ZoomOut;
    private readonly InputAction m_Gameplay_SwitchCamera;
    public struct GameplayActions
    {
        private MasterControls m_Wrapper;
        public GameplayActions(MasterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveAndSteer => m_Wrapper.m_Gameplay_MoveAndSteer;
        public InputAction @Aim => m_Wrapper.m_Gameplay_Aim;
        public InputAction @LockTarget => m_Wrapper.m_Gameplay_LockTarget;
        public InputAction @ShootGun => m_Wrapper.m_Gameplay_ShootGun;
        public InputAction @ZoomIn => m_Wrapper.m_Gameplay_ZoomIn;
        public InputAction @ZoomOut => m_Wrapper.m_Gameplay_ZoomOut;
        public InputAction @SwitchCamera => m_Wrapper.m_Gameplay_SwitchCamera;
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
                Aim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                Aim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                Aim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                LockTarget.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockTarget;
                LockTarget.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockTarget;
                LockTarget.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockTarget;
                ShootGun.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootGun;
                ShootGun.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootGun;
                ShootGun.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootGun;
                ZoomIn.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoomIn;
                ZoomIn.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoomIn;
                ZoomIn.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoomIn;
                ZoomOut.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoomOut;
                ZoomOut.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoomOut;
                ZoomOut.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoomOut;
                SwitchCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCamera;
                SwitchCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCamera;
                SwitchCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCamera;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                MoveAndSteer.started += instance.OnMoveAndSteer;
                MoveAndSteer.performed += instance.OnMoveAndSteer;
                MoveAndSteer.canceled += instance.OnMoveAndSteer;
                Aim.started += instance.OnAim;
                Aim.performed += instance.OnAim;
                Aim.canceled += instance.OnAim;
                LockTarget.started += instance.OnLockTarget;
                LockTarget.performed += instance.OnLockTarget;
                LockTarget.canceled += instance.OnLockTarget;
                ShootGun.started += instance.OnShootGun;
                ShootGun.performed += instance.OnShootGun;
                ShootGun.canceled += instance.OnShootGun;
                ZoomIn.started += instance.OnZoomIn;
                ZoomIn.performed += instance.OnZoomIn;
                ZoomIn.canceled += instance.OnZoomIn;
                ZoomOut.started += instance.OnZoomOut;
                ZoomOut.performed += instance.OnZoomOut;
                ZoomOut.canceled += instance.OnZoomOut;
                SwitchCamera.started += instance.OnSwitchCamera;
                SwitchCamera.performed += instance.OnSwitchCamera;
                SwitchCamera.canceled += instance.OnSwitchCamera;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMoveAndSteer(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnLockTarget(InputAction.CallbackContext context);
        void OnShootGun(InputAction.CallbackContext context);
        void OnZoomIn(InputAction.CallbackContext context);
        void OnZoomOut(InputAction.CallbackContext context);
        void OnSwitchCamera(InputAction.CallbackContext context);
    }
}
