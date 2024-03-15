using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject optionPanel;
    public GameObject OptionFirstbutton;
    public GameObject PauseFirstbutton;

    private bool panelActive = false;

    void Start()
    {
        // Assurez-vous que les panneaux d'options sont désactivés au début
        if (optionPanel != null)
            optionPanel.SetActive(false);

        InputManager.PauseMenuEnable(this);
    }

    public void OnPlayButtonClick()
    {
        // Ferme le panneau
        if (panel != null)
        {
            panelActive = false;
            PauseOff();
        }
    }

    public void OnOptionButtonClick()
    {
        // Ouvre le panneau d'options
        if (optionPanel != null)
        {
            optionPanel.SetActive(true);
            Debug.Log(optionPanel.activeSelf);
        }

        if (optionPanel.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            //Selectionne le first button
            EventSystem.current.SetSelectedGameObject(OptionFirstbutton);
        }
    }

    public void OnMainMenuButtonClick()
    {
        // Recharge la scène "Main Menu"
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void OnRestartButtonClick()
    {
        if (panel != null)
            panel.SetActive(false);
        // Recharge la scène actuelle
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //Rétablit le temps du jeu
        Time.timeScale = 1f; 
       
    }

    public void OnQuitButtonClick()
    {
        // Ferme le jeu
        Application.Quit();
    }

    public void PauseMenuState(InputAction.CallbackContext context)
    {
        panelActive = !panelActive;

        if (panelActive)
            PauseOn();
        else
            PauseOff();
    }

    void PauseOn()
    {
        panel.SetActive(true);

        InputManager.Actions.Gamepad.GamepadStrenght.Disable();
        InputManager.Actions.Gamepad.ThrowPlayer.Disable();
        InputManager.Actions.MouseKeyboard.MouseStartDrag.Disable();

        InputManager.Actions.Gamepad.FreeCam.Disable();
        InputManager.Actions.Gamepad.StartFreeCam.Disable();
        InputManager.Actions.MouseKeyboard.FreeCam.Disable();
        InputManager.Actions.MouseKeyboard.StartFreeCam.Disable();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
        //Time.fixedDeltaTime = 0f;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseFirstbutton);
    }

    void PauseOff()
    {
        panel.SetActive(false);

        InputManager.Actions.Gamepad.GamepadStrenght.Enable();
        InputManager.Actions.Gamepad.ThrowPlayer.Enable();
        InputManager.Actions.MouseKeyboard.MouseStartDrag.Enable();

        InputManager.Actions.Gamepad.FreeCam.Enable();
        InputManager.Actions.Gamepad.StartFreeCam.Enable();
        InputManager.Actions.MouseKeyboard.FreeCam.Enable();
        InputManager.Actions.MouseKeyboard.StartFreeCam.Enable();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        // R�tablit le temps � sa valeur normale pour reprendre le jeu
        Time.timeScale = 1f;
        //Time.fixedDeltaTime = 1f;
    }

    private void OnDisable()
    {
        InputManager.PauseMenuDisable(this);
    }
}
