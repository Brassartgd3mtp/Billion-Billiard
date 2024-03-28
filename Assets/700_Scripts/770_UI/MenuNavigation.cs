using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public GameObject playButton;

    public bool GamepadIsActive, MouseKeybordIsActive;
    public void Start()
    {
        if (SwapControls.state == CurrentState.Gamepad)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (SwapControls.state == CurrentState.Gamepad)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(playButton);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                MouseKeybordIsActive = false;
                GamepadIsActive = true;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GamepadIsActive = false;
            MouseKeybordIsActive = true;
        }
    }
}
