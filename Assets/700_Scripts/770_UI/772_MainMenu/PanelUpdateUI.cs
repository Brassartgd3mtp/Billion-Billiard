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

    [Header("Images")]

    [SerializeField] private Image img_star1;
    [SerializeField] private Image img_star2;
    [SerializeField] private Image img_star3;

    [SerializeField] private Sprite spr_star;

    [Header("Images")]

    [SerializeField] private Image IMG_pbMedal;
    [SerializeField] private List<Sprite> medalImages = new List<Sprite>();

    private void Awake()
    {
        levelSelectorScript = FindAnyObjectByType<LevelSelectorManager>();
        for (int i = 0; i < levelSelectorScript.Panels.Count; i++)
        {
            if(levelSelectorScript.Panels[i] == this.gameObject)
            {
                int buildIndex = i + 2;

                PlayerPrefs.GetFloat("PBinSeconds" + buildIndex);
                
                scoreText.text = PlayerPrefs.GetFloat("hiscore" + buildIndex).ToString();
                timerText.text = PlayerPrefs.GetString("PB" + buildIndex);

                switch (PlayerPrefs.GetFloat("stars" + buildIndex))
                    {      
                    case 0:
                        break;
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

                IMG_pbMedal.sprite = medalImages[PlayerPrefs.GetInt("MedalValue" + buildIndex)];

                return;
            }                    
        }
    }
}
