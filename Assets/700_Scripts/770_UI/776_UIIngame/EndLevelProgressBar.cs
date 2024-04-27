using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelProgressBar : MonoBehaviour
{
    [Header("References")]

    private LevelScore levelScoreScript;
    private PlayerStats playerStatsScript;

    private Slider slider;
    private ParticleSystem progressBarParticleSystem;

    [Header("SliderBehaviour")]

    private float targetProgress;
    private float fillspeed = 0.25f;
    private float currentScore; // it's moneyCount but float instead of int
    private void Awake()
    {
        slider = GetComponent<Slider>();
        progressBarParticleSystem = GetComponentInChildren<ParticleSystem>();

        levelScoreScript = FindAnyObjectByType<LevelScore>();
        playerStatsScript = PlayerStats.Instance;

        currentScore = playerStatsScript.moneyCount;
    }

    private void Start()
    {
        IncrementProgress(currentScore / levelScoreScript.TotalScore);
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
