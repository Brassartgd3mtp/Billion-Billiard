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
    [SerializeField] private ParticleSystem myParticleSystem;

    public string Ball_DestructibleDoor_Hit;


    public bool Open
    {
        get { return open; }
        set
        {
            open = value;
            //myAnimator.SetBool("CollisionPlayer", open);
            EndAnimation();
        }
    }

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((Player.value & 1 << collision.gameObject.layer) > 0)
        {
            AudioManager2.Instance.PlaySDFX(Ball_DestructibleDoor_Hit);
            StartCoroutine(OpenWithDelay());
        }
    }

    IEnumerator OpenWithDelay()
    {
        myParticleSystem.Play();
        yield return new WaitForSeconds(DelayOpen);
        Open = true;
    }
}
