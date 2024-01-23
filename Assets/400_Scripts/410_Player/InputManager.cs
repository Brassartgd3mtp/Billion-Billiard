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

            actions.Gamepad.ThrowPlayer.performed += pc.ThrowPlayer;
            actions.Gamepad.ArrowDirection.performed += pc.SetArrowDirection;
            actions.Gamepad.ArrowDirection.canceled += pc.SetArrowDirection;
            actions.Gamepad.StrenghtModifier.performed += pc.ModifyStrenght;
            actions.Gamepad.StrenghtModifier.canceled += pc.ModifyStrenght;
            #endregion
            #region Mouse/Keyboard
            TurnBasedSystem.OnEnablePlayerInput += actions.MouseKeyboard.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.MouseKeyboard.Disable;

            actions.MouseKeyboard.MouseStrenght.performed += pc.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed += pc.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled += pc.MouseStartDrag;
            #endregion
        }

        if (pfc != null)
        {
            actions.Global.FreeCam.performed += pfc.FreeCam;
            actions.Gamepad.CancelFreeCam.canceled += pfc.CancelFreeCam;
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
        actions.MouseKeyboard.Enable();
        actions.Cheat.Enable();
        actions.Global.Enable();
    }

    private void NoClipMode(InputAction.CallbackContext context)
    {
        if (!nc.ModeOn)
        {
            nc.ModeOn = true;
            actions.Global.Disable();
            actions.Gamepad.Disable();
            actions.MouseKeyboard.Disable();
            nc.PlayerCollider.enabled = false;
        }
        else
        {
            nc.ModeOn = false;
            actions.Global.Enable();
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
            actions.Gamepad.ThrowPlayer.performed -= pc.ThrowPlayer;
            actions.Gamepad.ArrowDirection.performed -= pc.SetArrowDirection;
            actions.Gamepad.ArrowDirection.canceled -= pc.SetArrowDirection;
            actions.Gamepad.StrenghtModifier.performed -= pc.ModifyStrenght;
            actions.Gamepad.StrenghtModifier.canceled -= pc.ModifyStrenght;
            #endregion
            #region Mouse/Keyboard
            actions.MouseKeyboard.MouseStrenght.performed -= pc.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed -= pc.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled -= pc.MouseStartDrag;
            #endregion
        }

        if (pfc != null)
        {
            actions.Global.FreeCam.performed -= pfc.FreeCam;
            actions.Gamepad.CancelFreeCam.canceled -= pfc.CancelFreeCam;
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
