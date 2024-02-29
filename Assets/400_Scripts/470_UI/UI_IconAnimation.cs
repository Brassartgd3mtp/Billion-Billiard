using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_IconAnimation : MonoBehaviour
{
    public float animationDuration = 0.05f; // Durée totale de l'animation
    public float scaleChangeDuration = 1f; // Durée pour un changement complet de scale

    void Start()
    {
        StartCoroutine(ScaleAnimationCoroutine());
    }

    IEnumerator ScaleAnimationCoroutine()
    {
        while (true)
        {
            yield return ScaleOverTime(Vector3.one, new Vector3(0.5f, 0.5f, 0.5f), scaleChangeDuration);

            yield return new WaitForSeconds(animationDuration/2);

            yield return ScaleOverTime(new Vector3(0.5f, 0.5f, 0.5f), Vector3.one, scaleChangeDuration);

            yield return new WaitForSeconds(animationDuration/2);
        }
    }

    IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float duration)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        transform.localScale = endScale;
    }
}
