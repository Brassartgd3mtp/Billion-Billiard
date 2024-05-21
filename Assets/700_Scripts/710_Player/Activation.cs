using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Activation : MonoBehaviour
{
    public GameObject ObjectToActivate;
    public bool Activate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
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
}
