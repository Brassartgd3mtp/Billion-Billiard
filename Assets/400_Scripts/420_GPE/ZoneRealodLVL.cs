using UnityEngine;
using System.Collections;

public class ResetPlayerPosition : MonoBehaviour
{
    public Transform resetPosition; // Position o� le joueur doit �tre remis
    public float resetDelay = 0.2f; // D�lai avant que le joueur puisse bouger � nouveau apr�s le reset

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ResetPlayerCoroutine(other)); // D�marrer la coroutine pour r�initialiser le joueur
        }
    }

    private IEnumerator ResetPlayerCoroutine(Collider playerCollider)
    {
        // Assurez-vous que la position de r�initialisation est d�finie
        if (resetPosition != null)
        {
            Rigidbody playerRigidbody = playerCollider.GetComponent<Rigidbody>();

            // D�sactiver le rigidbody pour arr�ter le mouvement
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
                playerRigidbody.isKinematic = true;
            }

            // D�placer le joueur � la position de r�initialisation
            playerCollider.transform.position = resetPosition.position;
            playerCollider.transform.rotation = resetPosition.rotation;

            yield return new WaitForSeconds(resetDelay);

            // R�activer le rigidbody apr�s le d�lai
            if (playerRigidbody != null)
            {
                playerRigidbody.isKinematic = false;
            }
        }
        else
        {
            Debug.LogWarning("Reset position not set!"); // Afficher un avertissement si la position de r�initialisation n'est pas d�finie
        }
    }
}
