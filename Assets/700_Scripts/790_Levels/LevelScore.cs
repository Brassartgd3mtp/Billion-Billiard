using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelScore : MonoBehaviour
{
    public int TotalIngotScore = 0;

    private void Awake()
    {
        MoneyStats[] allIngots = FindObjectsByType<MoneyStats>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);
        
        foreach(MoneyStats ingot in allIngots)
        {
            TotalIngotScore += ingot.value;
        }
    }
}