using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public int nextLevel;

    public void Start()
    {
        nextLevel = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (SwapControls.state == CurrentState.Gamepad)
            {
                Gamepad.current.ResetHaptics();
            }
            SceneManager.LoadScene(nextLevel);
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
