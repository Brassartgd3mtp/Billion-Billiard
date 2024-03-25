using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayer : MonoBehaviour
{
    [Header("Shots")]
    public int shotRemaining;
    public int nbrOfShots;

    [Header("Reload")]
    public bool PassiveReloadEnabled;
    public float PassiveReloadCooldown = 3;
    private float ReloadCooldown;

    [Header("References")]
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
        if (ReloadCooldown > 0 && PassiveReloadEnabled)
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
        if (PassiveReloadEnabled)
        {
            shotRemaining++;
            uI_ShotRemaining.UpdateUI(shotRemaining);
            ParticleShotRemaining.PassiveUpdateShots();
            TurnBasedSystem.ReloadForPlayer();
        }
    }

    public void ShotCount()
    {
        shotRemaining--;
        ParticleShotRemaining.Death();
        uI_ShotRemaining.UpdateUI(shotRemaining);
        
        if (shotRemaining <= 0)
            TurnBasedSystem.OnPlayerPlayed();
    }
}
