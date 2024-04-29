using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRating : MonoBehaviour
{
    [Header("References")]

    private ScoringCalculations calculationsScript;
    private GlobalData globalDataScript;

    [Header("Objects")]

    [SerializeField] private GameObject firstStar;
    [SerializeField] private GameObject secondStar;
    [SerializeField] private GameObject thirdStar;

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

    public void CalculateStarRating()
    {
        calculationsScript.PlayerScore = calculationsScript.CalculatePlayerScore();

        if (HasWon)
        {
            if (calculationsScript.PlayerScore > scoreForStarTwo)
            {
                numberOfStars++;
                // logique pour afficher etoile deux
            }
            if (calculationsScript.PlayerScore > scoreForStarThree)
            {
                numberOfStars++;
                // logique pour afficher etoile trois
            }
        }
        else
        {
            numberOfStars = 0;
        }
        globalDataScript.SetScores();

    }

}
