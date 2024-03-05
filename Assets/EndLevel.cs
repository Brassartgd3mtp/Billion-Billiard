using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public int nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (Gamepad.current != null)
            {
                Gamepad.current.ResetHaptics();
            }
            SceneManager.LoadScene(nextLevel);
        }
    }

    private void OnDisable()
    {
        if (Gamepad.current != null)
        {
        Gamepad.current.ResetHaptics();
        }
    }
}
