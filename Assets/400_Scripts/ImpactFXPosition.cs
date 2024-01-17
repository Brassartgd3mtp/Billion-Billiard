using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactFXPosition : MonoBehaviour
{

    public GameObject particlePrefab; // Drag-and-drop le prefab dans l'�diteur Unity

    void OnCollisionEnter(Collision collision)
    {
        // Obtient la position de la collision
        Vector3 collisionPosition = collision.contacts[0].point;

        // Obtient la normale de la surface de la collision
        Vector3 collisionNormal = collision.contacts[0].normal * -1;

        // Calcule la rotation � partir de la normale
        Quaternion rotationFromNormal = Quaternion.LookRotation(collisionNormal);

        // Instancie le Particle System � partir du prefab � la position de la collision avec une rotation bas�e sur la normale
        GameObject particleInstance = Instantiate(particlePrefab, collisionPosition, rotationFromNormal);

        // D�truit la particule apr�s un certain temps
        Destroy(particleInstance, particleInstance.GetComponent<ParticleSystem>().main.duration);

        Debug.Log(collisionNormal);
    }
}