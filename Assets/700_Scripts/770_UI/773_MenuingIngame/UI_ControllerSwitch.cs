using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ControllerSwitch : MonoBehaviour
{
    public GameObject controllerImage;
    public GameObject mouseImage;
    public float DisplayTimer = 1;

    [SerializeField] CanvasGroup canvaGroup;

    void Start()
    {
        controllerImage.gameObject.SetActive(false);
        mouseImage.gameObject.SetActive(false);
    }

    public void GamepadIcon()
    {
        controllerImage.gameObject.SetActive(true);
        mouseImage.gameObject.SetActive(false);

        DisplayTimer = 1;
        canvaGroup.alpha = 1;

        StartCoroutine(Timer());
    }

    public void MouseIcon()
    {
        mouseImage.gameObject.SetActive(true);
        controllerImage.gameObject.SetActive(false);

        DisplayTimer = 1;
        canvaGroup.alpha = 1;

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (DisplayTimer > 0)
        {
            DisplayTimer -= Time.deltaTime;
            canvaGroup.alpha = DisplayTimer;
            yield return null;
        }

        yield break;
    }
}