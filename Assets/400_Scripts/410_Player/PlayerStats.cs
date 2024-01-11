using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int moneyCount;
    public static PlayerStats Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

}
