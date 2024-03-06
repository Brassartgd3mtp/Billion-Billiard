using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCParameters : MonoBehaviour
{
    private Rigidbody rb;

    [Tooltip("Masse du player, la masse du Rigidbody se set au start à partir de cette valeur")]
    public float NPCMass;

    [Tooltip("Courbe de la valeur de Drag sur le temps. Elle débute lorsque la valeur de Speed est inférieure à speedForStartingDrag")]
    public AnimationCurve dragCurve;
    public float timerOfCurve;

    [Tooltip("Valeur de la Speed à partir de laquelle la courbe de Drag débute")]
    public float speedForStartingDrag;
    private bool startDrag;
    private float timerForDrag;


    private Vector3 vel;
    [SerializeField] private float speed;
    private bool canDrag;

    private bool isShooted;

    public float bounciness;
    public PhysicMaterial physicMaterial;


    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        physicMaterial = rb.GetComponent<PhysicMaterial>();
        bounciness = physicMaterial.bounciness;
    }

    public void Start()
    {
        rb.mass = NPCMass;
    }

    public void Update()
    {
        vel = rb.velocity;
        speed = vel.magnitude;

        if (isShooted)
        {
            timerOfCurve = 0;
            timerForDrag -= Time.deltaTime;
            canDrag = false;

            if (timerForDrag <= 0)
            {
                isShooted = false;
                timerForDrag = 0.5f;
                canDrag = true;
            }
        }

        if (speed < speedForStartingDrag && canDrag)
        {
            timerOfCurve += Time.deltaTime;
            rb.drag = dragCurve.Evaluate(timerOfCurve);
        }
        else rb.drag = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerController playerController)) 
        {
            isShooted = true;
        }
    }
}
