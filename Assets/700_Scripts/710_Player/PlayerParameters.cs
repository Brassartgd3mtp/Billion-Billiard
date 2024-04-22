using Assets.SimpleLocalization.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    private PlayerController playerController;

    private Rigidbody rb;

    [Tooltip("Masse du player, la masse du Rigidbody se set au start à partir de cette valeur")]
    public float playerMass;

    [Tooltip("Courbe de la valeur de Drag sur le temps. Elle débute lorsque la valeur de Speed est inférieure à speedForStartingDrag")]
    public AnimationCurve dragCurve;
    public float timerOfCurve;

    [Tooltip("Valeur de la Speed à partir de laquelle la courbe de Drag débute")]
    public float speedForStartingDrag;
    private bool startDrag;
    private float timerForDrag;

    private Vector3 vel;
    [SerializeField] public float speed;
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

        if (playerController.isShooted && !playerController.iceLock)
        {
            timerOfCurve = 0;
            timerForDrag -= Time.deltaTime;
            canDrag = false;

            if (timerForDrag <= 0)
            {
                playerController.isShooted = false;
                timerForDrag = 0.5f;
                canDrag = true;
            }
        }

        if (speed < speedForStartingDrag && canDrag)
        {
            timerOfCurve += Time.deltaTime;
            rb.drag = dragCurve.Evaluate(timerOfCurve);
        }
        //else rb.drag = 1;
    }
}
