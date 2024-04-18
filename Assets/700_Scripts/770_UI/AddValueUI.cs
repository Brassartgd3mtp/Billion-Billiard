using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddValueUI : MonoBehaviour
{
    public GameObject PopUpText;
    public GameObject AddedValueEmptyParent;

    void Start()
    {
        
    }


    void Update()
    {

    }
    public void updateUI(int value)
    {
        if(PopUpText.TryGetComponent(out TextMeshProUGUI textValue))
        {
            textValue.text = $"+{value}";
            textValue.fontSize = (value + 23 + value * 1.5f);
            if(textValue.fontSize > 64)
            {
                textValue.fontSize = 64;
            }
            GameObject newText = Instantiate(PopUpText,AddedValueEmptyParent.transform);  
            
        }
    }
}
