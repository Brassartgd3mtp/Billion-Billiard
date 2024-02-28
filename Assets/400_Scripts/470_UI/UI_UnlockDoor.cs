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
        direction.y = 0;

        float clampedX = Mathf.Clamp(direction.x / Target.position.x * -Screen.width / 4, -Screen.width / 2 * .9f, Screen.width / 2 * .9f);
        float clampedZ = Mathf.Clamp(direction.z / Target.position.z * -Screen.height / 2, -Screen.height / 2 * .9f, Screen.height / 2 * .9f);

        IconPadlockUnlock.transform.localPosition = new Vector3(clampedX,clampedZ, 0);
    }
}