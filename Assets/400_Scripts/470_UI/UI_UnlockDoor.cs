using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UnlockDoor : MonoBehaviour
{
    public Transform Origin;
    public Transform Target;

    public Transform IconPadlockUnlock;
    void Update()
    {
        if (Origin == null || Target == null)
        {
            Debug.LogError("Veuillez assigner l'origine et la cible dans l'inspecteur.");
            return;
        }

        // Calcul de la direction de la cible par rapport à l'origine
        Vector3 direction = (Target.position - Origin.position);

        IconPadlockUnlock.transform.position = new Vector3(direction.x/ Target.position.x*1920, direction.z/ Target.position.z*-1080, 0);

        Debug.Log("Direction de la cible par rapport à l'origine : " + direction);
        //float angle = Vector3.Angle(Origin.forward, direction);
        //Debug.Log("Angle entre l'axe avant de l'origine et la direction de la cible : " + angle);
    }
}