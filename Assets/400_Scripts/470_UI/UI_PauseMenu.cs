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
    [SerializeField] private InputManager playerInput;
    [SerializeField] private InputManager freeCamInput;

    void Start()
    {
        // Assurez-vous que les panneaux d'options sont désactivés au début
        if (optionPanel != null)
            optionPanel.SetActive(false);
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

        playerInput.Actions.Gamepad.GamepadStrenght.Disable();
        playerInput.Actions.Gamepad.ThrowPlayer.Disable();
        playerInput.Actions.MouseKeyboard.MouseStartDrag.Disable();

        freeCamInput.Actions.Gamepad.FreeCam.Disable();
        freeCamInput.Actions.Gamepad.StartFreeCam.Disable();
        freeCamInput.Actions.MouseKeyboard.FreeCam.Disable();
        freeCamInput.Actions.MouseKeyboard.StartFreeCam.Disable();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseFirstbutton);
    }

    void PauseOff()
    {
        panel.SetActive(false);

        playerInput.Actions.Gamepad.GamepadStrenght.Enable();
        playerInput.Actions.Gamepad.ThrowPlayer.Enable();
        playerInput.Actions.MouseKeyboard.MouseStartDrag.Enable();

        freeCamInput.Actions.Gamepad.FreeCam.Enable();
        freeCamInput.Actions.Gamepad.StartFreeCam.Enable();
        freeCamInput.Actions.MouseKeyboard.FreeCam.Enable();
        freeCamInput.Actions.MouseKeyboard.StartFreeCam.Enable();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        // R�tablit le temps � sa valeur normale pour reprendre le jeu
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
