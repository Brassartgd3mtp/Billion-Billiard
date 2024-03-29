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

        InputHandler.PauseMenuEnable(this);

        EventSystem.current.firstSelectedGameObject = PauseFirstbutton;
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

        InputHandler.Actions.Gamepad.GamepadStrenght.Disable();
        InputHandler.Actions.Gamepad.ThrowPlayer.Disable();
        InputHandler.Actions.MouseKeyboard.MouseStartDrag.Disable();

        InputHandler.Actions.Gamepad.FreeCam.Disable();
        InputHandler.Actions.Gamepad.StartFreeCam.Disable();
        InputHandler.Actions.MouseKeyboard.FreeCam.Disable();
        InputHandler.Actions.MouseKeyboard.StartFreeCam.Disable();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
        //Time.fixedDeltaTime = 0f;

        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(PauseFirstbutton);
    }

    void PauseOff()
    {
        panel.SetActive(false);

        InputHandler.Actions.Gamepad.GamepadStrenght.Enable();
        InputHandler.Actions.Gamepad.ThrowPlayer.Enable();
        InputHandler.Actions.MouseKeyboard.MouseStartDrag.Enable();

        InputHandler.Actions.Gamepad.FreeCam.Enable();
        InputHandler.Actions.Gamepad.StartFreeCam.Enable();
        InputHandler.Actions.MouseKeyboard.FreeCam.Enable();
        InputHandler.Actions.MouseKeyboard.StartFreeCam.Enable();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        // R�tablit le temps � sa valeur normale pour reprendre le jeu
        Time.timeScale = 1f;
        //Time.fixedDeltaTime = 1f;
    }

    private void OnDisable()
    {
        InputHandler.PauseMenuDisable();
    }
}
