using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRating : MonoBehaviour
{
    [Header("References")]

    private ScoringCalculations calculationsScript;
    private GlobalData globalDataScript;

    [Header("Score Per Star")]

    [SerializeField] private float scoreForStarTwo;
    [SerializeField] private float scoreForStarThree;

    [Header("Win Condition Logic")]

    [HideInInspector] public int numberOfStars = 0;
    [HideInInspector] public bool HasWon = false;

    private void Awake()
    {
        calculationsScript = GetComponent<ScoringCalculations>();
        globalDataScript = GetComponent<GlobalData>();
    }

    public void CalculateStarRating() //here we calculate how much stars the player scored in this run
    {
        calculationsScript.PlayerScore = calculationsScript.CalculatePlayerScore(); //we calculate the player score

        if (HasWon) //Passes to true when the victory screen is displayed
        {
            //we check if the current score is higher than the prerequisite to get two, then three stars
            if (calculationsScript.PlayerScore > scoreForStarTwo)
            {
                numberOfStars++;
            }
            if (calculationsScript.PlayerScore > scoreForStarThree)
            {
                numberOfStars++;
            }
        }
        else // in case this script has to be called outside of the victory screen, if a way to lose is implemented
        {
            numberOfStars = 0;
        }
        globalDataScript.SetScores(); // the function to store all the score data

    }

}
