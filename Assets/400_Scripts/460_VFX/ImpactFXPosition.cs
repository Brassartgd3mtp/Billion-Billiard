using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactFXPosition : MonoBehaviour
{
    public VFXScriptableObject vFXScriptableObject;
    public float velocityThreshold = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Obtient la position de la collision
        Vector3 collisionPosition = collision.contacts[0].point;

        // Obtient la normale de la surface de la collision
        Vector3 collisionNormal = collision.contacts[0].normal * 1;

        // Calcule la rotation à partir de la normale
        Quaternion rotationFromNormal = Quaternion.LookRotation(collisionNormal);

        float impactVelocity = collision.relativeVelocity.magnitude;
        Debug.Log(impactVelocity);

        GameObject particlePrefab;
        if (collision.transform.TryGetComponent(out Obstacle _obstacle))
        {
            Obstacle.ObstacleType obstacleType = _obstacle.obstacleType;

            particlePrefab = vFXScriptableObject.GetObstacleType(obstacleType);
        }
        else particlePrefab = vFXScriptableObject.prefabParticleDefault;


        if (impactVelocity > velocityThreshold)
        {
            Instantiate(particlePrefab, collisionPosition, rotationFromNormal);
            //particlePrefab.
        }

        // Détruit la particule après un certain temps
        //Destroy(particleInstance, particleInstance.GetComponent<ParticleSystem>().main.duration);
    }
}