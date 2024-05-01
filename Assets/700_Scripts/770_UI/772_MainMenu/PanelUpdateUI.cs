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
        //This function uses the buildindex to display the scores in the right panel

        levelSelectorScript = FindAnyObjectByType<LevelSelectorManager>();

        for (int i = 0; i < levelSelectorScript.Panels.Count; i++) //we check for all the panels
        {
            if(levelSelectorScript.Panels[i] == this.gameObject) // we check if we have found this panel
            {
                int buildIndex = i + 2; // We add +2 to the number from the list because of Main menu + level selector, that are preceding the levels in the build order

                PlayerPrefs.GetFloat("PBinSeconds" + buildIndex);
                
                scoreText.text = PlayerPrefs.GetFloat("hiscore" + buildIndex).ToString();
                timerText.text = PlayerPrefs.GetString("PB" + buildIndex);

                switch (PlayerPrefs.GetFloat("stars" + buildIndex)) // we display as much gold star as the best star rating stored in our datas
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

                IMG_pbMedal.sprite = medalImages[PlayerPrefs.GetInt("MedalValue" + buildIndex)]; //then we display the medal corresponding to the best score stored

                return;
            }                    
        }
    }
}
