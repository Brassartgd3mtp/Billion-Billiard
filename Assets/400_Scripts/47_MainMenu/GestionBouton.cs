using UnityEngine;
using UnityEngine.UI;

public class GestionBouton : MonoBehaviour
{
    public GameObject panelSettings;
    public GameObject PanelLVL;

    void Start()
    {
        // Assurez-vous que les panels sont désactivés au démarrage du jeu
        panelSettings.SetActive(false);
        PanelLVL.SetActive(false);
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
    }

    public void PanelLVLFermer()
    {
        // Appelé lorsque le bouton pour fermer le Panel 2 est appuyé
        FermerPanel(PanelLVL);
    }

    public void Quitter()
    {
        // Appelé lorsque le bouton pour quitter est appuyé

        // Quitte le jeu après 2 secondes (ajustez selon vos besoins)
        Invoke("QuitterJeu", 2f);

        Debug.Log("Quitter"); 
    }

    void OuvrirPanel(GameObject panel)
    {
        // Active le panel spécifié
        panel.SetActive(true);
    }

    void FermerPanel(GameObject panel)
    {
        // Désactive le panel spécifié
        panel.SetActive(false);
    }

    void QuitterJeu()
    {
        // Quitte le jeu (fonctionne uniquement dans un build, pas dans l'éditeur Unity)
        Application.Quit();
    }
}
