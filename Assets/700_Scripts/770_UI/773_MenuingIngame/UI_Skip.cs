using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Skip : MonoBehaviour
{
    public GameObject DisplayToSkip;

    // Start is called before the first frame update
    void Start()
    {
        InputHandler.UISkipEnable(this);

        InputHandler.PlayerControllerDisable();
        InputHandler.FreeCamDisable();
        InputHandler.PauseMenuDisable();

        DisplayToSkip.SetActive(true);
    }

    public void SkipCanva(InputAction.CallbackContext context)
    {
        if (DisplayToSkip.activeSelf)
        {
            DisplayToSkip.SetActive(false);

            InputHandler.PlayerControllerEnable();
            InputHandler.FreeCamEnable();
            InputHandler.PauseMenuEnable();

            InputHandler.UISkipDisable();
        }
    }
}
