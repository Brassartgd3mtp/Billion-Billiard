using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyerBelt : MonoBehaviour
{
    [SerializeField, Space] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    private Vector3 direction;

    [SerializeField, Range(0, 1), Space] private float speed;
    // Start is called before the first frame update
    void Awake()
    {
        float directionX = endPoint.transform.position.x - startPoint.transform.position.x;
        float directionZ = endPoint.transform.position.z - startPoint.transform.position.z;

        direction = new Vector3(directionX, 0, directionZ);
    }

    void Update()
    {
        //Intégrer l'animation du convoyer en prenant compte de la speed
    }

    float vel;
    Vector3 objectEnterPos;
    Vector3 objectExitPos;
    float timer = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Transform _coltrans))
        {
            objectEnterPos = _coltrans.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Transform _coltrans))
        {
            timer += Time.deltaTime;
            _coltrans.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rigidbody _colrb))
        {
            objectExitPos = _colrb.position;

            vel = Vector3.Distance(objectEnterPos, objectExitPos) / timer;
            Debug.Log(vel);
            
            timer = 0;

            _colrb.AddForce(direction * vel * speed * 2, ForceMode.Force);
        }
    }
}
