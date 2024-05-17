using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ImpactFXPosition : MonoBehaviour
{
    public VFXScriptableObject vFXScriptableObject;

    private Rigidbody rb;
    private int soundId;
    public float maxVelocity = 80f;
    public float scaleValueMin = 0.5f;
    public float scaleValueMax = 1.3f;

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

        GameObject particlePrefab;

        if (collision.transform.TryGetComponent(out Obstacle _obstacle))
        {
            Obstacle.ObstacleType obstacleType = _obstacle.obstacleType;

            particlePrefab = vFXScriptableObject.GetObstacleType(obstacleType);
            //soundId = vFXScriptableObject.soundObstacleId;
            //SoundCollision();

            ParticleSystem impactVFX = particlePrefab.GetComponent<ParticleSystem>();
            GameObject particleInstance = Instantiate(particlePrefab, collisionPosition, rotationFromNormal);

            float impactVelocity = collision.relativeVelocity.magnitude;
            float scaleValue = Remap(impactVelocity, 0, maxVelocity, scaleValueMin, scaleValueMax);

            particleInstance.transform.localScale = new Vector3(scaleValue, 1f, scaleValue);

        }
        else particlePrefab = vFXScriptableObject.prefabParticleDefault;
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

    private void SoundCollision()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(soundId, audioSource);
    }


    //Fonction de remapping pour avoir une valeur de scale entre 1 et 2 pour le particle instancié
    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return from2 + (value - from1) * (to2 - from2) / (to1 - from1);
    }
}