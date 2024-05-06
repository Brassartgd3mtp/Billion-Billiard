using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] float baseForce = 0;
    private void OnCollisionStay(Collision collision)
    {
        Vector3 direction = transform.position - PlayerController.Instance.transform.position;

        if (collision.gameObject.layer == 3 && PlayerController.rb.velocity.magnitude == 0)
        {
            PlayerController.rb.AddForce(direction.normalized * baseForce, ForceMode.Impulse);
        }
    }
}