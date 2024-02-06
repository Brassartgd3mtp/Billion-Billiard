using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 vel;
    public float speed;

    public bool hasStopped { get; private set; }
    private bool isMoving;
    public bool reShooted;

    public bool dragChecker;

    public int shotRemaining;
    public int nbrOfShots;
    

    public PlayerController playerController;
    public UI_ShotRemaining uI_ShotRemaining;

    public static TurnBasedPlayer Instance;

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

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
            reShooted = false;
        }

        if (speed < 0.3f && dragChecker && !reShooted)
        {
            rb.drag = 10;
        }
        else rb.drag = 1;

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
    
    public void RecupBoostReload()
    {
        shotRemaining += 1;
        uI_ShotRemaining.UpdateUI();
        TurnBasedSystem.ReloadForPlayer();
    }

    public void ShotCount()
    {
        hasStopped = false;
        shotRemaining--;
        uI_ShotRemaining.UpdateUI();
        
        if (shotRemaining <= 0)
        {
            TurnBasedSystem.OnPlayerPlayed();
        }
    }

    public void CheckShotRemaining()
    {
        if (isMoving)
        {
            hasStopped = false;
        }

        if (hasStopped && shotRemaining <= 0)
        {
            TurnBasedSystem.PlayerTurnEnd();
            playerController.isShooted = false;
            shotRemaining = nbrOfShots;
            uI_ShotRemaining.UpdateUI();
        }
    }
}
