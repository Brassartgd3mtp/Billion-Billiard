using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    Scene activeScene;

    void Start()
    {
        activeScene = SceneManager.GetActiveScene();

        InputHandler.ReloadSceneEnable(this);
    }

    public void Reload(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(activeScene.name);
    }

    private void OnDisable()
    {
        InputHandler.ReloadSceneDisable();
    }

}
