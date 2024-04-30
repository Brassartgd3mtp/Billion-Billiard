using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalData : MonoBehaviour
{
    [Header("References")]

    private StarRating starRatingScript;
    private ScoringCalculations calculationsScript;
    private LevelTimer timerScript;

    private void Awake()
    {
        starRatingScript = GetComponent<StarRating>();
        calculationsScript = GetComponent<ScoringCalculations>();
        timerScript = GetComponent<LevelTimer>();
    }
    public void SetScores() // Here we chec if we are in a situation to get a new highscore, if yes we set the highscore to be the value the player just scored
    {
        //stars
        if (starRatingScript.numberOfStars > PlayerPrefs.GetFloat("stars" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetFloat("stars" + SceneManager.GetActiveScene().buildIndex, starRatingScript.numberOfStars);
        }

        //score
        if (calculationsScript.PlayerScore > PlayerPrefs.GetFloat("hiscore" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetFloat("hiscore" + SceneManager.GetActiveScene().buildIndex, calculationsScript.PlayerScore);
        }

        //timer
        if (PlayerPrefs.GetFloat("PBinSeconds" + SceneManager.GetActiveScene().buildIndex) ==0 || timerScript.TimerInSeconds <= PlayerPrefs.GetFloat("PBinSeconds" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetFloat("PBinSeconds" + SceneManager.GetActiveScene().buildIndex, timerScript.TimerInSeconds);
            PlayerPrefs.SetString("PB" + SceneManager.GetActiveScene().buildIndex, string.Format("{0:00}:{1:00}", timerScript.Minutes, timerScript.Seconds));
        }

        //medal
        if(timerScript.MedalValue > PlayerPrefs.GetInt("MedalValue" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetInt("MedalValue" + SceneManager.GetActiveScene().buildIndex, timerScript.MedalValue);
        }
    }

}
