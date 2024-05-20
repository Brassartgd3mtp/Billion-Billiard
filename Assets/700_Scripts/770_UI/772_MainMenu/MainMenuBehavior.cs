using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
    public Button playButton;

    public GameObject mainPanel;
    public GameObject settingsPanel;
    public EventSystem eventSystem;
    public GameObject sliderVolume;
    public GameObject StartButton;


    [Header("PARAMETER ANIMATION PLAY TRANSITION")]
    public Animator MyAnimator;
    public string NameAnimation;
    public float WaitTime = 2f;
    public ParticleSystem ParticleSystemConfetti;

    public void Awake()
    {
        Screen.fullScreen = true;
        MyAnimator.GetComponent<Animation>();
    }

    public void PlayButton()
    {
        StartCoroutine(TransitionPlayButton());
    }

    IEnumerator TransitionPlayButton()
    {
        ParticleSystemConfetti.Play();
        MyAnimator.Play(NameAnimation);

        yield return new WaitForSeconds(WaitTime);

        if (!CutscenesCurrent.isCutsceneFirstTime[0])
        {
            LevelSelectorData.CurrentLevelIndex = 2;
            CutscenesCurrent.PackIndex = 0;
            SceneManager.LoadScene(12);
        }
        else
            SceneManager.LoadScene(1);


        yield return null;

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(sliderVolume.gameObject);
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
        eventSystem.SetSelectedGameObject(StartButton.gameObject);
    }
}
