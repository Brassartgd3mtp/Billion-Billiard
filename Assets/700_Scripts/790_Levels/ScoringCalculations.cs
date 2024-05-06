using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringCalculations : MonoBehaviour
{
    [Header("References")]
    private LevelScore levelScoreScript;
    private LevelTimer levelTimerScript;
    private PlayerStats playerStatsScript;

    private void Awake()
    {
        levelTimerScript = GetComponent<LevelTimer>();
        levelScoreScript = GetComponent<LevelScore>();
        playerStatsScript = FindAnyObjectByType<PlayerStats>();
    }

    public float MaximumScore()
    {
        return levelScoreScript.TotalIngotScore
            * levelScoreScript.NeededPercentage / 100
             + levelTimerScript.GoldScore;
    }

    public int PlayerScore()
    {
        return playerStatsScript.moneyCount
                    + levelTimerScript.FinalTimerScore();
    }
}
