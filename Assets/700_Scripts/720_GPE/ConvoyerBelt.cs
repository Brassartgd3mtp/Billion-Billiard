using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyerBelt : MonoBehaviour
{
    [SerializeField, Space] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    private Vector3 direction;
    public string ConvoyerSound;

    [SerializeField, Range(0, 100), Space] private float speed;

    void Update()
    {
        float directionX = endPoint.transform.position.x - startPoint.transform.position.x;
        float directionZ = endPoint.transform.position.z - startPoint.transform.position.z;

        direction = new Vector3(directionX, 0, directionZ);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rigidbody _colrb))
            _colrb.AddForce(direction * speed * 2);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        AudioManager2.Instance.PlaySDFX(ConvoyerSound);
    }

}