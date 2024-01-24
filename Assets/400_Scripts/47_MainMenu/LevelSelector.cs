using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionNiveaux : MonoBehaviour
{
    public SceneAsset niveau1;
    public SceneAsset niveau2;
    public SceneAsset niveauSandbox;

    public void ChargerNiveau1()
    {
        ChargerNiveau(niveau1);
    }

    public void ChargerNiveau2()
    {
        ChargerNiveau(niveau2);
    }

    public void ChargerNiveauSandbox()
    {
        ChargerNiveau(niveauSandbox);
    }

    private void ChargerNiveau(SceneAsset niveau)
    {
        // Vérifie si le niveau n'est pas null
        if (niveau != null)
        {
            // Charge la scène correspondante
            SceneManager.LoadScene(niveau.name);
        }
        else
        {
            Debug.LogWarning("La scène n'a pas été définie.");
        }
    }
}
