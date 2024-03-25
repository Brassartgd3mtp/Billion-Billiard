using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ControllerSwitch : MonoBehaviour
{
    public GameObject controllerImage;
    public GameObject mouseImage;
    private float TimeToDisplay = 2;
    private bool Timer = false;
    private bool TimerFinished = false;

    void Start()
    {
        controllerImage.gameObject.SetActive(false);
        mouseImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (SwapControls.state == CurrentState.Gamepad)
        {
            controllerImage.gameObject.SetActive(true);
            mouseImage.gameObject.SetActive(false);
            Timer = true;
        }
        else
        {
            mouseImage.gameObject.SetActive(true);
            controllerImage.gameObject.SetActive(false);
            Timer = true;
        }

        if (Timer)
        {
            TimeToDisplay -= Time.deltaTime;
            if (TimeToDisplay <= 0)
            {
                Timer = false;
                TimerFinished = true;
            }
        }

        if(TimerFinished)
        {
            controllerImage.gameObject.SetActive(false);
            mouseImage.gameObject.SetActive(false);
            TimeToDisplay = 2;
            TimerFinished = false;
        }

        
    }
}
