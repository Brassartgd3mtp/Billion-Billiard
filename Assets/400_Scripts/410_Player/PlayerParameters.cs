using Assets.SimpleLocalization.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    private PlayerController playerController;

    private Rigidbody rb;
    public float playerMass;

    public AnimationCurve dragCurve;
    public float timerOfCurve;

    public float speedForStartingDrag;
    private bool startDrag;
    private float timerForDrag;

    private Vector3 vel;
    [SerializeField] private float speed;
    private bool canDrag;


    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    public void Start()
    {
        rb.mass = playerMass;
    }

    public void Update()
    {
        vel = rb.velocity;
        speed = vel.magnitude;

        if (playerController.isShooted)
        {
            timerOfCurve = 0;
            timerForDrag -= Time.deltaTime;
            canDrag = false;

            if (timerForDrag <= 0)
            {
                playerController.isShooted = false;
                timerForDrag = 0.5f;
                canDrag= true;
            }
        }

        if (speed < speedForStartingDrag && canDrag)
        {
            timerOfCurve += Time.deltaTime;
            rb.drag = dragCurve.Evaluate(timerOfCurve);
        }
        else rb.drag = 1;
    }
}
