using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayer : MonoBehaviour
{
    public float speed;

    public bool hasStopped { get; private set; }
    public bool reShooted;

    public bool dragChecker;

    public int shotRemaining;
    public int nbrOfShots;

    public float PassiveReloadCooldown = 3;

    public float ReloadCooldown;

    public PlayerController playerController;
    public UI_ShotRemaining uI_ShotRemaining;
    public ParticleShotRemaining ParticleShotRemaining;

    public static TurnBasedPlayer Instance;

    public void Start()
    {
        ReloadCooldown = PassiveReloadCooldown;

        if (Instance == null)
        {
            Instance = this;
        }

        TurnBasedSystem.players.Add(gameObject);

        shotRemaining = nbrOfShots;
        playerController = GetComponent<PlayerController>();
        uI_ShotRemaining.UpdateUI(shotRemaining);
        ParticleShotRemaining.Initialize(nbrOfShots);
    }

    public void Update()
    {
        

        if (ReloadCooldown > 0)
        {
            if (shotRemaining != nbrOfShots)
                ReloadCooldown -= Time.deltaTime;
        }
        else
        {
            PassiveReload();
            ReloadCooldown = PassiveReloadCooldown;
        }
    }
    
    public void RecupBoostReload()
    {
        shotRemaining += 1;
        uI_ShotRemaining.UpdateUI(shotRemaining);
        TurnBasedSystem.ReloadForPlayer();
        ParticleShotRemaining.PassiveUpdateShots();
    }

    public void PassiveReload()
    {
        shotRemaining++;
        uI_ShotRemaining.UpdateUI(shotRemaining);
        ParticleShotRemaining.PassiveUpdateShots();
        TurnBasedSystem.ReloadForPlayer();
    }

    public void ShotCount()
    {
        hasStopped = false;
        shotRemaining--;
        ParticleShotRemaining.Death();
        uI_ShotRemaining.UpdateUI(shotRemaining);
        
        if (shotRemaining <= 0)
            TurnBasedSystem.OnPlayerPlayed();
    }
}
