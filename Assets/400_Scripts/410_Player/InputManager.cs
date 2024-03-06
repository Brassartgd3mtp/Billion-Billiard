using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerActionMap actions;

    private NoClip noClip;
    // Start is called before the first frame update
    void Awake()
    {
        actions = new PlayerActionMap();

        if (gameObject.TryGetComponent(out PlayerController playerController))
        {
            #region Gamepad
            TurnBasedSystem.OnEnablePlayerInput += actions.Gamepad.GamepadStrenght.Enable;
            TurnBasedSystem.OnEnablePlayerInput += actions.Gamepad.ThrowPlayer.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.Gamepad.GamepadStrenght.Disable;
            TurnBasedSystem.OnDisablePlayerInput += actions.Gamepad.ThrowPlayer.Disable;

            actions.Gamepad.ThrowPlayer.performed += playerController.GamepadThrow;
            actions.Gamepad.GamepadStrenght.performed += playerController.GamepadStrenght;
            actions.Gamepad.GamepadStrenght.canceled += playerController.GamepadStrenght;
            actions.Gamepad.PauseMenu.performed += PauseMenu;
            #endregion
            #region Mouse/Keyboard
            TurnBasedSystem.OnEnablePlayerInput += actions.MouseKeyboard.MouseStartDrag.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.MouseKeyboard.MouseStartDrag.Disable;

            actions.MouseKeyboard.MouseStrenght.performed += playerController.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed += playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled += playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseCancelThrow.performed += playerController.MouseCancelThrow;
            #endregion
        }

        if (gameObject.TryGetComponent(out PlayerFreeCam playerFreeCam))
        {
            actions.Gamepad.FreeCam.performed += playerFreeCam.FreeCam;
            actions.Gamepad.FreeCam.canceled += playerFreeCam.FreeCam;
            actions.MouseKeyboard.FreeCam.performed += playerFreeCam.FreeCam;
            actions.MouseKeyboard.StartFreeCam.performed += playerFreeCam.StartFreeCam;
            actions.MouseKeyboard.StartFreeCam.canceled += playerFreeCam.StartFreeCam;
        }

        if (gameObject.TryGetComponent(out ReloadScene reloadScene))
        {
            actions.Cheat.ReloadScene.performed += reloadScene.Reload;
        }

        if (gameObject.TryGetComponent(out NoClip noClip))
        {
            this.noClip = noClip;
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

    public GameObject panel;
    private bool panelActive = false;
    public GameObject PauseFirstbutton;
    public void PauseMenu(InputAction.CallbackContext context)
    {
        if (!panelActive)
        {
            panelActive = true;
        } else panelActive = false;


        if (panelActive)
        {
            panel.SetActive(true);
            Gamepad.current?.ResetHaptics();
            EventSystem.current.SetSelectedGameObject(null);
            //Selectionne le first button
            EventSystem.current.SetSelectedGameObject(PauseFirstbutton);
        }

        if (panelActive)
        {
            //actions.Gamepad.Disable();
            Time.timeScale = 0f; // Met le temps à zéro pour mettre le jeu en pause
            
            //Time.fixedDeltaTime = 0f;
        }
        else
        {
            //actions.Gamepad.Enable();
            panel.SetActive(false);
            Time.timeScale = 1f; // Rétablit le temps é sa valeur normale pour reprendre le jeu
        }
    }

    private void OnDisable()
    {
        if (gameObject.TryGetComponent(out PlayerController playerController))
        {
            #region Gamepad
            actions.Gamepad.ThrowPlayer.performed -= playerController.GamepadThrow;
            actions.Gamepad.GamepadStrenght.performed -= playerController.GamepadStrenght;
            actions.Gamepad.GamepadStrenght.canceled -= playerController.GamepadStrenght;
            actions.Gamepad.PauseMenu.performed -= PauseMenu;
            #endregion
            #region Mouse/Keyboard
            actions.MouseKeyboard.MouseStrenght.performed -= playerController.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed -= playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled -= playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseCancelThrow.performed -= playerController.MouseCancelThrow;
            #endregion
        }

        if (gameObject.TryGetComponent(out PlayerFreeCam playerFreeCam))
        {
            actions.Gamepad.FreeCam.performed -= playerFreeCam.FreeCam;
            actions.Gamepad.FreeCam.canceled -= playerFreeCam.FreeCam;
            actions.MouseKeyboard.FreeCam.performed -= playerFreeCam.FreeCam;
            actions.MouseKeyboard.StartFreeCam.performed -= playerFreeCam.StartFreeCam;
            actions.MouseKeyboard.StartFreeCam.canceled -= playerFreeCam.StartFreeCam;
        }

        if (gameObject.TryGetComponent(out ReloadScene reloadScene))
        {
            actions.Cheat.ReloadScene.performed -= reloadScene.Reload;
        }

        if (gameObject.TryGetComponent(out NoClip noClip))
        {
            actions.Cheat.NoClip.performed -= NoClipMode;
            actions.Cheat.NoClipControl.performed -= noClip.MovePlayer;
            actions.Cheat.NoClipControl.canceled -= noClip.MovePlayer;
        }
    }
}
