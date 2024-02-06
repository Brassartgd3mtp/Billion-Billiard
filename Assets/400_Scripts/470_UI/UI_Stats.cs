using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    public int UIMoneyCount;
    public PlayerStats playerStats;
    public TextMeshProUGUI TEXT_Money_Count;

    public static UI_Stats Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {
        playerStats = PlayerStats.Instance;
        UIMoneyCount = 0;
        UpdateStats();
    }

    public void UpdateStats()
    {
        UIMoneyCount = playerStats.moneyCount;
        TEXT_Money_Count.text = $"Money {UIMoneyCount}";
    }
}
