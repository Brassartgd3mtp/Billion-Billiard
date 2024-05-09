using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
    public Button playButton;

    public GameObject mainPanel;
    public GameObject settingsPanel;

    public void PlayButton()
    {
        if (!CutscenesCurrent.isCutsceneFirstTime[0])
        {
            LevelSelectorData.CurrentLevelIndex = 2;
            CutscenesCurrent.PackIndex = 0;
            SceneManager.LoadScene(12);
        }
        else
            SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(11);
    }

    public void CutscenesButton()
    {
        SceneManager.LoadScene(13);
    }

    public void QuitPanelSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
