using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interrupteur : MonoBehaviour
{
    private bool contactPNJ = false;
    private new Collider collider;
   
    public bool ContactPNJ
    {
        get { return contactPNJ; }
        set
        {
            contactPNJ = value;
            collider.isTrigger = true;
        }
    }

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
}