using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_WorldMoney : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float timerBeforeDestroy = 10;

    public void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        transform.Rotate(90, 0, Random.Range(-45f,45f));
    }
    public void UpdateMoney(int value)
    {
        text.text = $" + {value}";
    }

    public void Update()
    {
        timerBeforeDestroy -= Time.deltaTime;

        if(timerBeforeDestroy < 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
