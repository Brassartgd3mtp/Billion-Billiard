using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionBehavior : MonoBehaviour
{
    public PlayerStats playerStats;
    public UI_Stats uI_Stats;

    public Vector3 posBeforeStrike;

    public static PlayerCollisionBehavior Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playerStats = GetComponent<PlayerStats>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (collision.gameObject.TryGetComponent(out MoneyStats moneyStats))
            {
                AddMoney(moneyStats.value);
                uI_Stats.UpdateStats();
                collision.gameObject.TryGetComponent(out LootAnimation lootAnimation);
                lootAnimation.StartAnimation();
            }

            if (collision.gameObject.TryGetComponent(out HoleForPlayer holeForPlayer))
            {
                transform.position = posBeforeStrike;
            }
        }
    }

    public void AddMoney(int money)
    {
        playerStats.moneyCount += money;
    }

}
