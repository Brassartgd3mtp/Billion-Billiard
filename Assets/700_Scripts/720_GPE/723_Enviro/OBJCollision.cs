using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJCollision : MonoBehaviour
{
    public Rigidbody MyRigidbody;
    public float ImpactForce;

    private void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Vector3 _forceDirection = transform.position - collision.transform.position;

            MyRigidbody.AddForce(_forceDirection * ImpactForce, ForceMode.VelocityChange);

            MyRigidbody.transform.Rotate(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180), Space.World);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
