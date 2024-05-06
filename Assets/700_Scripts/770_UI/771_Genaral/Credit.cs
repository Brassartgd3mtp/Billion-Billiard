using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits_AnimationAccelerator : MonoBehaviour
{
    public Animator animator; // R�f�rence � l'Animator de l'objet � animer

    void Update()
    {
        if (Input.GetButtonDown("Fire2")) // "Fire2" correspond � la touche ronde sur la manette par d�faut
        {
            // D�clencher l'acc�l�ration de l'animation
            animator.speed = 4f; // Vitesse d'animation acc�l�r�e (2x)
        }
        else if (Input.GetButtonUp("Fire2")) // Si la touche est rel�ch�e
        {
            // Remettre la vitesse de l'animation � la normale
            animator.speed = 1f; // Vitesse d'animation normale (1x)
        }
    }
    public void MainMenuButton()
    {
        // Recharge la sc�ne "Main Menu"
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void GoToMainMenu()
    {

    }
}
