using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Obstacle;

public class FXSlideIce : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public ParticleSystem IceSlideParticle;


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (myRigidbody.velocity.magnitude == 0)
        {
            if (IceSlideParticle.isPlaying)
            {
                IceSlideParticle.Stop();
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Obstacle _obstacle) &&
             (_obstacle.obstacleType == Obstacle.ObstacleType.Ice || _obstacle.obstacleType == Obstacle.ObstacleType.IceAngle))
        {
            if (myRigidbody.velocity.magnitude > 0.2f)
            {
                if (!IceSlideParticle.isPlaying)
                {
                    IceSlideParticle.Play();
                }
            }
            else
            {
                if (IceSlideParticle.isPlaying)
                {
                    IceSlideParticle.Stop();
                }
            }
        }
    }
}
