using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayer : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 vel;
    public float speed;

    public bool isMoving;
    public bool isPlayed;
    

    public PlayerController playerController;

    public void Start()
    {
        TurnBasedSystem.players.Add(this.gameObject);

        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        isPlayed = false;
        isMoving = false;
    }

    public void Update()
    {
        vel = rb.velocity;
        speed = vel.magnitude;
 
        if (isMoving == true && playerController.isShooted && isPlayed == false && speed == 0) 
        {
            isPlayed = true;
        }

        if (isPlayed)
        {
            TurnBasedSystem.CheckPlayerTurn();
            Debug.Log("Je check");
            ResetForNextTurn();
        }


        if (speed > 0)
        {
            isMoving = true;
        }


        
    }

    public void ResetForNextTurn()
    {
        playerController.isShooted = false;
        isMoving = false;
        isPlayed = false;
    }

}
