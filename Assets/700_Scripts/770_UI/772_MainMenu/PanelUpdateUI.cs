using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelUpdateUI : MonoBehaviour
{
    [Header("References")]

    private LevelSelectorManager levelSelectorScript;

    [Header("Parameters")]

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    private float amountOfStars;
    [SerializeField] bool hasSuceededinEnoughShots;

    private void Awake()
    {
        levelSelectorScript = FindAnyObjectByType<LevelSelectorManager>();
        for (int i = 0; i < levelSelectorScript.Panels.Count; i++)
        {
            if(levelSelectorScript.Panels[i] == this.gameObject)
            {
                int buildIndex = i + 2;

                PlayerPrefs.GetFloat("PBinSeconds" + buildIndex);
                PlayerPrefs.GetFloat("stars" + buildIndex);
                scoreText.text = PlayerPrefs.GetFloat("hiscore" + buildIndex).ToString();
                timerText.text = PlayerPrefs.GetString("PB" + buildIndex);

                return;
            }
        }
    }


}
