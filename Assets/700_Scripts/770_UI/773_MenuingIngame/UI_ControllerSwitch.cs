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

    //Ces valeurs sont à supprimer, c'est juste pour voir si le script global fonctionne.
    public bool controlSwitchToMouse = false;
    public bool controlSwitchToController = false;


    void Start()
    {
        controllerImage.gameObject.SetActive(false);
        mouseImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (controlSwitchToMouse)
        {
            mouseImage.gameObject.SetActive(true);
            Timer = true;
        }
        if (controlSwitchToController)
        {
            controllerImage.gameObject.SetActive(true);
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
