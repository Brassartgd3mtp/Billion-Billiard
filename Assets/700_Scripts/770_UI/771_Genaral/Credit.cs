using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credits_AnimationAccelerator : MonoBehaviour
{
    public Animator animator; // Référence à l'Animator de l'objet à animer

    private void OnEnable()
    {
        InputHandler.CreditsEnable(this);
    }

    public void MainMenuButton()
    {
        // Recharge la scène "Main Menu"
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    internal void Accelerate(InputAction.CallbackContext context)
    {
        if (context.performed)
            animator.speed = 4f;
        if (context.canceled)
            animator.speed = 1f;
    }

    internal void Exit(InputAction.CallbackContext context)
    {
        MainMenuButton();
    }

    private void OnDisable()
    {
        InputHandler.CreditsDisable();
    }
}
