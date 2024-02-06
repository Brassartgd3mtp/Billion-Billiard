using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TurnState { START, PLAYERTURN, ENEMYTURN }

public static class TurnBasedSystem
{

    //public static TurnBasedSystem Instance;

    //public List<GameObject> Players = new List<GameObject>();
    public static List<GameObject> players = new List<GameObject>();

    public static List<GameObject> enemies = new List<GameObject>();

    public static TurnState state;

    public static event Action OnEnablePlayerInput;
    public static event Action OnDisablePlayerInput;

    private static void OnStateValueChanged(TurnState _state)
    {
        state = _state;
        switch (_state)
        {
            case TurnState.START:
                OnStateValueChanged(TurnState.PLAYERTURN);
                break;
    
            case TurnState.PLAYERTURN:
                OnEnablePlayerInput?.Invoke();
                Debug.Log("Player Turn");
                break;
    
            case TurnState.ENEMYTURN:
                CheckEnemyList();
                Debug.Log("Enemies Turn");
                break;
        }
    }


    #region PlayerTurnManager
    public static void OnPlayerTurnStart()
    {
        Debug.Log("PlayerTurn");
        state = TurnState.PLAYERTURN;
        OnStateValueChanged(state);
    }

    public static void OnPlayerPlayed() 
    {
        OnDisablePlayerInput?.Invoke();
    }
    
    public static void ReloadForPlayer()
    {
        OnEnablePlayerInput?.Invoke();
    }
        // Check si l'ensemble des personnage dans la list de player ont joué et que leur speed est à 0
        // Si tout est ok, joue : PlayerTurnEnd


    public static void PlayerTurnEnd() 
    {

        //ResetPlayerTurn();
        state = TurnState.ENEMYTURN;
        OnStateValueChanged(state);
    }

    //public static void ResetPlayerTurn() 
    //{
    //    // Reset le isShooted du PlayerController pour le prochain tour
    //}
    #endregion


    #region EnemyTurnManager
    private static void CheckEnemyList()
    {
        if (enemies.Count <=  0) 
        {
            EndEnemyTurn();
        }
        // Vérifie s'il y a des ennemies dans la list d'ennemis devant jour
        // Si, List = null : fin du tour
        // Si, List != null : Faire jouer l'ennemi
    }

    private static void OnPlayEnemyTurn()
    {
        //Fais jouer les ennemis de la list
    }

    private static void CheckEnemyTurn()
    {
        // Se joue quand l'enemi a fini son action (se joue lorsque la vélocité de l'ensemble des ennemis de la liste est à 0 et qu'ils ont joués)
    }

    private static void EndEnemyTurn()
    {
        state = TurnState.PLAYERTURN;
        OnStateValueChanged(state);
    }
    #endregion


}
