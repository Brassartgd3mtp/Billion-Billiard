using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    public GameObject ObjectToActivate;
    public bool Activate;

    private void OnTriggerEnter(Collider other)
    {
        if (Activate)
        {
            ObjectToActivate.gameObject.SetActive(true);
        }
        else
        {
            ObjectToActivate.gameObject.SetActive(false);
        }
        
    }
}
