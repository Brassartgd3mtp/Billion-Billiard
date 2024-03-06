using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public PlayerActionMap Actions;

    private PlayerController playerController;
    private PlayerFreeCam playerFreeCam;
    private ReloadScene reloadScene;
    private NoClip noClip;
    private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Awake()
    {
        Actions = new PlayerActionMap();

        if (gameObject.TryGetComponent(out PlayerController playerController))
        {
            this.playerController = playerController;
            #region Gamepad
            TurnBasedSystem.OnEnablePlayerInput += Actions.Gamepad.GamepadStrenght.Enable;
            TurnBasedSystem.OnEnablePlayerInput += Actions.Gamepad.ThrowPlayer.Enable;
            TurnBasedSystem.OnDisablePlayerInput += Actions.Gamepad.GamepadStrenght.Disable;
            TurnBasedSystem.OnDisablePlayerInput += Actions.Gamepad.ThrowPlayer.Disable;

            Actions.Gamepad.ThrowPlayer.performed += playerController.Throw;
            Actions.Gamepad.GamepadStrenght.performed += playerController.GamepadStrenght;
            Actions.Gamepad.GamepadStrenght.canceled += playerController.GamepadStrenght;
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

        if (gameObject.TryGetComponent(out PlayerFreeCam playerFreeCam))
        {
            this.playerFreeCam = playerFreeCam;
            Actions.Gamepad.FreeCam.performed += playerFreeCam.FreeCam;
            Actions.Gamepad.FreeCam.canceled += playerFreeCam.FreeCam;
            Actions.Gamepad.StartFreeCam.started += playerFreeCam.StartFreeCam;
            Actions.Gamepad.StartFreeCam.canceled += playerFreeCam.StartFreeCam;
            Actions.MouseKeyboard.FreeCam.performed += playerFreeCam.FreeCam;
            Actions.MouseKeyboard.StartFreeCam.started += playerFreeCam.StartFreeCam;
            Actions.MouseKeyboard.StartFreeCam.canceled += playerFreeCam.StartFreeCam;
        }

        if (gameObject.TryGetComponent(out ReloadScene reloadScene))
        {
            this.reloadScene = reloadScene;
            Actions.Cheat.ReloadScene.performed += reloadScene.Reload;
        }

        if (gameObject.TryGetComponent(out NoClip noClip))
        {
            this.noClip = noClip;
            Actions.Cheat.NoClip.performed += NoClipMode;
            Actions.Cheat.NoClipControl.performed += noClip.MovePlayer;
            Actions.Cheat.NoClipControl.canceled += noClip.MovePlayer;
        }

        if (gameObject.TryGetComponent(out PauseMenu pauseMenu))
        {
            this.pauseMenu = pauseMenu;
            Actions.Gamepad.PauseMenu.started += pauseMenu.PauseMenuState;
            Actions.MouseKeyboard.PauseMenu.started += pauseMenu.PauseMenuState;
        }

        Actions.Gamepad.Enable();
        if (Gamepad.current == null)
            Actions.MouseKeyboard.Enable();
        Actions.Cheat.Enable();
    }

    private void NoClipMode(InputAction.CallbackContext context)
    {
        if (!noClip.ModeOn)
        {
            noClip.ModeOn = true;
            Actions.Gamepad.Disable();
            Actions.MouseKeyboard.Disable();
            noClip.PlayerCollider.enabled = false;
        }
        else
        {
            noClip.ModeOn = false;
            Actions.Gamepad.Enable();
            Actions.MouseKeyboard.Enable();
            noClip.PlayerCollider.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            #region Gamepad
            Actions.Gamepad.ThrowPlayer.performed -= playerController.Throw;
            Actions.Gamepad.GamepadStrenght.performed -= playerController.GamepadStrenght;
            Actions.Gamepad.GamepadStrenght.canceled -= playerController.GamepadStrenght;
            #endregion
            #region Mouse/Keyboard
            Actions.MouseKeyboard.MouseStrenght.performed -= playerController.MouseStrenght;
            Actions.MouseKeyboard.MouseStartDrag.performed -= playerController.MouseStartDrag;
            Actions.MouseKeyboard.MouseStartDrag.canceled -= playerController.MouseStartDrag;
            Actions.MouseKeyboard.MouseCancelThrow.performed -= playerController.MouseCancelThrow;
            #endregion
        }

        if (playerFreeCam != null)
        {
            Actions.Gamepad.FreeCam.performed -= playerFreeCam.FreeCam;
            Actions.Gamepad.FreeCam.canceled -= playerFreeCam.FreeCam;
            Actions.Gamepad.StartFreeCam.started -= playerFreeCam.StartFreeCam;
            Actions.Gamepad.StartFreeCam.canceled -= playerFreeCam.StartFreeCam;
            Actions.MouseKeyboard.FreeCam.performed -= playerFreeCam.FreeCam;
            Actions.MouseKeyboard.StartFreeCam.started -= playerFreeCam.StartFreeCam;
            Actions.MouseKeyboard.StartFreeCam.canceled -= playerFreeCam.StartFreeCam;
        }

        if (reloadScene != null)
        {
            Actions.Cheat.ReloadScene.performed -= reloadScene.Reload;
        }

        if (noClip != null)
        {
            Actions.Cheat.NoClip.performed -= NoClipMode;
            Actions.Cheat.NoClipControl.performed -= noClip.MovePlayer;
            Actions.Cheat.NoClipControl.canceled -= noClip.MovePlayer;
        }

        if (pauseMenu != null)
        {
            Actions.Gamepad.PauseMenu.started -= pauseMenu.PauseMenuState;
            Actions.MouseKeyboard.PauseMenu.started -= pauseMenu.PauseMenuState;
        }
    }
}
