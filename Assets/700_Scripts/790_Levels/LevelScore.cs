using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScore : MonoBehaviour
{
    [Header("References")]

    [Header("Ingot Lists")] // the lists of all the differents ingots

    private List<GameObject> bronzeIngots = new List<GameObject>();
    private List<GameObject> silverIngots = new List<GameObject>();
    private List<GameObject> goldIngots = new List<GameObject>();
    private List<GameObject> bigGoldIngots = new List<GameObject>();
    private List<GameObject> platiniumIngots = new List<GameObject>();


    [Header("Score")] // all the total score values for each list of ingots

    private int bronzeScore;
    private int silverScore;
    private int goldScore;
    private int bigGoldScore;
    private int platiniumScore;

    [HideInInspector] public int TotalIngotScore;


    private void Awake()
    {

    // First we find all the ingots and put then in the lists

        // we create an array that will contain all the ingots
        MoneyStats[] allIngots = FindObjectsByType<MoneyStats>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);
        
        //then we sort the ingots to get their value later
        foreach(MoneyStats ingot in allIngots)
        {
            if (ingot.type == IngotType.Bronze)
            {
                bronzeIngots.Add(ingot.gameObject);
            }
            else if (ingot.type == IngotType.Silver)
            {
                silverIngots.Add(ingot.gameObject);
            }
            else if (ingot.type == IngotType.Gold)
            {
                goldIngots.Add(ingot.gameObject);
            }
            else if (ingot.type == IngotType.BigGold)
            {
                bigGoldIngots.Add(ingot.gameObject);
            }
            else if (ingot.type == IngotType.Platinium)
            {
                platiniumIngots.Add(ingot.gameObject);
            }
            else break;
        }

     // Then we calculate the total score avaliable in the level by summing up the score of all the types of ingots

        // Get the score of all the bronze ingots
        if (bronzeIngots.Count > 0)
        {
            if (bronzeIngots[0].gameObject.TryGetComponent(out MoneyStats moneyStats))
            {
                bronzeScore = moneyStats.value * bronzeIngots.Count;
            }
        }
        else bronzeScore = 0;

        // Get the score of all the silver ingots
        if (silverIngots.Count > 0)
        {
            if (silverIngots[0].gameObject.TryGetComponent(out MoneyStats moneyStats))
            {
                silverScore = moneyStats.value * silverIngots.Count;
            }
        }
        else silverScore = 0;

        // Get the score of all the gold ingots
        if (goldIngots.Count > 0)
        {
            if (goldIngots[0].gameObject.TryGetComponent(out MoneyStats moneyStats))
            {
                goldScore = moneyStats.value * goldIngots.Count;
            }
        }
        else goldScore = 0;

        // Get the score of all the big gold ingots
        if (bigGoldIngots.Count > 0)
        {
            if (bigGoldIngots[0].gameObject.TryGetComponent(out MoneyStats moneyStats))
            {
                bigGoldScore = moneyStats.value * bigGoldIngots.Count;
            }
        }
        else bigGoldScore = 0;

        // Get the score of all the platinium ingots
        if (platiniumIngots.Count > 0)
        {
            if (platiniumIngots[0].gameObject.TryGetComponent(out MoneyStats moneyStats))
            {
                platiniumScore = moneyStats.value * platiniumIngots.Count;
            }
        }
        else platiniumScore = 0;


        // Get the total score
        TotalIngotScore = bronzeScore + silverScore + goldScore + bigGoldScore + platiniumScore;

    }


}
