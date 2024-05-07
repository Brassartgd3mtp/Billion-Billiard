using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlayFX : MonoBehaviour
{
    public ParticleSystem MyParticleSystem;
    public LayerMask PlayerLayer;

    private void Start()
    {
        MyParticleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerLayer == (PlayerLayer | (1 << other.gameObject.layer)))
        {
            // Si oui, jouer le ParticleSystem
            if (MyParticleSystem != null)
            {
                MyParticleSystem.Play();
            }
        }
    }
}
