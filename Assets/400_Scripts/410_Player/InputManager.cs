using UnityEngine;
using UnityEngine.EventSystems;
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

            actions.Gamepad.ThrowPlayer.performed += playerController.GamepadThrow;
            actions.Gamepad.GamepadStrenght.performed += playerController.GamepadStrenght;
            actions.Gamepad.GamepadStrenght.canceled += playerController.GamepadStrenght;
            actions.Gamepad.PauseMenu.performed += PauseMenu;
            #endregion
            #region Mouse/Keyboard
            TurnBasedSystem.OnEnablePlayerInput += actions.MouseKeyboard.Enable;
            TurnBasedSystem.OnDisablePlayerInput += actions.MouseKeyboard.Disable;

            actions.MouseKeyboard.MouseStrenght.performed += playerController.MouseStrenght;
            actions.MouseKeyboard.MouseStartDrag.performed += playerController.MouseStartDrag;
            actions.MouseKeyboard.MouseStartDrag.canceled += playerController.MouseStartDrag;
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
        if (Gamepad.current == null) actions.MouseKeyboard.Enable();
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
        if (playerController != null)
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
