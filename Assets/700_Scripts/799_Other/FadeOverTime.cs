using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeOverTime : MonoBehaviour
{

    [SerializeField] private float startingTime;
    [SerializeField] private float duration;


    float timer = 0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        startingTime -= Time.time;
        if(startingTime <= 0)
        {
            DoFading();
        }
    }

    void DoFading()
    {
        float durationInv = 1f / (duration != 0f ? duration : 1f);
        float alpha = Mathf.Lerp(1f, 0f, timer * durationInv);
        timer += Time.deltaTime;
        GetComponent<TextMeshProUGUI>().color = new Color( 0.23f, 0.88f , 0.25f , alpha);
    }
}
