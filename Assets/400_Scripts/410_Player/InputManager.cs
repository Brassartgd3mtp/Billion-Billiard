using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerActionMap actions;

    [SerializeField] private PlayerController pc;
    [SerializeField] private PlayerFreeCam pfc;
    [SerializeField] private ReloadScene rs;
    [SerializeField] private NoClip nc;
    // Start is called before the first frame update
    void Awake()
    {
        actions = new PlayerActionMap();

        if (pc != null)
        {
            #region Gamepad
            TurnBasedSystem.OnEnablePlayerInput += actions.Gamepad.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.Gamepad.Disable;

            actions.Gamepad.ThrowPlayer.performed += pc.GamepadThrow;
            actions.Gamepad.GamepadStrenght.performed += pc.SetArrowDirection;
            actions.Gamepad.GamepadStrenght.canceled += pc.SetArrowDirection;
            #endregion
            #region Mouse/Keyboard
            TurnBasedSystem.OnEnablePlayerInput += actions.MouseKeyboard.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.MouseKeyboard.Disable;

            actions.MouseKeyboard.MouseStrenght.performed += pc.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed += pc.MouseStartDrag;
            actions.MouseKeyboard.ThrowPlayer.performed += pc.MouseThrow;
            #endregion
        }

        if (pfc != null)
        {
            actions.Gamepad.FreeCam.performed += pfc.FreeCam;
            actions.Gamepad.CancelFreeCam.canceled += pfc.CancelFreeCam;
            actions.MouseKeyboard.FreeCam.performed += pfc.FreeCam;
            actions.MouseKeyboard.StartFreeCam.performed += pfc.StartFreeCam;
            actions.MouseKeyboard.StartFreeCam.canceled += pfc.StartFreeCam;
        }

        if (rs != null)
        {
            actions.Cheat.ReloadScene.performed += rs.Reload;
        }

        if (nc != null)
        {
            actions.Cheat.NoClip.performed += NoClipMode;
            actions.Cheat.NoClipControl.performed += nc.MovePlayer;
            actions.Cheat.NoClipControl.canceled += nc.MovePlayer;
        }

        actions.Gamepad.Enable();
        if (Gamepad.current == null) actions.MouseKeyboard.Enable();
        actions.Cheat.Enable();
    }

    private void NoClipMode(InputAction.CallbackContext context)
    {
        if (!nc.ModeOn)
        {
            nc.ModeOn = true;
            actions.Gamepad.Disable();
            actions.MouseKeyboard.Disable();
            nc.PlayerCollider.enabled = false;
        }
        else
        {
            nc.ModeOn = false;
            actions.Gamepad.Enable();
            actions.MouseKeyboard.Enable();
            nc.PlayerCollider.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (pc != null)
        {
            #region Gamepad
            actions.Gamepad.ThrowPlayer.performed -= pc.GamepadThrow;
            actions.Gamepad.GamepadStrenght.performed -= pc.SetArrowDirection;
            actions.Gamepad.GamepadStrenght.canceled -= pc.SetArrowDirection;
            #endregion
            #region Mouse/Keyboard
            actions.MouseKeyboard.MouseStrenght.performed -= pc.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed -= pc.MouseStartDrag;
            actions.MouseKeyboard.ThrowPlayer.performed -= pc.MouseThrow;
            #endregion
        }

        if (pfc != null)
        {
            actions.Gamepad.FreeCam.performed -= pfc.FreeCam;
            actions.Gamepad.CancelFreeCam.canceled -= pfc.CancelFreeCam;
            actions.MouseKeyboard.FreeCam.performed -= pfc.FreeCam;
            actions.MouseKeyboard.StartFreeCam.performed -= pfc.StartFreeCam;
            actions.MouseKeyboard.StartFreeCam.canceled -= pfc.StartFreeCam;
        }

        if (rs != null)
        {
            actions.Cheat.ReloadScene.performed -= rs.Reload;
        }

        if (nc != null)
        {
            actions.Cheat.NoClip.performed -= NoClipMode;
            actions.Cheat.NoClipControl.performed -= nc.MovePlayer;
            actions.Cheat.NoClipControl.canceled -= nc.MovePlayer;
        }
    }
}
