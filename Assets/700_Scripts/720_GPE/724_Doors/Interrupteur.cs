using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interrupteur : MonoBehaviour
{
    [SerializeField] private OneWayDoor lockedDoor;

    private bool contactPNJ = false;
    private new Collider collider;
   
    public bool ContactPNJ
    {
        get { return contactPNJ; }
        set
        {
            contactPNJ = value;
            lockedDoor.OpenDoor();
            collider.isTrigger = true;
        }
    }

    private void Start()
    {
        collider = GetComponent<Collider>();

        if (lockedDoor == null)
            Debug.LogError("Veuillez assigner la porte à l'interrupteur");
        return;
    }
}