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
            panel.SetActive(false);
    }

    public void OnOptionButtonClick()
    {
        // Ouvre le panneau d'options
        if (optionPanel != null)

            optionPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        //Selectionne le first button
        EventSystem.current.SetSelectedGameObject(OptionFirstbutton);
    }

    public void OnMainMenuButtonClick()
    {
        // Recharge la scène "Main Menu"
        SceneManager.LoadScene("Main_Menu");
    }

    public void OnRestartButtonClick()
    {
        if (panel != null)
            panel.SetActive(false);
        // Recharge la scène actuelle
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
       
    }

    public void OnQuitButtonClick()
    {
        // Ferme le jeu
        Application.Quit();
    }
}
