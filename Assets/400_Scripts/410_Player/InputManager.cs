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
            TurnBasedSystem.OnEnablePlayerInput += actions.Gamepad.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.Gamepad.Disable;

            actions.Gamepad.ThrowPlayer.performed += pc.ThrowPlayer;
            actions.Gamepad.ArrowDirection.performed += pc.SetArrowDirection;
            actions.Gamepad.ArrowDirection.canceled += pc.SetArrowDirection;
            actions.Gamepad.StrenghtModifier.performed += pc.ModifyStrenght;
            actions.Gamepad.StrenghtModifier.canceled += pc.ModifyStrenght;
        }
        
        if (pfc != null)
        {
            actions.Gamepad.FreeCam.performed += pfc.FreeCam;
            actions.Gamepad.FreeCam.canceled += pfc.FreeCam;
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
        actions.Cheat.Enable();
    }

    private void NoClipMode(InputAction.CallbackContext context)
    {
        if (!nc.ModeOn)
        {
            nc.ModeOn = true;
            actions.Gamepad.Disable();
            nc.PlayerCollider.enabled = false;
        }
        else
        {
            nc.ModeOn = false;
            actions.Gamepad.Enable();
            nc.PlayerCollider.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (pc != null)
        {
            actions.Gamepad.ThrowPlayer.performed -= pc.ThrowPlayer;
            actions.Gamepad.ArrowDirection.performed -= pc.SetArrowDirection;
            actions.Gamepad.ArrowDirection.canceled -= pc.SetArrowDirection;
            actions.Gamepad.StrenghtModifier.performed -= pc.ModifyStrenght;
            actions.Gamepad.StrenghtModifier.canceled -= pc.ModifyStrenght;
        }

        if (pfc != null)
        {
            actions.Gamepad.FreeCam.performed -= pfc.FreeCam;
            actions.Gamepad.FreeCam.canceled -= pfc.FreeCam;
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
