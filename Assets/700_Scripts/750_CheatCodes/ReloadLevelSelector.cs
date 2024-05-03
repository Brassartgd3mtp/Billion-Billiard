using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReloadLevelSelector : MonoBehaviour
{
    void Start()
    {
        InputHandler.ReloadLSEnable(this);
    }

    public void Reload(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(1);
    }

    private void OnDisable()
    {
        InputHandler.ReloadLSDisable();
    }
}