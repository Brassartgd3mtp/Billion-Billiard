using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelTimer : MonoBehaviour
{

    [Header("Time logic")]

    [SerializeField] private float timerInSeconds;
    private bool timeStarted = true;
    private float minutes;
    private float seconds;

    [Header("Medals Timers")]

    [SerializeField] private float goldMedalThresholdInSeconds;
    [SerializeField] private float silverMedalThresholdInSeconds;
    [SerializeField] private float bronzeMedalThresholdInSeconds;

    [Header("Score per Medals")]

    public float GoldScore;
    [SerializeField] private float silverScore;
    [SerializeField] private float bronzeScore;

    [HideInInspector] public float FinalTimerScore;
    

    [Header("UI")]

    [SerializeField] private TextMeshProUGUI TXT_Timer;

    void Update()
    {
        if (timeStarted == true)
        {
            timerInSeconds += Time.deltaTime;
            CalculateTime();
        }
        if(timerInSeconds <= bronzeMedalThresholdInSeconds + 1)
        {
            CheckForMedal();
        }

        if (timerInSeconds < 0)
        {
            timerInSeconds = 0;
        }
    }

    private void CalculateTime()
    {
        minutes = Mathf.Floor(timerInSeconds / 60);
        seconds = timerInSeconds % 60;

        TXT_Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void CheckForMedal()
    {
        if(timerInSeconds < goldMedalThresholdInSeconds)
        {
            FinalTimerScore = GoldScore;
        }
        else if(timerInSeconds < silverMedalThresholdInSeconds) 
        { 
            FinalTimerScore = silverScore; 
        }
        else if(timerInSeconds < bronzeMedalThresholdInSeconds)
        {
            FinalTimerScore = bronzeScore;
        }
        else FinalTimerScore = 0;
    }

}