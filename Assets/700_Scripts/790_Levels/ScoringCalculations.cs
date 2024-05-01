using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringCalculations : MonoBehaviour
{
    [Header("References")]
    private LevelScore levelScoreScript;
    private LevelTimer levelTimerScript;
    private PlayerStats playerStatsScript;

    [Header("Scores")]

    public float PlayerScore;

    private void Awake()
    {
        levelTimerScript = GetComponent<LevelTimer>();
        levelScoreScript = GetComponent<LevelScore>();
        playerStatsScript = FindAnyObjectByType<PlayerStats>();
    }

    public float CalculateTotalScore() // calculate the maximum score possible
    {
        return levelScoreScript.TotalIngotScore // the maximum score possible to have with ingots
             + levelTimerScript.GoldScore; // the score you get with a gold medal
    }

public float CalculatePlayerScore() // walculate the score the player just got
    {
        PlayerScore = playerStatsScript.moneyCount // the money the player has at the end of the level
                    + levelTimerScript.FinalTimerScore; // the score the player got with their time

        return PlayerScore;
    }
}
