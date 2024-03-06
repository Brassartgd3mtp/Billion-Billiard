using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonActions : MonoBehaviour
{
    public GameObject panel;
    public GameObject optionPanel;
    public GameObject OptionFirstbutton;
    public GameObject PauseFirstbutton;

    public InputManager InputManager;

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
     //       InputManager.PauseOff();
            panel.SetActive(false);
     //       InputManager.panelActive = false;
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
}
