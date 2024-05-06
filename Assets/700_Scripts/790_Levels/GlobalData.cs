using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalData : MonoBehaviour
{
    #region References
    private static StarRating starRatingScript;
    private static ScoringCalculations calculationsScript;
    private static LevelTimer timerScript;
    #endregion

    #region StoreData
    public static Dictionary<int, float> Stars = new Dictionary<int, float>()
    {
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},
        {6, 0},
        {7, 0},
        {8, 0},
        {9, 0},
        {10, 0},
        {11, 0},
    };
    public static Dictionary<int, float> Highscore = new Dictionary<int, float>()
    {
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},
        {6, 0},
        {7, 0},
        {8, 0},
        {9, 0},
        {10, 0},
        {11, 0},
    };
    public static Dictionary<int, float> PBInSeconds = new Dictionary<int, float>()
    {
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},
        {6, 0},
        {7, 0},
        {8, 0},
        {9, 0},
        {10, 0},
        {11, 0},
    };
    public static Dictionary<int, string> PB = new Dictionary<int, string>()
    {
        {2, string.Empty},
        {3, string.Empty},
        {4, string.Empty},
        {5, string.Empty},
        {6, string.Empty},
        {7, string.Empty},
        {8, string.Empty},
        {9, string.Empty},
        {10, string.Empty},
        {11, string.Empty},
    };
    public static Dictionary<int, int> MedalValues = new Dictionary<int, int>()
    {
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},
        {6, 0},
        {7, 0},
        {8, 0},
        {9, 0},
        {10, 0},
        {11, 0},
    };
    #endregion

    private void Awake()
    {
        starRatingScript = GetComponent<StarRating>();
        calculationsScript = GetComponent<ScoringCalculations>();
        timerScript = GetComponent<LevelTimer>();
    }

    public static void SaveScores()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (starRatingScript.NumberOfStars() > Stars[currentScene] || Stars[currentScene] == 0)
        {
            Stars[currentScene] = starRatingScript.NumberOfStars();
        }

        if (calculationsScript.PlayerScore() > Highscore[currentScene] || Highscore[currentScene] == 0)
        {
            Highscore[currentScene] = calculationsScript.PlayerScore();
        }

        if (PBInSeconds[currentScene] == 0 || timerScript.TimerInSeconds <= PBInSeconds[currentScene])
        {
            PBInSeconds[currentScene] = timerScript.TimerInSeconds;
            PB[currentScene] = string.Format("{0:00}:{1:00}", timerScript.Minutes, timerScript.Seconds);
        }

        if (timerScript.MedalValue() > MedalValues[currentScene] || MedalValues[currentScene] == 0)
        {
            MedalValues[currentScene] = timerScript.MedalValue();
        }
    }

}
