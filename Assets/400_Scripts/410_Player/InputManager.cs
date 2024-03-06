using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerActionMap actions;

    private PlayerController playerController;
    private PlayerFreeCam playerFreeCam;
    private ReloadScene reloadScene;
    private NoClip noClip;
    // Start is called before the first frame update
    void Awake()
    {
        actions = new PlayerActionMap();

        if (gameObject.TryGetComponent(out PlayerController playerController))
        {
            this.playerController = playerController;
            #region Gamepad
            TurnBasedSystem.OnEnablePlayerInput += actions.Gamepad.GamepadStrenght.Enable;
            TurnBasedSystem.OnEnablePlayerInput += actions.Gamepad.ThrowPlayer.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.Gamepad.GamepadStrenght.Disable;
            TurnBasedSystem.OnDisablePlayerInput += actions.Gamepad.ThrowPlayer.Disable;

            actions.Gamepad.ThrowPlayer.performed += playerController.Throw;
            actions.Gamepad.GamepadStrenght.performed += playerController.GamepadStrenght;
            actions.Gamepad.GamepadStrenght.canceled += playerController.GamepadStrenght;
            actions.Gamepad.PauseMenu.performed += PauseMenu;
            #endregion
            #region Mouse/Keyboard
            TurnBasedSystem.OnEnablePlayerInput += actions.MouseKeyboard.MouseStartDrag.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.MouseKeyboard.MouseStartDrag.Disable;

            actions.MouseKeyboard.MouseStrenght.performed += playerController.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.started += playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.performed += playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled += playerController.MouseThrow;
            actions.MouseKeyboard.MouseCancelThrow.performed += playerController.MouseCancelThrow;
            #endregion
        }

        if (gameObject.TryGetComponent(out PlayerFreeCam playerFreeCam))
        {
            this.playerFreeCam = playerFreeCam;
            actions.Gamepad.FreeCam.performed += playerFreeCam.FreeCam;
            actions.Gamepad.FreeCam.canceled += playerFreeCam.FreeCam;
            actions.Gamepad.StartFreeCam.started += playerFreeCam.StartFreeCam;
            actions.Gamepad.StartFreeCam.canceled += playerFreeCam.StartFreeCam;
            actions.MouseKeyboard.FreeCam.performed += playerFreeCam.FreeCam;
            actions.MouseKeyboard.StartFreeCam.started += playerFreeCam.StartFreeCam;
            actions.MouseKeyboard.StartFreeCam.canceled += playerFreeCam.StartFreeCam;
        }

        if (gameObject.TryGetComponent(out ReloadScene reloadScene))
        {
            this.reloadScene = reloadScene;
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
    public bool panelActive = false;
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
            Time.fixedDeltaTime = .02f;
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
            actions.Gamepad.PauseMenu.performed -= PauseMenu;
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
            actions.Gamepad.StartFreeCam.started -= playerFreeCam.StartFreeCam;
            actions.Gamepad.StartFreeCam.canceled -= playerFreeCam.StartFreeCam;
            actions.MouseKeyboard.FreeCam.performed -= playerFreeCam.FreeCam;
            actions.MouseKeyboard.StartFreeCam.started -= playerFreeCam.StartFreeCam;
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
