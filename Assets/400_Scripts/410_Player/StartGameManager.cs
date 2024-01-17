using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    private void Awake()
    {
        TurnBasedSystem.InitializeTurns();
    }

}
