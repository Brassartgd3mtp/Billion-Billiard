using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayer : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 vel;
    public float speed;

    public bool isShooted;
    public bool isMoving;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        isShooted = false;
    }

    public void Update()
    {
        vel = rb.velocity;
        speed = vel.magnitude;

        if (speed > 0)
        {
            isMoving = true;
        } else isMoving = false;
    }

    public bool IsShooted
    {
        get { return isShooted; }
        set
        {
            isShooted = true;
            TurnBasedSystem.Instance.Check();
            Debug.Log("oui");
        }
    }
}
