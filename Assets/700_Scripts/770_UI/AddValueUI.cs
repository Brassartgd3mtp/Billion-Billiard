using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddValueUI : MonoBehaviour
{
    public TextMeshProUGUI Text_Value;

    void Start()
    {
        
    }


    void Update()
    {
        if (Text_Value.alpha > 0)
        {
            Text_Value.alpha -= Time.deltaTime;
        }
    }
    public void updateUI(int value)
    {
        Text_Value.text = $"+{value}";
        Text_Value.alpha = 1;
    }
}
