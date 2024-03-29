using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHelpIndicator : MonoBehaviour
{
    public GameObject helpIndicator;
    private bool isActive;
    void Start()
    {
        helpIndicator.SetActive(false);
        isActive = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            if(isActive)
            {
                helpIndicator.SetActive(false);
                isActive = false;
            }
            if(!isActive)
            {
                helpIndicator.SetActive(true);
                isActive = true;
            }
        }
    }
}
