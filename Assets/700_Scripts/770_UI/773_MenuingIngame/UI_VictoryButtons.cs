using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_VictoryButtons : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] int index;

    private void OnEnable()
    {
        if (CutscenesCurrent.isCutsceneFirstTime[index])
        {
            buttons[0].SetActive(true);
            buttons[1].SetActive(true);
            buttons[2].SetActive(false);

            if (SwapControls.state == CurrentState.Gamepad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(buttons[0]);
            }
            
        }
        else
        {
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            buttons[2].SetActive(true);

            if (SwapControls.state == CurrentState.Gamepad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(buttons[2]);
            }
        }
    }

    public void LoadCutscene()
    {
        if (!CutscenesCurrent.isCutsceneFirstTime[index])
        {
            CutscenesCurrent.PackIndex = index;
            LevelSelectorData.CurrentLevelIndex = 1;
            SceneManager.LoadScene(12);
        }
    }
}
