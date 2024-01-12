using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employees : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8) //Layer Interactable
        {
            if(collision.gameObject.TryGetComponent(out Interrupteur interrupteur))
            {
                interrupteur.ContactPNJ = true;
            }

            if (collision.gameObject.TryGetComponent(out HoleForPNJ holeForPNJ))
            {
                Destroy(gameObject);
            }
        }
    }
}
