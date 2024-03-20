using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimBumper : MonoBehaviour
{
    public Animation MyAnimation;
    private void Start()
    {
        MyAnimation = GetComponent<Animation>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        MyAnimation.Play();
    }
}
