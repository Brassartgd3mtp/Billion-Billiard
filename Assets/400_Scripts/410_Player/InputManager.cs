using UnityEngine;
using UnityEngine.EventSystems;
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
            actions.Gamepad.GamepadStrenght.performed += pc.GamepadStrenght;
            actions.Gamepad.GamepadStrenght.canceled += pc.GamepadStrenght;
            actions.Gamepad.PauseMenu.performed += PauseMenu;
            #endregion
            #region Mouse/Keyboard
            TurnBasedSystem.OnEnablePlayerInput += actions.MouseKeyboard.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.MouseKeyboard.Disable;

            actions.MouseKeyboard.MouseStrenght.performed += pc.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed += pc.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled += pc.MouseStartDrag;
            actions.MouseKeyboard.MouseCancelThrow.performed += pc.MouseCancelThrow;
            #endregion
        }

        if (pfc != null)
        {
            actions.Gamepad.FreeCam.performed += pfc.FreeCam;
            actions.Gamepad.FreeCam.canceled += pfc.FreeCam;
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

    public GameObject panel;
    private bool panelActive = false;
    public GameObject PauseFirstbutton;
    public void PauseMenu(InputAction.CallbackContext context)
    {
        // Inverse l'�tat d'activation du panneau
        panelActive = !panelActive;

        // Active ou d�sactive le panneau selon l'�tat
        panel.SetActive(panelActive);

        if (panelActive)
        {
            EventSystem.current.SetSelectedGameObject(null);
            //Selectionne le first button
            EventSystem.current.SetSelectedGameObject(PauseFirstbutton);
        }

        if (panelActive)
        {
            //actions.Gamepad.Disable();
            Time.timeScale = 0f; // Met le temps � z�ro pour mettre le jeu en pause
            Time.fixedDeltaTime = 0f;
        }
        else
        {
            //actions.Gamepad.Enable();
            Time.timeScale = 1f; // R�tablit le temps � sa valeur normale pour reprendre le jeu
            Time.fixedDeltaTime = 1f;
        }
    }

    private void OnDisable()
    {
        if (pc != null)
        {
            #region Gamepad
            actions.Gamepad.ThrowPlayer.performed -= pc.GamepadThrow;
            actions.Gamepad.GamepadStrenght.performed -= pc.GamepadStrenght;
            actions.Gamepad.GamepadStrenght.canceled -= pc.GamepadStrenght;
            actions.Gamepad.PauseMenu.performed -= PauseMenu;
            #endregion
            #region Mouse/Keyboard
            actions.MouseKeyboard.MouseStrenght.performed -= pc.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed -= pc.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled -= pc.MouseStartDrag;
            actions.MouseKeyboard.MouseCancelThrow.performed -= pc.MouseCancelThrow;
            #endregion
        }

        if (pfc != null)
        {
            actions.Gamepad.FreeCam.performed -= pfc.FreeCam;
            actions.Gamepad.FreeCam.canceled -= pfc.FreeCam;
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
