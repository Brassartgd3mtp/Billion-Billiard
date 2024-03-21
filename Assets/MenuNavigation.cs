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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null) 
        {
            if (Gamepad.current.buttonSouth.isPressed || Gamepad.current.leftStick.x.value != 0 || Gamepad.current.leftStick.y.value != 0 ||
                Gamepad.current.dpad.up.isPressed || Gamepad.current.dpad.down.isPressed)
            {
                EventSystem.current.SetSelectedGameObject(playButton);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                MouseKeybordIsActive = false;
                GamepadIsActive = true;             
            }
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                MouseKeybordIsActive = true;
                GamepadIsActive = false;
            }
        }
    }

}
