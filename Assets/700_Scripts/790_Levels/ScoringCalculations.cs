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
        levelTimerScript = FindAnyObjectByType<LevelTimer>();
        levelScoreScript = FindAnyObjectByType<LevelScore>();
        playerStatsScript = FindAnyObjectByType<PlayerStats>();

        Debug.Log(playerStatsScript);
    }

    public float CalculateTotalScore()
    {
        float scoreIngot = levelScoreScript.TotalIngotScore;
        float scoreTimer = levelTimerScript.GoldScore;
        float total = scoreIngot + scoreTimer;

        return total;
    }

public float CalculatePlayerScore()
    {

        float scoreMoney = playerStatsScript.moneyCount;
        float scoreTimer = levelTimerScript.FinalTimerScore;
        float total = scoreMoney + scoreTimer;

        return total;
    }
}
