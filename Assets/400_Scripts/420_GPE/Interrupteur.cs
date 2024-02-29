using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interrupteur : MonoBehaviour
{
    [SerializeField] private LockedDoor lockedDoor;

    private bool contactPNJ = false;

    public bool ContactPNJ
    {
        get { return contactPNJ; }
        set
        {
            contactPNJ = value;
            lockedDoor.Unlock = value;
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(lockedDoor == null)
            Debug.LogError("Veuillez assigner la porte à l'interrupteur");
        return;
    }
}