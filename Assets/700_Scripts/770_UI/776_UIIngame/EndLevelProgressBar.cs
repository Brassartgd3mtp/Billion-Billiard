using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelProgressBar : MonoBehaviour
{
    [Header("References")]

    private LevelScore levelScoreScript;
    private PlayerStats playerStatsScript;


    private Slider slider;
    private float targetProgress;
    private float fillspeed = 0.5f;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        levelScoreScript = FindAnyObjectByType<LevelScore>();
        playerStatsScript = FindAnyObjectByType<PlayerStats>();
    }

    private void Start()
    {
        IncrementProgress(playerStatsScript.moneyCount/levelScoreScript.TotalScore);
    }
    private void Update()
    {
        if(slider.value < targetProgress)
        {
            slider.value += fillspeed * Time.deltaTime;
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }


}
