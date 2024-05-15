using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private LevelTimer levelTimerScript;

    [Header("Images")]

    [SerializeField] private Image IMG_currentMedal;
    [SerializeField] private Image IMG_nextMedal;
    [SerializeField] private List<Sprite> medalSprites;

    [Header("Slider")]

    [SerializeField] private Slider timeSlider;
    public float EndningTimeValue;
    [SerializeField] private Image fillImage;

    [Header("Texts")]

    [SerializeField] private TextMeshProUGUI beginningTime;
    [SerializeField] private TextMeshProUGUI endingTime;

    // Start is called before the first frame update
    void Start()
    {
        EndningTimeValue = levelTimerScript.bronzeMedalThresholdInSeconds;

        timeSlider.value = timeSlider.maxValue;

        StartCoroutine(CheckForCurrentMedal());
    }

    // Update is called once per frame
    void Update()
    {
        if (levelTimerScript.TimeStarted)
        {
            if(timeSlider.value > 0)
            {
                timeSlider.value = 1 - (levelTimerScript.TimerInSeconds / EndningTimeValue);
                GetFillColor();
            }

        }
    }

    IEnumerator CheckForCurrentMedal()
    {
        while(levelTimerScript.TimeStarted)
        {
            int n = levelTimerScript.MedalValue();
            IMG_currentMedal.sprite = medalSprites[n];
            if(n >= 1 )
            {
                IMG_nextMedal.sprite = medalSprites[levelTimerScript.MedalValue() - 1];
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    void GetFillColor()
    {
        switch(levelTimerScript.MedalValue())
        {
            case 1:
                fillImage.color = new Color(0.75f, 0.3f, 0.2f);
                break; 
            case 2:
                fillImage.color = new Color(0.7f, 0.7f, 0.9f);
                break;
            case 3:
                fillImage.color = new Color(0.8f, 0.6f, 0.1f);
                break;
        }
    }
}
