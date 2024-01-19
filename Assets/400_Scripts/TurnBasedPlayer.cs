using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 vel;
    private float speed;

    public bool hasStopped { get; private set; }
    private bool isMoving;
    private bool dragChecker;

    public int shotRemaining;
    public int nbrOfShots;
    

    public PlayerController playerController;

    public void Start()
    {
        TurnBasedSystem.players.Add(this.gameObject);

        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        vel = rb.velocity;
        speed = vel.magnitude;

        if (speed > 0) 
        {
            isMoving = true;
        }

        if (speed > 0.3f)
        {
            dragChecker = true;
        }

        if (speed < 0.3f && dragChecker)
        {
            rb.drag = 10;
        }

        if (speed == 0)
        {
            isMoving = false;
            rb.drag = 1;
        }

        if (playerController.isShooted && !hasStopped && speed == 0) 
        {
            dragChecker = false;
            hasStopped = true;

            Invoke("CheckShotRemaining", 0.1f);
        }
    }
    
    public void ShotCount()
    {
        hasStopped = false;
        shotRemaining--;
        
        if (shotRemaining <= 0)
        {
            TurnBasedSystem.OnPlayerPlayed();
        }
    }

    public void CheckShotRemaining()
    {
        Debug.Log("CheckShotRemaining");

        if (isMoving)
        {
            hasStopped = false;
        }

        if (hasStopped && shotRemaining <= 0)
        {
        TurnBasedSystem.PlayerTurnEnd();
        playerController.isShooted = false;
        shotRemaining = nbrOfShots;
        }
    }
}
