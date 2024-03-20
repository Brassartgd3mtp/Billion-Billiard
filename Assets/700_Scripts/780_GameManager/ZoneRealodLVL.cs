using UnityEngine;
using System.Collections;

public class ResetPlayerPosition : MonoBehaviour
{
    public Transform resetPosition; // Position où le joueur doit être remis
    public float resetDelay = 0.2f; // Délai avant que le joueur puisse bouger à nouveau après le reset

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ResetPlayerCoroutine(other)); // Démarrer la coroutine pour réinitialiser le joueur
        }
    }

    private IEnumerator ResetPlayerCoroutine(Collider playerCollider)
    {
        // Assurez-vous que la position de réinitialisation est définie
        if (resetPosition != null)
        {
            Rigidbody playerRigidbody = playerCollider.GetComponent<Rigidbody>();

            // Désactiver le rigidbody pour arrêter le mouvement
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
                playerRigidbody.isKinematic = true;
            }

            // Déplacer le joueur à la position de réinitialisation
            playerCollider.transform.position = resetPosition.position;
            playerCollider.transform.rotation = resetPosition.rotation;

            yield return new WaitForSeconds(resetDelay);

            // Réactiver le rigidbody après le délai
            if (playerRigidbody != null)
            {
                playerRigidbody.isKinematic = false;
            }
        }
        else
        {
            Debug.LogWarning("Reset position not set!"); // Afficher un avertissement si la position de réinitialisation n'est pas définie
        }
    }
}
