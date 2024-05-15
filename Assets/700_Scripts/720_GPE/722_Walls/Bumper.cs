using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] float baseForce = 0;
    //public PlayerParameters playerParameters;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = PlayerController.Instance.transform.position - transform.position;

        if (collision.gameObject.layer == 3 && PlayerController.Instance.playerParameters.speed == 0)
        {
            PlayerController.rb.AddForce(direction.normalized * baseForce, ForceMode.Impulse);
        }
    }
}