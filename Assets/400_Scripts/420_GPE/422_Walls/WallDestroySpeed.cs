using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroySpeed : MonoBehaviour
{
    [SerializeField] float destroyValue;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            if (PlayerController.rb.velocity.magnitude > destroyValue)
            {
                Destroy(gameObject);
            }
        }
    }
}
