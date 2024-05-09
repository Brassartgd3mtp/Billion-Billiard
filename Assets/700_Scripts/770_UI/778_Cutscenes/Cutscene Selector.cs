using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneSelector : MonoBehaviour
{
    public void LoadCutscene(int packIndex)
    {
        if (CutscenesCurrent.isCutsceneFirstTime[packIndex])
        {
            CutscenesCurrent.PackIndex = packIndex;
            SceneManager.LoadScene(12);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
