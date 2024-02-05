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
        //Int�grer l'animation du convoyer en prenant compte de la speed
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Transform _coltrans))
        {
            _coltrans.position += direction * speed * Time.deltaTime;
        }
    }
}
