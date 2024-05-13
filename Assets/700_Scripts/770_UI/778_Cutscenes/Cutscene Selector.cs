using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CutsceneSelector : MonoBehaviour
{
    public Sprite LockImage;
    public Image[] CutscenesLocker = new Image[8];

    private void OnEnable()
    {
        Image[] baseImages = new Image[8];
        Array.Copy(CutscenesLocker, baseImages, 8);

        for (int i = 0; i < CutscenesLocker.Length; i++)
        {
            if (!CutscenesCurrent.isCutsceneFirstTime[i])
            {
                CutscenesLocker[i].raycastTarget = false;
                CutscenesLocker[i].sprite = LockImage;
            }
            else
            {
                CutscenesLocker[i].raycastTarget = true;
                CutscenesLocker[i].sprite = baseImages[i].sprite;
            }
        }
    }

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
