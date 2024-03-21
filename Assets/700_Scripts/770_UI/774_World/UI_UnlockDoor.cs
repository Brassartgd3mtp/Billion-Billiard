using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UnlockDoor : MonoBehaviour
{
    private Transform Origin;
    public Transform LockedDoor;

    public Transform IconPadlockUnlock;

    private void Start()
    {
        Origin = Camera.main.transform;        
    }


    void Update()
    {
        if (Origin == null || LockedDoor == null)
        {
            Debug.LogError("Veuillez assigner l'origine et la cible dans l'inspecteur.");
            return;
        }

        // Calcul de la direction de la cible par rapport à l'origine
        Vector3 direction = (LockedDoor.position - Origin.position);
        direction.y = 0;

        float clampedX = Mathf.Clamp(direction.x / LockedDoor.position.x * -Screen.width / 4, -Screen.width / 2 * .9f, Screen.width / 2 * .9f);
        float clampedZ = Mathf.Clamp(direction.z / LockedDoor.position.z * -Screen.height / 2.5f, -Screen.height / 2 * .9f, Screen.height / 2 * .9f);

        IconPadlockUnlock.transform.localPosition = new Vector3(clampedX,clampedZ, 0);
    }
}