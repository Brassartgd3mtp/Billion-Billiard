using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelTimer : MonoBehaviour
{

    [Header("Time logic")]

    public float TimerInSeconds;
    public bool TimeStarted = true;
    public float Minutes;
    public float Seconds;

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

    [Header("Value")]

    public int MedalValue;

    void Update()
    {
        if (TimeStarted)
        {
            TimerInSeconds += Time.deltaTime;
            CalculateTime();
        }
        if(TimerInSeconds <= bronzeMedalThresholdInSeconds + 1)
        {
            CheckForMedal();
        }

        if (TimerInSeconds < 0) //just in case the timer is negative for some reason
        {
            TimerInSeconds = 0;
        }
    }

    private void CalculateTime() //do the maths to display the timer is the right format
    {
        Minutes = Mathf.Floor(TimerInSeconds / 60);
        Seconds = TimerInSeconds % 60;

        TXT_Timer.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }

    private void CheckForMedal() // check which medal the player is currently running for
    {
        if (TimerInSeconds < goldMedalThresholdInSeconds)
        {
            FinalTimerScore = GoldScore;
            MedalValue = 3; //MedalValue is used to know which medal to display is other scripts
        }
        else if (TimerInSeconds < silverMedalThresholdInSeconds)
        {
            FinalTimerScore = silverScore;
            MedalValue = 2;
        }
        else if (TimerInSeconds < bronzeMedalThresholdInSeconds)
        {
            FinalTimerScore = bronzeScore;
            MedalValue = 1;
        }
        else
        {
            FinalTimerScore = 0;
            MedalValue = 0;
        }
    }

}