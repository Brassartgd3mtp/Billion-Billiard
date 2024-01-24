using UnityEngine;
using UnityEngine.UI;

public class GestionBouton : MonoBehaviour
{
    public GameObject panelSettings;
    public GameObject PanelLVL;

    void Start()
    {
        // Assurez-vous que les panels sont d�sactiv�s au d�marrage du jeu
        panelSettings.SetActive(false);
        PanelLVL.SetActive(false);
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
    }

    public void PanelLVLFermer()
    {
        // Appel� lorsque le bouton pour fermer le Panel 2 est appuy�
        FermerPanel(PanelLVL);
    }

    public void Quitter()
    {
        // Appel� lorsque le bouton pour quitter est appuy�

        // Quitte le jeu apr�s 2 secondes (ajustez selon vos besoins)
        Invoke("QuitterJeu", 2f);

        Debug.Log("Quitter"); 
    }

    void OuvrirPanel(GameObject panel)
    {
        // Active le panel sp�cifi�
        panel.SetActive(true);
    }

    void FermerPanel(GameObject panel)
    {
        // D�sactive le panel sp�cifi�
        panel.SetActive(false);
    }

    void QuitterJeu()
    {
        // Quitte le jeu (fonctionne uniquement dans un build, pas dans l'�diteur Unity)
        Application.Quit();
    }
}
