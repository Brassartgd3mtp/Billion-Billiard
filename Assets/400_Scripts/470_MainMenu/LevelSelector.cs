using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionNiveaux : MonoBehaviour
{
    public string niveau1;
    public string niveau2;
    public string niveauSandbox;

    public void ChargerNiveau1()
    {
         SceneManager.LoadScene(niveau1);
    }

    public void ChargerNiveau2()
    {
        SceneManager.LoadScene(niveau2);
    }

    public void ChargerNiveauSandbox()
    {
        SceneManager.LoadScene(niveauSandbox);
    }

   
    }

