using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Skip : MonoBehaviour
{
    public GameObject DisplayToSkip;

    public GameObject XboxGamepad;
    public GameObject Mouse;
    public GameObject NextObjToShow;

    // Start is called before the first frame update
    void Start()
    {
        InputHandler.UISkipEnable(this);

        InputHandler.PlayerControllerDisable();
        InputHandler.TrajectoryPredictionDisable();
        InputHandler.RoomCamDisable();
        InputHandler.PauseMenuDisable();

        Time.timeScale = 0f;
    }
    private void Update()
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
    }

    public void SkipCanva(InputAction.CallbackContext context)
    {
        InputHandler.PlayerControllerEnable();
        InputHandler.TrajectoryPredictionEnable();
        InputHandler.RoomCamEnable();
        InputHandler.PauseMenuEnable();

        InputHandler.UISkipDisable();

        Time.timeScale = 1f;

        if (NextObjToShow != null) 
            NextObjToShow.SetActive(true);

        if (DisplayToSkip != null)
            DisplayToSkip.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

        InputHandler.UISkipDisable();
    }
}
