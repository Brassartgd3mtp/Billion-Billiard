using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class EndLevelProgressBar : MonoBehaviour
{
    [Header("References")]

    private ScoringCalculations calculationsScript;
    private StarRating starRatingScript;
    private VictoryScreen victoryScreenScript;

    private Slider slider;
    [SerializeField] private ParticleSystem progressBarParticleSystem;
    [SerializeField] private GameObject posSetter;

    [Header("SliderBehaviour")]

    public bool BarCanMove; //will be set to false to stop when the bar reaches a star and then go back to true to resume the movement
    private float targetProgress;
    private float fillspeed = 0.2f;

    public UnityEvent OnSecondStarValueReached;
    public UnityEvent OnThirdStarValueReached;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        calculationsScript = FindAnyObjectByType<ScoringCalculations>();
        starRatingScript = FindAnyObjectByType<StarRating>();
        victoryScreenScript = GetComponentInParent<VictoryScreen>();

        slider.value = starRatingScript.scoreForStarTwo / calculationsScript.MaximumScore();
        victoryScreenScript.IMG_star2.transform.position = posSetter.transform.position;

        slider.value = starRatingScript.scoreForStarThree / calculationsScript.MaximumScore();
        victoryScreenScript.IMG_star3.transform.position = posSetter.transform.position;

        slider.value = slider.minValue;
        victoryScreenScript.IMG_star1.transform.localPosition = slider.fillRect.localPosition;
    }
    private void Start()
    {
        IncrementProgress(calculationsScript.PlayerScore() / calculationsScript.MaximumScore());
    }
    private void Update() // the progression of the progress bar, same logic as a timer
    {
        if (BarCanMove)
        {
            if (slider.value < targetProgress)
            {
                slider.value += fillspeed * Time.deltaTime;

                if (!victoryScreenScript.GO_GoldenStar2.activeSelf && slider.value >= starRatingScript.scoreForStarTwo / calculationsScript.MaximumScore())
                {
                    OnSecondStarValueReached.Invoke();
                }
                if (!victoryScreenScript.GO_GoldenStar3.activeSelf && slider.value >= starRatingScript.scoreForStarThree / calculationsScript.MaximumScore())
                {
                    OnThirdStarValueReached.Invoke();
                }
            }
        }
    }
    public void IncrementProgress(float newProgress) // define the value the bar is going to reach
    {
        targetProgress = slider.value + newProgress;
    }
}