using System.Collections;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [Header("Fog Settings")]
    public float targetFogDensity = 0.05f;  // Densit� de fog cible lorsque le joueur est dans la zone.
    public float transitionSpeed = 1.0f;    // Vitesse de transition de la densit� de fog.

    private float initialFogDensity;        // Densit� de fog initiale.

    private void Start()
    {
        initialFogDensity = RenderSettings.fogDensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est entr� dans la zone. Augmentez la densit� de fog.
            StartCoroutine(ChangeFogDensity(targetFogDensity));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur a quitt� la zone. R�tablissez la densit� de fog initiale.
            StartCoroutine(ChangeFogDensity(initialFogDensity));
        }
    }

    private IEnumerator ChangeFogDensity(float targetDensity)
    {
        float currentDensity = RenderSettings.fogDensity;
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionSpeed)
        {
            elapsedTime += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(currentDensity, targetDensity, elapsedTime / transitionSpeed);
            yield return null;
        }

        RenderSettings.fogDensity = targetDensity;
    }
}