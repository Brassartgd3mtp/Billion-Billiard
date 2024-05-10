using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class StarRating : MonoBehaviour
{
    [Header("References")]

    private ScoringCalculations calculationsScript;

    [Header("Score Per Star")]

    public int scoreForStarTwo;
    public int scoreForStarThree;

    private void Awake()
    {
        calculationsScript = GetComponent<ScoringCalculations>();
    }

    public int NumberOfStars()
    {
        Dictionary<int, int> StarScore = new Dictionary<int, int>()
        {
            { scoreForStarThree, 3 },
            { scoreForStarTwo, 2 }
        };

        foreach (int key in StarScore.Keys)
        {
            if (calculationsScript.PlayerScore() >= key)
            {
                return StarScore[key];
            }
        }

        return 1;
    }

    //if (HasWon) //Passes to true when the victory screen is displayed
    //{
    //    //we check if the current score is higher than the prerequisite to get two, then three stars
    //    if (calculationsScript.PlayerScore() >= scoreForStarTwo)
    //    {
    //        numberOfStars++;
    //    }
    //    if (calculationsScript.PlayerScore() >= scoreForStarThree)
    //    {
    //        numberOfStars++;
    //    }
    //}
    //else // in case this script has to be called outside of the victory screen, if a way to lose is implemented
    //{
    //    numberOfStars = 0;
    //}
    //globalDataScript.SetScores(); // the function to store all the score data
}
