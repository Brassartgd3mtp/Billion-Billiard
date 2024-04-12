using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public int nextLevel;
    public GameObject VictoryScreen;

    public void Start()
    {
        nextLevel = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            InputHandler.PlayerControllerDisable();
            InputHandler.Actions.Gamepad.Disable();
            InputHandler.Actions.MouseKeyboard.Disable();

            if (SwapControls.state == CurrentState.Gamepad)
            {
                Gamepad.current.ResetHaptics();
            }

            //SceneManager.LoadScene(nextLevel);
            VictoryScreen.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (SwapControls.state == CurrentState.Gamepad)
        {
            Gamepad.current.ResetHaptics();
        }
    }
}
