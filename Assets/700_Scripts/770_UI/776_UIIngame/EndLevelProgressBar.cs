using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelProgressBar : MonoBehaviour
{
    [Header("References")]

    private ScoringCalculations calculationsScript;

    private Slider slider;
    [SerializeField] private ParticleSystem progressBarParticleSystem;

    [Header("SliderBehaviour")]

    public bool BarCanMove; //will be set to false to stop when the bar reaches a star and then go back to true to resume the movement
    private float targetProgress;
    private float fillspeed = 0.2f;
    private void Awake()
    {
        slider = GetComponent<Slider>();

        calculationsScript = FindAnyObjectByType<ScoringCalculations>();
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

                if (!progressBarParticleSystem.isPlaying)
                    progressBarParticleSystem.Play();
            }
            else progressBarParticleSystem.Stop();
        }

    }

    public void IncrementProgress(float newProgress) // define the value the bar is going to reach
    {
        targetProgress = slider.value + newProgress;
    }


}