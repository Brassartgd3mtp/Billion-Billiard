using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FXSlideIce : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public ParticleSystem IceSlideParticle;

    private float timer;
    private bool isStickToWall;

    AudioSource audioSource;

    private void Start()
    {
        timer = 2;
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = transform.Find("FX_Slide_Ice").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (myRigidbody.velocity.magnitude == 0)
        {
            if (IceSlideParticle.isPlaying)
            {
                IceSlideParticle.Stop();
                audioSource.Stop();
            }
        }

        if (!isStickToWall && timer > 0)
        {
            timer -= Time.deltaTime;

        } else if (!isStickToWall && timer <= 0)
        {
            IceSlideParticle.Stop();
            audioSource.Stop();
            timer = 2f;
        }

        Debug.Log(IceSlideParticle.isPlaying);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Obstacle _obstacle) &&
             (_obstacle.obstacleType == Obstacle.ObstacleType.Ice || _obstacle.obstacleType == Obstacle.ObstacleType.IceAngle))
        {
            if (myRigidbody.velocity.magnitude > 0.2f)
            {
                if (!IceSlideParticle.isPlaying)
                {
                    isStickToWall = true;
                    IceSlideParticle.Play();
                    IceSlideSound();
                    timer = 2f;
                }
                else if (IceSlideParticle.isPlaying && timer > 0)
                {
                    timer = 2f;
                }
            }


        }
    }
    

    private void OnCollisionExit(Collision collision)
    {
        if (IceSlideParticle.isPlaying)
        {
            //IceSlideParticle.Stop();
            isStickToWall = false;
            audioSource.Stop();
        }
    }

    private void IceSlideSound()
    {
        AudioManager.Instance.PlaySoundLoop(35, audioSource);
    }
}
