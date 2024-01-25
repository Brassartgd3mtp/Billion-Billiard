//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/400_Scripts/410_Player/PlayerActionMap.inputactions
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

public partial class @PlayerActionMap: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActionMap"",
    ""maps"": [
        {
            ""name"": ""Gamepad"",
            ""id"": ""c866f438-9a08-4a10-b9f9-8d7cf7931380"",
            ""actions"": [
                {
                    ""name"": ""GamepadStrenght"",
                    ""type"": ""Value"",
                    ""id"": ""f96633d4-39bb-47a5-9995-ba42fe8c2439"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Throw Player"",
                    ""type"": ""Button"",
                    ""id"": ""f547db76-7812-46c1-9c7b-f21f8c3fe600"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FreeCam"",
                    ""type"": ""Value"",
                    ""id"": ""4989151e-72b5-48fe-bbb5-eea2d8a909f1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4f87ae14-2ef3-448a-b499-ad1e9610afd6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadStrenght"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cd52645-d479-4a58-8e92-f32b76a4e43f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73cceba9-c0f4-4b4a-bf45-bf3389db2508"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mouse/Keyboard"",
            ""id"": ""3a2aa13f-b26f-46be-8b8a-9da547179610"",
            ""actions"": [
                {
                    ""name"": ""MouseStartDrag"",
                    ""type"": ""Button"",
                    ""id"": ""5dc5b5a5-9f26-4cd6-91a4-a99171f3c6e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseStrenght"",
                    ""type"": ""Value"",
                    ""id"": ""0e48db39-acc7-46f5-824c-9fdc566444d6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""StartFreeCam"",
                    ""type"": ""Button"",
                    ""id"": ""9eec3fb6-c41e-42d7-aeda-34b17f7fe899"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseCancelThrow"",
                    ""type"": ""Button"",
                    ""id"": ""b6fedc0f-8e3f-4790-a0ed-66c9ab48b553"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FreeCam"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e745f-8798-44b8-a305-4352426b295d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""060298e0-395e-4f9e-b4d7-a25e5a31337d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseStartDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32627dc9-3601-40e5-8ff2-b6c41ee50bbf"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseStrenght"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""117826e2-d3e5-48f1-8d4b-4d9ed1fc166b"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartFreeCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a6cb2a4-6fc6-4f71-8751-0ff9827c193e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseCancelThrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a54f9ed7-e6d4-4870-8ea2-3011bddff833"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Cheat"",
            ""id"": ""0ccd1fb2-2384-4316-aa70-6f133026c6e2"",
            ""actions"": [
                {
                    ""name"": ""Reload Scene"",
                    ""type"": ""Button"",
                    ""id"": ""6e714593-840a-467e-91f6-ecbefae76fdd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""No-Clip"",
                    ""type"": ""Button"",
                    ""id"": ""9ed9e3d5-dbb1-4d59-9ba5-9b55a1220681"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""No-Clip Control"",
                    ""type"": ""Value"",
                    ""id"": ""e6a7a912-7270-407f-9efe-076ddbf7be8f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dc736d58-ed1b-4f32-b4bb-06a8a9ff11fd"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": ""MultiTap(tapCount=3)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload Scene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dc43dd1-5049-463b-9d00-df1220334d7c"",
                    ""path"": ""<Keyboard>/rightBracket"",
                    ""interactions"": ""MultiTap(tapCount=3)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload Scene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f418a2b6-7ac7-4d18-9692-0a4782b15491"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": ""MultiTap(tapCount=3)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""No-Clip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46b72e63-06b2-4264-920b-354c3e160e83"",
                    ""path"": ""<Keyboard>/backslash"",
                    ""interactions"": ""MultiTap(tapCount=3)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""No-Clip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02ac39ac-505b-4e67-a967-304219bdc89c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""No-Clip Control"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0915eb11-52e2-47a6-bcd8-b653c2eefbe5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""No-Clip Control"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d98d4bf8-25ba-49f8-bdc1-32d9cf766059"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""No-Clip Control"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""53e80e15-d561-45cc-b8c2-0165c7654de9"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""No-Clip Control"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""30eb8abe-bcd4-4f39-a7ba-f5f9d0eb9719"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""No-Clip Control"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d16c8bec-fd07-462d-a3b3-7019b2d84ca7"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""No-Clip Control"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""MainMenu"",
            ""id"": ""4e3b26f7-b632-45cd-be3e-0f0ff854419b"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""70762f9f-2db8-40ae-8467-3ea717f788b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""38eb72fd-132e-4122-aa42-ef8a88d701db"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gamepad
        m_Gamepad = asset.FindActionMap("Gamepad", throwIfNotFound: true);
        m_Gamepad_GamepadStrenght = m_Gamepad.FindAction("GamepadStrenght", throwIfNotFound: true);
        m_Gamepad_ThrowPlayer = m_Gamepad.FindAction("Throw Player", throwIfNotFound: true);
        m_Gamepad_FreeCam = m_Gamepad.FindAction("FreeCam", throwIfNotFound: true);
        // Mouse/Keyboard
        m_MouseKeyboard = asset.FindActionMap("Mouse/Keyboard", throwIfNotFound: true);
        m_MouseKeyboard_MouseStartDrag = m_MouseKeyboard.FindAction("MouseStartDrag", throwIfNotFound: true);
        m_MouseKeyboard_MouseStrenght = m_MouseKeyboard.FindAction("MouseStrenght", throwIfNotFound: true);
        m_MouseKeyboard_StartFreeCam = m_MouseKeyboard.FindAction("StartFreeCam", throwIfNotFound: true);
        m_MouseKeyboard_MouseCancelThrow = m_MouseKeyboard.FindAction("MouseCancelThrow", throwIfNotFound: true);
        m_MouseKeyboard_FreeCam = m_MouseKeyboard.FindAction("FreeCam", throwIfNotFound: true);
        // Cheat
        m_Cheat = asset.FindActionMap("Cheat", throwIfNotFound: true);
        m_Cheat_ReloadScene = m_Cheat.FindAction("Reload Scene", throwIfNotFound: true);
        m_Cheat_NoClip = m_Cheat.FindAction("No-Clip", throwIfNotFound: true);
        m_Cheat_NoClipControl = m_Cheat.FindAction("No-Clip Control", throwIfNotFound: true);
        // MainMenu
        m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
        m_MainMenu_Newaction = m_MainMenu.FindAction("New action", throwIfNotFound: true);
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

    // Gamepad
    private readonly InputActionMap m_Gamepad;
    private List<IGamepadActions> m_GamepadActionsCallbackInterfaces = new List<IGamepadActions>();
    private readonly InputAction m_Gamepad_GamepadStrenght;
    private readonly InputAction m_Gamepad_ThrowPlayer;
    private readonly InputAction m_Gamepad_FreeCam;
    public struct GamepadActions
    {
        private @PlayerActionMap m_Wrapper;
        public GamepadActions(@PlayerActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @GamepadStrenght => m_Wrapper.m_Gamepad_GamepadStrenght;
        public InputAction @ThrowPlayer => m_Wrapper.m_Gamepad_ThrowPlayer;
        public InputAction @FreeCam => m_Wrapper.m_Gamepad_FreeCam;
        public InputActionMap Get() { return m_Wrapper.m_Gamepad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamepadActions set) { return set.Get(); }
        public void AddCallbacks(IGamepadActions instance)
        {
            if (instance == null || m_Wrapper.m_GamepadActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GamepadActionsCallbackInterfaces.Add(instance);
            @GamepadStrenght.started += instance.OnGamepadStrenght;
            @GamepadStrenght.performed += instance.OnGamepadStrenght;
            @GamepadStrenght.canceled += instance.OnGamepadStrenght;
            @ThrowPlayer.started += instance.OnThrowPlayer;
            @ThrowPlayer.performed += instance.OnThrowPlayer;
            @ThrowPlayer.canceled += instance.OnThrowPlayer;
            @FreeCam.started += instance.OnFreeCam;
            @FreeCam.performed += instance.OnFreeCam;
            @FreeCam.canceled += instance.OnFreeCam;
        }

        private void UnregisterCallbacks(IGamepadActions instance)
        {
            @GamepadStrenght.started -= instance.OnGamepadStrenght;
            @GamepadStrenght.performed -= instance.OnGamepadStrenght;
            @GamepadStrenght.canceled -= instance.OnGamepadStrenght;
            @ThrowPlayer.started -= instance.OnThrowPlayer;
            @ThrowPlayer.performed -= instance.OnThrowPlayer;
            @ThrowPlayer.canceled -= instance.OnThrowPlayer;
            @FreeCam.started -= instance.OnFreeCam;
            @FreeCam.performed -= instance.OnFreeCam;
            @FreeCam.canceled -= instance.OnFreeCam;
        }

        public void RemoveCallbacks(IGamepadActions instance)
        {
            if (m_Wrapper.m_GamepadActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGamepadActions instance)
        {
            foreach (var item in m_Wrapper.m_GamepadActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GamepadActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GamepadActions @Gamepad => new GamepadActions(this);

    // Mouse/Keyboard
    private readonly InputActionMap m_MouseKeyboard;
    private List<IMouseKeyboardActions> m_MouseKeyboardActionsCallbackInterfaces = new List<IMouseKeyboardActions>();
    private readonly InputAction m_MouseKeyboard_MouseStartDrag;
    private readonly InputAction m_MouseKeyboard_MouseStrenght;
    private readonly InputAction m_MouseKeyboard_StartFreeCam;
    private readonly InputAction m_MouseKeyboard_MouseCancelThrow;
    private readonly InputAction m_MouseKeyboard_FreeCam;
    public struct MouseKeyboardActions
    {
        private @PlayerActionMap m_Wrapper;
        public MouseKeyboardActions(@PlayerActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseStartDrag => m_Wrapper.m_MouseKeyboard_MouseStartDrag;
        public InputAction @MouseStrenght => m_Wrapper.m_MouseKeyboard_MouseStrenght;
        public InputAction @StartFreeCam => m_Wrapper.m_MouseKeyboard_StartFreeCam;
        public InputAction @MouseCancelThrow => m_Wrapper.m_MouseKeyboard_MouseCancelThrow;
        public InputAction @FreeCam => m_Wrapper.m_MouseKeyboard_FreeCam;
        public InputActionMap Get() { return m_Wrapper.m_MouseKeyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseKeyboardActions set) { return set.Get(); }
        public void AddCallbacks(IMouseKeyboardActions instance)
        {
            if (instance == null || m_Wrapper.m_MouseKeyboardActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MouseKeyboardActionsCallbackInterfaces.Add(instance);
            @MouseStartDrag.started += instance.OnMouseStartDrag;
            @MouseStartDrag.performed += instance.OnMouseStartDrag;
            @MouseStartDrag.canceled += instance.OnMouseStartDrag;
            @MouseStrenght.started += instance.OnMouseStrenght;
            @MouseStrenght.performed += instance.OnMouseStrenght;
            @MouseStrenght.canceled += instance.OnMouseStrenght;
            @StartFreeCam.started += instance.OnStartFreeCam;
            @StartFreeCam.performed += instance.OnStartFreeCam;
            @StartFreeCam.canceled += instance.OnStartFreeCam;
            @MouseCancelThrow.started += instance.OnMouseCancelThrow;
            @MouseCancelThrow.performed += instance.OnMouseCancelThrow;
            @MouseCancelThrow.canceled += instance.OnMouseCancelThrow;
            @FreeCam.started += instance.OnFreeCam;
            @FreeCam.performed += instance.OnFreeCam;
            @FreeCam.canceled += instance.OnFreeCam;
        }

        private void UnregisterCallbacks(IMouseKeyboardActions instance)
        {
            @MouseStartDrag.started -= instance.OnMouseStartDrag;
            @MouseStartDrag.performed -= instance.OnMouseStartDrag;
            @MouseStartDrag.canceled -= instance.OnMouseStartDrag;
            @MouseStrenght.started -= instance.OnMouseStrenght;
            @MouseStrenght.performed -= instance.OnMouseStrenght;
            @MouseStrenght.canceled -= instance.OnMouseStrenght;
            @StartFreeCam.started -= instance.OnStartFreeCam;
            @StartFreeCam.performed -= instance.OnStartFreeCam;
            @StartFreeCam.canceled -= instance.OnStartFreeCam;
            @MouseCancelThrow.started -= instance.OnMouseCancelThrow;
            @MouseCancelThrow.performed -= instance.OnMouseCancelThrow;
            @MouseCancelThrow.canceled -= instance.OnMouseCancelThrow;
            @FreeCam.started -= instance.OnFreeCam;
            @FreeCam.performed -= instance.OnFreeCam;
            @FreeCam.canceled -= instance.OnFreeCam;
        }

        public void RemoveCallbacks(IMouseKeyboardActions instance)
        {
            if (m_Wrapper.m_MouseKeyboardActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMouseKeyboardActions instance)
        {
            foreach (var item in m_Wrapper.m_MouseKeyboardActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MouseKeyboardActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MouseKeyboardActions @MouseKeyboard => new MouseKeyboardActions(this);

    // Cheat
    private readonly InputActionMap m_Cheat;
    private List<ICheatActions> m_CheatActionsCallbackInterfaces = new List<ICheatActions>();
    private readonly InputAction m_Cheat_ReloadScene;
    private readonly InputAction m_Cheat_NoClip;
    private readonly InputAction m_Cheat_NoClipControl;
    public struct CheatActions
    {
        private @PlayerActionMap m_Wrapper;
        public CheatActions(@PlayerActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @ReloadScene => m_Wrapper.m_Cheat_ReloadScene;
        public InputAction @NoClip => m_Wrapper.m_Cheat_NoClip;
        public InputAction @NoClipControl => m_Wrapper.m_Cheat_NoClipControl;
        public InputActionMap Get() { return m_Wrapper.m_Cheat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatActions set) { return set.Get(); }
        public void AddCallbacks(ICheatActions instance)
        {
            if (instance == null || m_Wrapper.m_CheatActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CheatActionsCallbackInterfaces.Add(instance);
            @ReloadScene.started += instance.OnReloadScene;
            @ReloadScene.performed += instance.OnReloadScene;
            @ReloadScene.canceled += instance.OnReloadScene;
            @NoClip.started += instance.OnNoClip;
            @NoClip.performed += instance.OnNoClip;
            @NoClip.canceled += instance.OnNoClip;
            @NoClipControl.started += instance.OnNoClipControl;
            @NoClipControl.performed += instance.OnNoClipControl;
            @NoClipControl.canceled += instance.OnNoClipControl;
        }

        private void UnregisterCallbacks(ICheatActions instance)
        {
            @ReloadScene.started -= instance.OnReloadScene;
            @ReloadScene.performed -= instance.OnReloadScene;
            @ReloadScene.canceled -= instance.OnReloadScene;
            @NoClip.started -= instance.OnNoClip;
            @NoClip.performed -= instance.OnNoClip;
            @NoClip.canceled -= instance.OnNoClip;
            @NoClipControl.started -= instance.OnNoClipControl;
            @NoClipControl.performed -= instance.OnNoClipControl;
            @NoClipControl.canceled -= instance.OnNoClipControl;
        }

        public void RemoveCallbacks(ICheatActions instance)
        {
            if (m_Wrapper.m_CheatActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICheatActions instance)
        {
            foreach (var item in m_Wrapper.m_CheatActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CheatActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CheatActions @Cheat => new CheatActions(this);

    // MainMenu
    private readonly InputActionMap m_MainMenu;
    private List<IMainMenuActions> m_MainMenuActionsCallbackInterfaces = new List<IMainMenuActions>();
    private readonly InputAction m_MainMenu_Newaction;
    public struct MainMenuActions
    {
        private @PlayerActionMap m_Wrapper;
        public MainMenuActions(@PlayerActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_MainMenu_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void AddCallbacks(IMainMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MainMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainMenuActionsCallbackInterfaces.Add(instance);
            @Newaction.started += instance.OnNewaction;
            @Newaction.performed += instance.OnNewaction;
            @Newaction.canceled += instance.OnNewaction;
        }

        private void UnregisterCallbacks(IMainMenuActions instance)
        {
            @Newaction.started -= instance.OnNewaction;
            @Newaction.performed -= instance.OnNewaction;
            @Newaction.canceled -= instance.OnNewaction;
        }

        public void RemoveCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MainMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);
    public interface IGamepadActions
    {
        void OnGamepadStrenght(InputAction.CallbackContext context);
        void OnThrowPlayer(InputAction.CallbackContext context);
        void OnFreeCam(InputAction.CallbackContext context);
    }
    public interface IMouseKeyboardActions
    {
        void OnMouseStartDrag(InputAction.CallbackContext context);
        void OnMouseStrenght(InputAction.CallbackContext context);
        void OnStartFreeCam(InputAction.CallbackContext context);
        void OnMouseCancelThrow(InputAction.CallbackContext context);
        void OnFreeCam(InputAction.CallbackContext context);
    }
    public interface ICheatActions
    {
        void OnReloadScene(InputAction.CallbackContext context);
        void OnNoClip(InputAction.CallbackContext context);
        void OnNoClipControl(InputAction.CallbackContext context);
    }
    public interface IMainMenuActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
