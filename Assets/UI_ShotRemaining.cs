using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ShotRemaining : MonoBehaviour
{
    public int UIShotRemaining;
    public TurnBasedPlayer turnBasedPlayer;
    public TextMeshProUGUI TEXT_ShotRemainingCount;

    public void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIShotRemaining = turnBasedPlayer.shotRemaining;
        TEXT_ShotRemainingCount.text = $"Shot Remaining : {UIShotRemaining}";
    }
}
