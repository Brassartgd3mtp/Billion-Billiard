using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScore : MonoBehaviour
{
    [Header("References")]

    private MoneyStats moneyStatsScript; // a reference for the script that holds the ingots vvalues


    [Header("Ingot Lists")] // the lists of all the differents ingots

    [SerializeField] private List<GameObject> bronzeIngots = new List<GameObject>();
    [SerializeField] private List<GameObject> silverIngots = new List<GameObject>();
    [SerializeField] private List<GameObject> goldIngots = new List<GameObject>();
    [SerializeField] private List<GameObject> platiniumIngots = new List<GameObject>();

    [Header("Score")] // all the total score values for each list of ingots

    private int bronzeScore;
    private int silverScore;
    private int goldScore;
    private int platiniumScore;

    public int TotalScore;

    private void Awake()
    {

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
        TotalScore = bronzeScore + silverScore + goldScore + platiniumScore;

        Debug.Log(bronzeScore);
        Debug.Log(silverScore);
        Debug.Log(goldScore);
        Debug.Log(platiniumScore);
        Debug.Log(TotalScore);
    }


}
