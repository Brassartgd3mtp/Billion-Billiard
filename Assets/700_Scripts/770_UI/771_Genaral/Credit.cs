using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits_AnimationAccelerator : MonoBehaviour
{
    public Animator animator; // Référence à l'Animator de l'objet à animer

    void Update()
    {
        if (Input.GetButtonDown("Fire2")) // "Fire2" correspond à la touche ronde sur la manette par défaut
        {
            // Déclencher l'accélération de l'animation
            animator.speed = 4f; // Vitesse d'animation accélérée (2x)
        }
        else if (Input.GetButtonUp("Fire2")) // Si la touche est relâchée
        {
            // Remettre la vitesse de l'animation à la normale
            animator.speed = 1f; // Vitesse d'animation normale (1x)
        }
    }
    public void MainMenuButton()
    {
        // Recharge la scène "Main Menu"
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void GoToMainMenu()
    {

    }
}
