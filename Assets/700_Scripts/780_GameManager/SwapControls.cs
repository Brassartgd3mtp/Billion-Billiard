using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwapControls : MonoBehaviour
{
    public static CurrentState state = CurrentState.MouseKeyboard;

    [SerializeField] private UI_ControllerSwitch uiControllerSwitch;

    private void Start()
    {
        InputHandler.SwapEnable(this);

        //if (Gamepad.current != null)
        //    ToGamepad();
    }

    void ToGamepad()
    {
        InputHandler.Actions.Swap.ToMouseKeyboard.Enable();

        InputHandler.Actions.Gamepad.Enable();
        InputHandler.Actions.MouseKeyboard.Disable();

        InputHandler.Actions.Swap.ToGamepad.Disable();

        state = CurrentState.Gamepad;

        uiControllerSwitch.GamepadIcon();

        Debug.LogWarning("Switch main controller to Gamepad !");
    }

    public void ToGamepad(InputAction.CallbackContext context)
    {
        ToGamepad();
    }

    public void ToMouseKeyboard(InputAction.CallbackContext context)
    {
        InputHandler.Actions.Swap.ToGamepad.Enable();

        InputHandler.Actions.MouseKeyboard.Enable();
        InputHandler.Actions.Gamepad.Disable();

        InputHandler.Actions.Swap.ToMouseKeyboard.Disable();

        state = CurrentState.MouseKeyboard;

        uiControllerSwitch.MouseIcon();

        Debug.LogWarning("Switch main controller to Mouse and Keyboard !");
    }

    private void OnDisable()
    {
        InputHandler.SwapDisable();
    }
}

public enum CurrentState
{
    MouseKeyboard,
    Gamepad
}