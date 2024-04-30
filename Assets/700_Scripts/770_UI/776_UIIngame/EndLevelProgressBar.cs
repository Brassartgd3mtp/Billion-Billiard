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

    private float targetProgress;
    private float fillspeed = 0.1f;
    private void Awake()
    {
        slider = GetComponent<Slider>();

        calculationsScript = FindAnyObjectByType<ScoringCalculations>();
    }

    private void Start()
    {
        IncrementProgress(calculationsScript.CalculatePlayerScore() / calculationsScript.CalculateTotalScore());
    }
    private void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += fillspeed * Time.deltaTime;

            if (!progressBarParticleSystem.isPlaying)
                progressBarParticleSystem.Play();
        }
        else progressBarParticleSystem.Stop();
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }


}
