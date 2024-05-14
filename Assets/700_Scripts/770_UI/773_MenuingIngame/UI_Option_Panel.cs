using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Option_Panel : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject PauseFirstbutton;
    void Start()
    {
        // Assurez-vous que le panneau d'options est désactivé au début
        if (optionPanel != null)
            optionPanel.SetActive(false);
    }

    public void OnCloseButtonClick()
    {
        // Ferme le panneau d'options
        if (optionPanel != null)
            optionPanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        //Selectionne le first button
        EventSystem.current.SetSelectedGameObject(PauseFirstbutton);

    }
}
