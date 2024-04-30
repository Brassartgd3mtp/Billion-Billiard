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
    public void SetScores()
    {
        Debug.Log(starRatingScript.numberOfStars);
        Debug.Log(calculationsScript.PlayerScore);
        Debug.Log(timerScript.TimerInSeconds);
        Debug.Log(PlayerPrefs.GetFloat("hiscore" + SceneManager.GetActiveScene()));
        if (starRatingScript.numberOfStars > PlayerPrefs.GetFloat("stars" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetFloat("stars" + SceneManager.GetActiveScene().buildIndex, starRatingScript.numberOfStars);
        }
        if (calculationsScript.PlayerScore > PlayerPrefs.GetFloat("hiscore" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetFloat("hiscore" + SceneManager.GetActiveScene().buildIndex, calculationsScript.PlayerScore);
        }
        if (timerScript.TimerInSeconds < PlayerPrefs.GetFloat("PBinSeconds" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetFloat("PBinSeconds" + SceneManager.GetActiveScene().buildIndex, timerScript.TimerInSeconds);
            PlayerPrefs.SetString("PB" + SceneManager.GetActiveScene().buildIndex, string.Format("{0:00}:{1:00}", timerScript.Minutes, timerScript.Seconds));
        }

    }

}
