using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employees : MonoBehaviour
{
    public bool inHole;

    private Vector3 startPos;
    private GameObject interuptor;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8) //Layer Interactable
        {
            if(collision.gameObject.TryGetComponent(out Interrupteur interrupteur))
            {
                interuptor = collision.gameObject;
                interrupteur.ContactPNJ = true;
                StartCoroutine(MoveInInteruptor());            }

            if (collision.gameObject.TryGetComponent(out HoleForPNJ holeForPNJ))
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator MoveInInteruptor() 
    {
        Debug.Log("move");
        float elapsedTime = 0;
        float waitTime = 1f;
        startPos = transform.position;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(startPos, interuptor.transform.position, (elapsedTime/waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }
}