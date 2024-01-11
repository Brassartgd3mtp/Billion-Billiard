using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    public int UIMoneyCount;
    public PlayerStats PlayerStats;
    public TextMeshProUGUI TEXT_Money_Count;

    public void Start()
    {
        UIMoneyCount = 0;
        UpdateStats();
    }

    public void UpdateStats()
    {
        TEXT_Money_Count.text = $"Money {UIMoneyCount}";
        UIMoneyCount = PlayerStats.moneyCount;
    }
}
