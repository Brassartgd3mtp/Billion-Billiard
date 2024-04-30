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

    public float CalculateTotalScore()
    {

        float scoreIngot = levelScoreScript.TotalIngotScore;
        float scoreTimer = levelTimerScript.GoldScore;
        float total = scoreIngot*levelScoreScript.PercentageNecessaryForMaxScore/100 + scoreTimer;

        return total;
    }

public float CalculatePlayerScore()
    {

        float scoreMoney = playerStatsScript.moneyCount;
        float scoreTimer = levelTimerScript.FinalTimerScore;
        PlayerScore = scoreMoney + scoreTimer;

        return PlayerScore;
    }
}
