using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelUpdateUI : MonoBehaviour
{
    [Header("References")]

    private LevelSelectorManager levelSelectorScript;

    [Header("Texts")]

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Star Images")]

    [SerializeField] private Image img_star1;
    [SerializeField] private Image img_star2;
    [SerializeField] private Image img_star3;

    [SerializeField] private Sprite spr_star;

    [Header("Medal Images")]

    [SerializeField] private Image IMG_pbMedal;
    [SerializeField] private List<Sprite> medalImages = new List<Sprite>();

    private void Awake()
    {
        levelSelectorScript = FindAnyObjectByType<LevelSelectorManager>();

        for (int i = 0; i < levelSelectorScript.Panels.Count; i++)
        {
            if (levelSelectorScript.Panels[i] == gameObject)
            {
                int buildIndex = i + 2;

                scoreText.text = GlobalData.Highscore[buildIndex].ToString();
                timerText.text = GlobalData.PB[buildIndex];

                switch (GlobalData.Stars[buildIndex])
                {
                    case 1:
                        img_star1.sprite = spr_star;
                        break;
                    case 2:
                        img_star1.sprite = spr_star;
                        img_star2.sprite = spr_star;
                        break;
                    case 3:
                        img_star1.sprite = spr_star;
                        img_star2.sprite = spr_star;
                        img_star3.sprite = spr_star;
                        break;
                }

                IMG_pbMedal.sprite = medalImages[GlobalData.MedalValues[buildIndex]];

                return;
            }                    
        }
    }
}
