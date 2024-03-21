using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public static PlayerActionMap Actions;

    // Start is called before the first frame update
    void Awake()
    {
        Actions = new PlayerActionMap();

        Actions.Cheat.Enable();
        Actions.Swap.Enable();
    }

    public static void PlayerControllerEnable(PlayerController playerController)
    {
        #region Gamepad
        TurnBasedSystem.OnEnablePlayerInput += Actions.Gamepad.GamepadStrenght.Enable;
        TurnBasedSystem.OnEnablePlayerInput += Actions.Gamepad.ThrowPlayer.Enable;
        TurnBasedSystem.OnDisablePlayerInput += Actions.Gamepad.GamepadStrenght.Disable;
        TurnBasedSystem.OnDisablePlayerInput += Actions.Gamepad.ThrowPlayer.Disable;

        Actions.Gamepad.ThrowPlayer.started += playerController.GamepadStrengthGauge;
        Actions.Gamepad.ThrowPlayer.canceled += playerController.GamepadStrengthGauge;
        Actions.Gamepad.ThrowPlayer.canceled += playerController.Throw;
        Actions.Gamepad.GamepadStrenght.performed += playerController.GamepadDirection;
        Actions.Gamepad.GamepadStrenght.canceled += playerController.GamepadDirection;
        #endregion
        #region Mouse/Keyboard
        TurnBasedSystem.OnEnablePlayerInput += Actions.MouseKeyboard.MouseStartDrag.Enable;
        TurnBasedSystem.OnDisablePlayerInput += Actions.MouseKeyboard.MouseStartDrag.Disable;

        Actions.MouseKeyboard.MouseStrenght.performed += playerController.MouseStrenght;
        Actions.MouseKeyboard.MouseStartDrag.started += playerController.MouseStartDrag;
        Actions.MouseKeyboard.MouseStartDrag.performed += playerController.MouseStartDrag;
        Actions.MouseKeyboard.MouseStartDrag.canceled += playerController.MouseThrow;
        Actions.MouseKeyboard.MouseCancelThrow.performed += playerController.MouseCancelThrow;
        #endregion
    }

    public static void FreeCamEnable(PlayerFreeCam playerFreeCam)
    {
        Actions.Gamepad.FreeCam.performed += playerFreeCam.FreeCam;
        Actions.Gamepad.FreeCam.canceled += playerFreeCam.FreeCam;
        Actions.Gamepad.StartFreeCam.started += playerFreeCam.StartFreeCam;
        Actions.Gamepad.StartFreeCam.canceled += playerFreeCam.StartFreeCam;
        Actions.MouseKeyboard.FreeCam.performed += playerFreeCam.FreeCam;
        Actions.MouseKeyboard.StartFreeCam.started += playerFreeCam.StartFreeCam;
        Actions.MouseKeyboard.StartFreeCam.canceled += playerFreeCam.StartFreeCam;
    }

    public static void ReloadSceneEnable(ReloadScene reloadScene)
    {
        Actions.Cheat.ReloadScene.performed += reloadScene.Reload;
    }

    public static void NoClipEnable(NoClip noClip)
    {
        Actions.Cheat.NoClip.performed += noClip.NoClipMode;
        Actions.Cheat.NoClipControl.performed += noClip.MovePlayer;
        Actions.Cheat.NoClipControl.canceled += noClip.MovePlayer;
    }

    public static void PauseMenuEnable(PauseMenu pauseMenu)
    {
        Actions.Gamepad.PauseMenu.started += pauseMenu.PauseMenuState;
        Actions.MouseKeyboard.PauseMenu.started += pauseMenu.PauseMenuState;
    }

    public static void SwapEnable(SwapControls swapControls)
    {
        Actions.Swap.ToMouseKeyboard.started += swapControls.ToMouseKeyboard;
        Actions.Swap.ToGamepad.started += swapControls.ToGamepad;
    }

    public static void PlayerControllerDisable(PlayerController playerController)
    {
        #region Gamepad
        Actions.Gamepad.ThrowPlayer.started -= playerController.GamepadStrengthGauge;
        Actions.Gamepad.ThrowPlayer.canceled -= playerController.GamepadStrengthGauge;
        Actions.Gamepad.ThrowPlayer.canceled -= playerController.Throw;
        Actions.Gamepad.GamepadStrenght.performed -= playerController.GamepadDirection;
        Actions.Gamepad.GamepadStrenght.canceled -= playerController.GamepadDirection;
        #endregion
        #region Mouse/Keyboard
        Actions.MouseKeyboard.MouseStrenght.performed -= playerController.MouseStrenght;
        Actions.MouseKeyboard.MouseStartDrag.performed -= playerController.MouseStartDrag;
        Actions.MouseKeyboard.MouseStartDrag.canceled -= playerController.MouseStartDrag;
        Actions.MouseKeyboard.MouseCancelThrow.performed -= playerController.MouseCancelThrow;
        #endregion
    }

    public static void FreeCamDisable(PlayerFreeCam playerFreeCam)
    {
        Actions.Gamepad.FreeCam.performed -= playerFreeCam.FreeCam;
        Actions.Gamepad.FreeCam.canceled -= playerFreeCam.FreeCam;
        Actions.Gamepad.StartFreeCam.started -= playerFreeCam.StartFreeCam;
        Actions.Gamepad.StartFreeCam.canceled -= playerFreeCam.StartFreeCam;
        Actions.MouseKeyboard.FreeCam.performed -= playerFreeCam.FreeCam;
        Actions.MouseKeyboard.StartFreeCam.started -= playerFreeCam.StartFreeCam;
        Actions.MouseKeyboard.StartFreeCam.canceled -= playerFreeCam.StartFreeCam;
    }

    public static void ReloadSceneDisable(ReloadScene reloadScene)
    {
        Actions.Cheat.ReloadScene.performed -= reloadScene.Reload;
    }

    public static void NoClipDisable(NoClip noClip)
    {
        Actions.Cheat.NoClip.performed -= noClip.NoClipMode;
        Actions.Cheat.NoClipControl.performed -= noClip.MovePlayer;
        Actions.Cheat.NoClipControl.canceled -= noClip.MovePlayer;
    }

    public static void PauseMenuDisable(PauseMenu pauseMenu)
    {
        Actions.Gamepad.PauseMenu.started -= pauseMenu.PauseMenuState;
        Actions.MouseKeyboard.PauseMenu.started -= pauseMenu.PauseMenuState;
    }

    public static void SwapDisable(SwapControls swapControls)
    {
        Actions.Swap.ToMouseKeyboard.started -= swapControls.ToMouseKeyboard;
        Actions.Swap.ToGamepad.started -= swapControls.ToGamepad;
    }
}