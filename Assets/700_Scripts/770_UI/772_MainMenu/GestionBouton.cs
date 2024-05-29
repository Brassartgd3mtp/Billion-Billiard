using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GestionBouton : MonoBehaviour
{
    public GameObject panelSettings;
    public GameObject PanelLVL;
    public GameObject mainMenuButton; // R�f�rence � l'objet que vous souhaitez s�lectionner dans le menu principal

    private EventSystem eventSystem;

    void Start()
    {
        // Assurez-vous que les panels sont d�sactiv�s au d�marrage du jeu
        panelSettings.SetActive(false);
        PanelLVL.SetActive(false);

        // R�cup�re le EventSystem
        eventSystem = EventSystem.current;
    }

    public void PanelSettingsOuvert()
    {
        // Appel� lorsque le bouton pour le Panel 1 est appuy�
        OuvrirPanel(panelSettings);
    }

    public void PanelLVLOuvert()
    {
        FermerPanel(panelSettings);
        // Appel� lorsque le bouton pour le Panel 2 est appuy�
        OuvrirPanel(PanelLVL);
    }

    public void PanelSettingsFermer()
    {
        // Appel� lorsque le bouton pour fermer le Panel 1 est appuy�
        FermerPanel(panelSettings);
        // S�lectionne l'objet du menu principal lors de la fermeture du panel
        eventSystem.SetSelectedGameObject(mainMenuButton);
    }

    public void PanelLVLFermer()
    {
        // Appel� lorsque le bouton pour fermer le Panel 2 est appuy�
        FermerPanel(PanelLVL);
        // S�lectionne l'objet du menu principal lors de la fermeture du panel
        eventSystem.SetSelectedGameObject(mainMenuButton);
    }

    public void Quitter()
    {
        Application.Quit();
    }

    void OuvrirPanel(GameObject panel)
    {
        // Active le panel sp�cifi�
        panel.SetActive(true);

        // S�lectionne le premier �l�ment du panel pour la navigation avec la manette
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(panel.GetComponentInChildren<Selectable>().gameObject);
        }
    }

    void FermerPanel(GameObject panel)
    {
        // D�sactive le panel sp�cifi�
        panel.SetActive(false);
    }
}
