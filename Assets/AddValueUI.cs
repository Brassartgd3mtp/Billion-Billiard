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
        
    }
    public void updateUI(int value)
    {
        Text_Value.text = $"+{value}";
    }
}
