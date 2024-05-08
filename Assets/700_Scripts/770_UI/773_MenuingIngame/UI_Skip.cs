using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Skip : MonoBehaviour
{
    public CanvasGroup DisplayToSkip;
    public GameObject XboxGamepad;
    public GameObject Mouse;
    public GameObject NextObjToShow;

    public bool AsFadeOut;
    public bool AsTimer;

    private float Timer = 2;

    // Start is called before the first frame update
    void Start()
    {
        if (!AsTimer)
        {
            InputHandler.UISkipEnable(this);
        }
        else
        {
            XboxGamepad.gameObject.SetActive(false);
            Mouse.gameObject.SetActive(false);
        }

        InputHandler.PlayerControllerDisable();
        InputHandler.TrajectoryPredictionDisable();
        InputHandler.RoomCamDisable();
        InputHandler.PauseMenuDisable();

        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (AsTimer)
        {
            if(Timer <= 0f)
            {
                if (SwapControls.state == CurrentState.Gamepad)
                {
                    XboxGamepad.gameObject.SetActive(true);
                    Mouse.gameObject.SetActive(false);
                }
                else
                {
                    XboxGamepad.gameObject.SetActive(false);
                    Mouse.gameObject.SetActive(true);
                }
                InputHandler.UISkipEnable(this);
            }
            else
            {
                Timer -= Time.deltaTime;
            }
        }
        
    }

    public void SkipCanva(InputAction.CallbackContext context)
    {
        InputHandler.PlayerControllerEnable();
        InputHandler.TrajectoryPredictionEnable();
        InputHandler.RoomCamEnable();
        InputHandler.PauseMenuEnable();

        InputHandler.UISkipDisable();

        Time.timeScale = 1f;

        if (DisplayToSkip != null)
            if (AsFadeOut)
                StartCoroutine(CanvaFadeOut());

            else
            {
                DisplayToSkip.alpha = 0;
            }

        

    }

    IEnumerator CanvaFadeOut()
    {
        while(DisplayToSkip.alpha > 0)
        {
            DisplayToSkip.alpha -= 0.02f;
            yield return null;
        }
        if (DisplayToSkip.alpha <= 0)
        {
            if (NextObjToShow != null)
            {
                if (DisplayToSkip.alpha <= 0.4)
                {
                    NextObjToShow.SetActive(true);
                }
            }
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

        InputHandler.UISkipDisable();
    }
}
