using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private LevelTimer levelTimerScript;
    [SerializeField] private Shake shakeScript;

    [Header("Medals")]

    [SerializeField] private Image IMG_currentMedal;
    [SerializeField] private Image IMG_nextMedal;
    [SerializeField] private List<Sprite> medalSprites;
    private int medalvalue;
    private int previousmedalvalue;

    [Header("Slider")]

    [SerializeField] private Slider timeSlider;
    public float EndningTimeValue;
    [SerializeField] private Image IMG_fillImage;

    [Header("Texts")]

    [SerializeField] private TextMeshProUGUI TXT_nextThreshold;

    [Header("Blink")]

    [SerializeField] private float blinkSpeed;
    private bool isBlinking;

    // Start is called before the first frame update
    void Start()
    {
        EndningTimeValue = levelTimerScript.bronzeMedalThresholdInSeconds;

        timeSlider.value = timeSlider.maxValue;

        medalvalue = levelTimerScript.MedalValue();
        previousmedalvalue = medalvalue;
        UpdateMedalImage();
        GetFillColor();
        TXT_nextThreshold.text = GetNextTimerThreshold();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelTimerScript.TimeStarted)
        {
            medalvalue = levelTimerScript.MedalValue();

            if (timeSlider.value > 0)
            {
                timeSlider.value = 1 - (levelTimerScript.TimerInSeconds / EndningTimeValue);
                if(!isBlinking)
                {
                    GetFillColor();
                }

                if (medalvalue != previousmedalvalue)
                {
                    UpdateMedalImage();
                    if (levelTimerScript.TimerInSeconds < levelTimerScript.bronzeMedalThresholdInSeconds)
                    {
                        TXT_nextThreshold.text = GetNextTimerThreshold();
                    }
                    else TXT_nextThreshold.text = "-- : --";
                }
            }
            if(CheckIfThresholdIsNear())
            {
                if(!isBlinking)
                {
                    StartCoroutine(StartBlinking(IMG_currentMedal, IMG_currentMedal.sprite, IMG_nextMedal.sprite));
                    StartCoroutine(StartBlinking(levelTimerScript.TXT_Timer, Color.white, Color.red));
               //   StartCoroutine(StartBlinking(IMG_fillImage, IMG_fillImage.color, Color.red));
                }
                shakeScript.StartShake();
            }
        }
        previousmedalvalue = medalvalue;
    }

    void UpdateMedalImage()
    {
            IMG_currentMedal.sprite = medalSprites[medalvalue];
            if(medalvalue >= 1 )
            {
                IMG_nextMedal.sprite = medalSprites[medalvalue - 1];
            }
    }
    void GetFillColor()
    {
        switch(medalvalue)
        {
            case 1: //bronze
                    IMG_fillImage.color = new Color(0.75f, 0.3f, 0.2f);
                break; 
            case 2: //silver
                    IMG_fillImage.color = new Color(0.7f, 0.7f, 0.9f);
                break;
            case 3: //gold
                    IMG_fillImage.color = new Color(0.8f, 0.6f, 0.1f);
                break;
        }
    }

    string GetNextTimerThreshold()
    {

        int time  = 0;
        int minutes;
        int seconds;

        switch (medalvalue)
        {
            case 3:
                time = levelTimerScript.goldMedalThresholdInSeconds;
                break;
            case 2:
                time = levelTimerScript.silverMedalThresholdInSeconds;
                break;
            case 1:
                time = levelTimerScript.bronzeMedalThresholdInSeconds;
                break;
        }

        minutes = (int)Mathf.Floor(time / 60);
        seconds = (int)time % 60;

        if (seconds == 60)
        {
            minutes++;
            seconds = 0;
        }

        return string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    bool CheckIfThresholdIsNear()
    {
        float _blinkSpeed;

        switch(medalvalue) 
        {
            case 3:
                if (levelTimerScript.goldMedalThresholdInSeconds - levelTimerScript.TimerInSeconds <= 5 && levelTimerScript.silverMedalThresholdInSeconds - levelTimerScript.TimerInSeconds > 0)
                {
                    _blinkSpeed = (levelTimerScript.goldMedalThresholdInSeconds - levelTimerScript.TimerInSeconds) / 5;
                    if (_blinkSpeed < 0.2)
                    {
                        _blinkSpeed = 0.2f;
                    }
                    blinkSpeed = _blinkSpeed;
                    return true;
                }

                else
                {
                    blinkSpeed = 0.2f;
                    return false;
                } 

            case 2:
                if (levelTimerScript.silverMedalThresholdInSeconds - levelTimerScript.TimerInSeconds <= 5 && levelTimerScript.silverMedalThresholdInSeconds - levelTimerScript.TimerInSeconds > 0)
                {
                    _blinkSpeed = (levelTimerScript.silverMedalThresholdInSeconds - levelTimerScript.TimerInSeconds)/5;
                    if (_blinkSpeed < 0.2)
                    {
                        _blinkSpeed = 0.2f;
                    }
                    blinkSpeed = _blinkSpeed;
                    return true;
                }
                else
                {
                    blinkSpeed = 0.2f;
                    return false;
                }
            case 1:
                if (levelTimerScript.bronzeMedalThresholdInSeconds - levelTimerScript.TimerInSeconds <= 5 && levelTimerScript.silverMedalThresholdInSeconds - levelTimerScript.TimerInSeconds > 0)
                {
                    _blinkSpeed = (levelTimerScript.bronzeMedalThresholdInSeconds - levelTimerScript.TimerInSeconds)/5;
                    if (_blinkSpeed < 0.2)
                    {
                        _blinkSpeed = 0.2f;
                    }
                    blinkSpeed = _blinkSpeed;
                    return true;
                }
                else
                {
                    blinkSpeed = 0.2f;
                    return false;
                }
        }
    return false;
    }
    private IEnumerator StartBlinking(Image imagetocblink, Color currentColor, Color blinkColor)
    {
        isBlinking = true;
        imagetocblink.color = blinkColor;
        if (blinkSpeed != float.NaN)
        {
            yield return new WaitForSeconds(blinkSpeed);
        }
        imagetocblink.color = currentColor;
        if(blinkSpeed != float.NaN)
        {
            yield return new WaitForSeconds(blinkSpeed);
        }
        isBlinking = false;
    }

    private IEnumerator StartBlinking(TextMeshProUGUI texttoblink, Color currentColor, Color blinkColor)
    {
        isBlinking = true;
        texttoblink.color = blinkColor;
        if (blinkSpeed != float.NaN)
        {
            yield return new WaitForSeconds(blinkSpeed);
        }
        texttoblink.color = currentColor;
        if (blinkSpeed != float.NaN)
        {
            yield return new WaitForSeconds(blinkSpeed);
        }
        isBlinking = false;
    }

    private IEnumerator StartBlinking(Image image,Sprite baseImage, Sprite newImage)
    {
        isBlinking = true;
        image.sprite = newImage;
        if (blinkSpeed != float.NaN)
        {
            yield return new WaitForSeconds(blinkSpeed);
        }
        image.sprite = baseImage;
        if (blinkSpeed != float.NaN)
        {
            yield return new WaitForSeconds(blinkSpeed);
        }
        image.sprite = newImage;
        UpdateMedalImage();
        isBlinking = false;
    }

}
