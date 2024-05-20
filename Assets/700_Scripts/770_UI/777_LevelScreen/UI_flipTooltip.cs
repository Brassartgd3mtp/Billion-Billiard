using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_flipTooltip : MonoBehaviour
{


    [SerializeField] private GameObject tooltip;

    private void Awake()
    {
        ShowTooltip();
    }

    public void ShowTooltip()
    {
        if (!Globals.HasClickedOnCard)
        {
            tooltip.SetActive(true);
        }
        else
            tooltip.SetActive(false);
    }
}
