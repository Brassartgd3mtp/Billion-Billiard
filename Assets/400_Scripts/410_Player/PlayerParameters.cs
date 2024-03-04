using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    private Rigidbody rb;

    public float playerMass;
    public AnimationCurve dragCurve;

    public float timerOfCurve;
    public float speedForStartingDrag;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        rb.mass = playerMass;
    }

    public void Update()
    {
        if (TurnBasedPlayer.Instance.speed <= speedForStartingDrag)
        {
            rb.drag = dragCurve.Evaluate(timerOfCurve);
        }
    }
}
