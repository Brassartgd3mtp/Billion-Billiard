using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ImpactFXPosition : MonoBehaviour
{
    public VFXScriptableObject vFXScriptableObject;
    public float velocityThreshold = 5f;
    public int countBurst = 50;
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

        GameObject particlePrefab;

        if (collision.transform.TryGetComponent(out Obstacle _obstacle))
        {
            Obstacle.ObstacleType obstacleType = _obstacle.obstacleType;

            particlePrefab = vFXScriptableObject.GetObstacleType(obstacleType);
        }
        else particlePrefab = vFXScriptableObject.prefabParticleDefault;


        if (impactVelocity > velocityThreshold)
        {
        
            ParticleSystem impactVFX = particlePrefab.GetComponent<ParticleSystem>();
        
            var burstVFX = impactVFX.emission;
        
            burstVFX.SetBursts(
                new ParticleSystem.Burst[]
                {
                new ParticleSystem.Burst(0.0f, countBurst, 1, 0.025f)
                });
        
            //Debug.Log(countBurst);
        
            Instantiate(particlePrefab, collisionPosition, rotationFromNormal);
        }
        else if (impactVelocity < velocityThreshold)
        {
            ParticleSystem impactVFX = particlePrefab.GetComponent<ParticleSystem>();
        
            var burstVFX = impactVFX.emission;
            
            burstVFX.SetBursts(
                new ParticleSystem.Burst[]
                {
                    new ParticleSystem.Burst(0.0f, countBurst/5, 1, 0.025f)
                });
        
            Debug.Log(countBurst / 5);
        
            Instantiate(particlePrefab, collisionPosition, rotationFromNormal);
        }

        // Détruit la particule après un certain temps
        //Destroy(particleInstance, particleInstance.GetComponent<ParticleSystem>().main.duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        rb = other.attachedRigidbody;
        if (rb == null)
            return;

        Vector3 colliderPosition = other.transform.position;

        Vector3 colliderNormal = other.transform.up;

        Quaternion rotationFromNormal = Quaternion.LookRotation(colliderNormal);

        GameObject particlePrefab;

        if (other.transform.TryGetComponent(out Obstacle _obstacle))
        {
            Obstacle.ObstacleType obstacleType = _obstacle.obstacleType;

            particlePrefab = vFXScriptableObject.GetObstacleType(obstacleType);
        }
        else
            particlePrefab = vFXScriptableObject.prefabParticleDefault;

        Instantiate(particlePrefab, colliderPosition, rotationFromNormal);
    }
}