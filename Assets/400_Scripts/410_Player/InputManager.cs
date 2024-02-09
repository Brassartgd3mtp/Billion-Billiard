using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerActionMap actions;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerFreeCam playerFreeCam;
    [SerializeField] private ReloadScene reloadScene;
    [SerializeField] private NoClip noClip;
    // Start is called before the first frame update
    void Awake()
    {
        actions = new PlayerActionMap();

        if (playerController != null)
        {
            #region Gamepad
            TurnBasedSystem.OnEnablePlayerInput += actions.Gamepad.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.Gamepad.Disable;

            actions.Gamepad.ThrowPlayer.performed += playerController.Throw;
            actions.Gamepad.GamepadStrenght.performed += playerController.GamepadStrenght;
            actions.Gamepad.GamepadStrenght.canceled += playerController.GamepadStrenght;
            #endregion
            #region Mouse/Keyboard
            TurnBasedSystem.OnEnablePlayerInput += actions.MouseKeyboard.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.MouseKeyboard.Disable;

            actions.MouseKeyboard.MouseStrenght.performed += playerController.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed += playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled += playerController.MouseThrow;
            actions.MouseKeyboard.MouseCancelThrow.performed += playerController.MouseCancelThrow;
            #endregion
        }

        if (playerFreeCam != null)
        {
            actions.Gamepad.FreeCam.performed += playerFreeCam.FreeCam;
            actions.Gamepad.FreeCam.canceled += playerFreeCam.FreeCam;
            actions.MouseKeyboard.FreeCam.performed += playerFreeCam.FreeCam;
            actions.MouseKeyboard.StartFreeCam.performed += playerFreeCam.StartFreeCam;
            actions.MouseKeyboard.StartFreeCam.canceled += playerFreeCam.StartFreeCam;
        }

        if (reloadScene != null)
        {
            actions.Cheat.ReloadScene.performed += reloadScene.Reload;
        }

        if (noClip != null)
        {
            actions.Cheat.NoClip.performed += NoClipMode;
            actions.Cheat.NoClipControl.performed += noClip.MovePlayer;
            actions.Cheat.NoClipControl.canceled += noClip.MovePlayer;
        }

        actions.Gamepad.Enable();
        if (Gamepad.current == null)
            actions.MouseKeyboard.Enable();
        actions.Cheat.Enable();
    }

    private void NoClipMode(InputAction.CallbackContext context)
    {
        if (!noClip.ModeOn)
        {
            noClip.ModeOn = true;
            actions.Gamepad.Disable();
            actions.MouseKeyboard.Disable();
            noClip.PlayerCollider.enabled = false;
        }
        else
        {
            noClip.ModeOn = false;
            actions.Gamepad.Enable();
            actions.MouseKeyboard.Enable();
            noClip.PlayerCollider.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            #region Gamepad
            actions.Gamepad.ThrowPlayer.performed -= playerController.Throw;
            actions.Gamepad.GamepadStrenght.performed -= playerController.GamepadStrenght;
            actions.Gamepad.GamepadStrenght.canceled -= playerController.GamepadStrenght;
            #endregion
            #region Mouse/Keyboard
            actions.MouseKeyboard.MouseStrenght.performed -= playerController.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed -= playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled -= playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseCancelThrow.performed -= playerController.MouseCancelThrow;
            #endregion
        }

        if (playerFreeCam != null)
        {
            actions.Gamepad.FreeCam.performed -= playerFreeCam.FreeCam;
            actions.Gamepad.FreeCam.canceled -= playerFreeCam.FreeCam;
            actions.MouseKeyboard.FreeCam.performed -= playerFreeCam.FreeCam;
            actions.MouseKeyboard.StartFreeCam.performed -= playerFreeCam.StartFreeCam;
            actions.MouseKeyboard.StartFreeCam.canceled -= playerFreeCam.StartFreeCam;
        }

        if (reloadScene != null)
        {
            actions.Cheat.ReloadScene.performed -= reloadScene.Reload;
        }

        if (noClip != null)
        {
            actions.Cheat.NoClip.performed -= NoClipMode;
            actions.Cheat.NoClipControl.performed -= noClip.MovePlayer;
            actions.Cheat.NoClipControl.canceled -= noClip.MovePlayer;
        }
    }
}
