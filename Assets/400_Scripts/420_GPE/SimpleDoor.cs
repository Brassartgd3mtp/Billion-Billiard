using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    private bool open = false;
    public float DelayOpen = 0.5f;
    public LayerMask Player;

    private Animator myAnimator;

    public bool Open
    {
        get { return open; }
        set
        {
            open = value;
            myAnimator.SetBool("CollisionPlayer", open);
        }
    }

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((Player.value & 1 << collision.gameObject.layer) > 0)
        {
            StartCoroutine(OpenWithDelay());
        }
    }

    IEnumerator OpenWithDelay()
    {
        yield return new WaitForSeconds(DelayOpen);
        Open = true;
    }
}
