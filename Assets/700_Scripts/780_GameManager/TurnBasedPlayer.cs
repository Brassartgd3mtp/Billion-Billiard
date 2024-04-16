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
    public UI_ShotRemaining UIShotRemaining;

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

        UIShotRemaining.Initialize(nbrOfShots);
    }

    public void Update()
    {
        if (shotRemaining <= 0)
            TurnBasedSystem.OnPlayerPlayed();

        if (PassiveReloadEnabled)
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

        foreach (Animator anim in UIShotRemaining.shotsAnimations)
        {
            anim.SetInteger("ShotsLeft", shotRemaining);
        }
    }
    
    public void RecupBoostReload()
    {
        shotRemaining++;

        SoundReload();
        TurnBasedSystem.ReloadForPlayer();

        UIShotRemaining.PassiveUpdateShots();
    }

    public void PassiveReload()
    {
        if (PassiveReloadEnabled)
        {
            shotRemaining++;

            SoundReload();
            TurnBasedSystem.ReloadForPlayer();

            UIShotRemaining.PassiveUpdateShots();
        }
    }

    private void SoundReload()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(17, audioSource);
    }

    public void ShotCount()
    {
        if (shotRemaining > 0)
        {
            foreach (Animator anim in UIShotRemaining.shotsAnimations)
            {
                if (anim.GetInteger("ShotsLeft") == 0/* && UIShotRemaining.lastIndex == 0*/)
                    /*anim.Play("Shots.TryToShoot", 0);*/
                    Debug.Log($"Shots left : {anim.GetInteger("ShotsLeft")}");
            }

            UIShotRemaining.Shot();

            shotRemaining--;
        }
    }
}
