using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ShotRemaining : MonoBehaviour
{
    public TextMeshProUGUI TEXT_ShotRemainingCount;


    public void UpdateUI(int _shotsRemaining)
    {
        TEXT_ShotRemainingCount.text = $"Shot Remaining : {_shotsRemaining}";
    }
}
