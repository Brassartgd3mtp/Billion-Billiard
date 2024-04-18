using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGaugeGradient : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Gradient colorGradient;

    void Update()
    {
        //Je récupère la valeur Z de l'index 1 qui est entre 0 et -5 que je normalise et j'applique une couleur à partir d'un gradient en fonction de la valeur
        if (lineRenderer != null && colorGradient != null)
        {
            Vector3 sizePosition = lineRenderer.GetPosition(1);

            float normalizedZ = Mathf.InverseLerp(-5f, 0f, sizePosition.z);

            Color lerpedColor = colorGradient.Evaluate(normalizedZ);

            lineRenderer.startColor = lerpedColor;
            lineRenderer.endColor = lerpedColor;
        }
    }
}