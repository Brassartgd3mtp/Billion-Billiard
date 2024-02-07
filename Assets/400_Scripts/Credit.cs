using UnityEngine;

public class AnimationAccelerator : MonoBehaviour
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
}
