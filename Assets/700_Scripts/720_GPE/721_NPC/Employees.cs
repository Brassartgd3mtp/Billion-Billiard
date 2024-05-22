using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employees : MonoBehaviour
{
    public bool inHole;

    private Vector3 startPos;
    private GameObject interuptor;
    private Rigidbody rb;

    [SerializeField]private Animator myAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        if (rb.velocity.magnitude > .5f)
        {
            myAnimator.SetBool("Employe_Roll", true);
            rb.freezeRotation = false;
        }
        else
        {
            myAnimator.SetBool("Employe_Roll", false);
            rb.freezeRotation = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 8) //Layer Interactable
        {
            if(collision.gameObject.TryGetComponent(out Interrupteur interrupteur))
            {
                interuptor = collision.gameObject;
                interrupteur.ContactPNJ = true;
                StartCoroutine(MoveInInteruptor());
                myAnimator.SetBool("In_Hole", true);
            }

            if (collision.gameObject.TryGetComponent(out HoleForPNJ holeForPNJ))
            {
                EmployeeFall();
                Destroy(gameObject);
            }
        }
    }

    private void EmployeeFall()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(11, audioSource);
    }

    private IEnumerator MoveInInteruptor() 
    {
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