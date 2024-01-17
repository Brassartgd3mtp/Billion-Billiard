using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactFXPosition : MonoBehaviour
{

    public GameObject particlePrefab; // Drag-and-drop le prefab dans l'éditeur Unity

    void OnCollisionEnter(Collision collision)
    {
        // Obtient la position de la collision
        Vector3 collisionPosition = collision.contacts[0].point;

        // Obtient la normale de la surface de la collision
        Vector3 collisionNormal = collision.contacts[0].normal * -1;

        // Calcule la rotation à partir de la normale
        Quaternion rotationFromNormal = Quaternion.LookRotation(collisionNormal);

        // Instancie le Particle System à partir du prefab à la position de la collision avec une rotation basée sur la normale
        GameObject particleInstance = Instantiate(particlePrefab, collisionPosition, rotationFromNormal);

        // Détruit la particule après un certain temps
        Destroy(particleInstance, particleInstance.GetComponent<ParticleSystem>().main.duration);

        Debug.Log(collisionNormal);
    }
}