using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private LevelTimer levelTimerScript;

    [Header("Images")]

    [SerializeField] private Image IMG_currentMedal;
    [SerializeField] private List<Sprite> medalSprites;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForCurrentMedal());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckForCurrentMedal()
    {
        while(levelTimerScript.TimeStarted)
        {
            IMG_currentMedal.sprite = medalSprites[levelTimerScript.MedalValue()];
            yield return new WaitForSeconds(0.5f);
        }
    }

}
