using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

    [SerializeField] private int bronzeMedalThresholdInSeconds;
    [SerializeField] private int silverMedalThresholdInSeconds;
    [SerializeField] private int goldMedalThresholdInSeconds;

    [Header("Score per Medals")]

    [SerializeField] private int bronzeScore;
    [SerializeField] private int silverScore;
    public int GoldScore;

    [Header("UI")]

    [SerializeField] private TextMeshProUGUI TXT_Timer;

    void Update()
    {
        if (TimeStarted)
        {
            TimerInSeconds += Time.deltaTime;
            CalculateTime();
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

    public int MedalValue()
    {
        Dictionary<int, int> MedalScore = new Dictionary<int, int>()
        {
            { goldMedalThresholdInSeconds, 3 },
            { silverMedalThresholdInSeconds, 2 },
            { bronzeMedalThresholdInSeconds, 1 }
        };

        foreach (int key in MedalScore.Keys)
        {
            if ((int)TimerInSeconds <= key)
            {
                return MedalScore[key];
            }
        }

        return 0;
    }

    public int FinalTimerScore()
    {
        Dictionary<int, int> TimerScore = new Dictionary<int, int>()
        {
            { goldMedalThresholdInSeconds, GoldScore },
            { silverMedalThresholdInSeconds, silverScore },
            { bronzeMedalThresholdInSeconds, bronzeScore }
        };
        foreach (int key in TimerScore.Keys)
        {
            if ((int)TimerInSeconds <= key)
            {
                return TimerScore[key];
            }
        }

        return 0;
    }

    //private void CheckForMedal() // check which medal the player is currently running for
    //{
    //    if (TimerInSeconds < goldMedalThresholdInSeconds)
    //    {
    //        FinalTimerScore = GoldScore;
    //        MedalValue = 3; //MedalValue is used to know which medal to display is other scripts
    //    }
    //    else if (TimerInSeconds < silverMedalThresholdInSeconds)
    //    {
    //        FinalTimerScore = silverScore;
    //        MedalValue = 2;
    //    }
    //    else if (TimerInSeconds < bronzeMedalThresholdInSeconds)
    //    {
    //        FinalTimerScore = bronzeScore;
    //        MedalValue = 1;
    //    }
    //    else
    //    {
    //        FinalTimerScore = 0;
    //        MedalValue = 0;
    //    }
    //}

}