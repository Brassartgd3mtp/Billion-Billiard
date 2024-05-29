using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GestionBouton : MonoBehaviour
{
    public GameObject panelSettings;
    public GameObject PanelLVL;
    public GameObject mainMenuButton; // Référence à l'objet que vous souhaitez sélectionner dans le menu principal

    private EventSystem eventSystem;

    void Start()
    {
        // Assurez-vous que les panels sont désactivés au démarrage du jeu
        panelSettings.SetActive(false);
        PanelLVL.SetActive(false);

        // Récupère le EventSystem
        eventSystem = EventSystem.current;
    }

    public void PanelSettingsOuvert()
    {
        // Appelé lorsque le bouton pour le Panel 1 est appuyé
        OuvrirPanel(panelSettings);
    }

    public void PanelLVLOuvert()
    {
        FermerPanel(panelSettings);
        // Appelé lorsque le bouton pour le Panel 2 est appuyé
        OuvrirPanel(PanelLVL);
    }

    public void PanelSettingsFermer()
    {
        // Appelé lorsque le bouton pour fermer le Panel 1 est appuyé
        FermerPanel(panelSettings);
        // Sélectionne l'objet du menu principal lors de la fermeture du panel
        eventSystem.SetSelectedGameObject(mainMenuButton);
    }

    public void PanelLVLFermer()
    {
        // Appelé lorsque le bouton pour fermer le Panel 2 est appuyé
        FermerPanel(PanelLVL);
        // Sélectionne l'objet du menu principal lors de la fermeture du panel
        eventSystem.SetSelectedGameObject(mainMenuButton);
    }

    public void Quitter()
    {
        Application.Quit();
    }

    void OuvrirPanel(GameObject panel)
    {
        // Active le panel spécifié
        panel.SetActive(true);

        // Sélectionne le premier élément du panel pour la navigation avec la manette
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(panel.GetComponentInChildren<Selectable>().gameObject);
        }
    }

    void FermerPanel(GameObject panel)
    {
        // Désactive le panel spécifié
        panel.SetActive(false);
    }
}
