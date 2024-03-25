using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimProps : MonoBehaviour
{
    public LayerMask LayerMask;
    public Animation MyAnimation;

    private void Start()
    {
        MyAnimation = GetComponent<Animation>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask == (LayerMask | (1 << collision.gameObject.layer)))
            MyAnimation.Play();
    }
}