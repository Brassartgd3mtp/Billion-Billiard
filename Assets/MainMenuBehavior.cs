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
        SceneManager.LoadScene(1);
    }

    public void SettingsButton()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void QuitPanelSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
