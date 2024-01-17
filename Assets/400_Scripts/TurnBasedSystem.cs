using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TurnState { START, PLAYERTURN, ENEMYTURN }

public static class TurnBasedSystem
{
    public static void InitializeTurns()
    {

    }

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

    public static void CheckPlayerTurn()
    {
        foreach (GameObject player in players) 
        {
            player.TryGetComponent(out TurnBasedPlayer turnBasedPlayer);
            if (turnBasedPlayer.isPlayed)
            {
                PlayerTurnEnd();
            }
        }
        // Check si l'ensemble des personnage dans la list de player ont joué et que leur speed est à 0
        // Si tout est ok, joue : PlayerTurnEnd
    }

    public static void PlayerTurnEnd() 
    {

        ResetPlayerTurn();
        state = TurnState.ENEMYTURN;
        OnStateValueChanged(state);
    }

    public static void ResetPlayerTurn() 
    {
        // Reset le isShooted du PlayerController pour le prochain tour
    }
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










    //public void AllowPlayerTurn()
    //{
    //    // Player can use his controls
    //    // Check when player use his actions
    //    // Check when the player's velocity is null
    //    // If all players in list has played, turn state for Enemies
    //}
    //
    //public void Check()
    //{
    //    StartCoroutine(CheckPlayerMovement());
    //}
    //public IEnumerator CheckPlayerMovement()
    //{
    //
    //    foreach (GameObject _enemy in Enemies)
    //    {
    //        _enemy.TryGetComponent(out TurnBasedPlayer turnBasedPlayer);
    //        if (_enemy.speed > 0)
    //        {
    //            break;
    //        } else
    //        {
    //            yield return new WaitForSeconds(1f);
    //            Debug.Log("Fin tour joueur");
    //            state = TurnState.ENEMYTURN;
    //        }
    //    }
    //
    //
    //
    //
    //
    //    yield return new WaitForSeconds(0.1f);
    //    player.TryGetComponent(out TurnBasedPlayer turnBasedPlayer);
    //    if (turnBasedPlayer.isMoving)
    //    {
    //        Debug.Log("là");
    //        StartCoroutine(CheckPlayerMovement());
    //    } else
    //    {
    //
    //        yield return new WaitForSeconds(1f);
    //        Debug.Log("Fin tour joueur");
    //        state = TurnState.ENEMYTURN;
    //
    //        yield break;
    //    }
    //
    //}
    //
    //public void AllowEnemyTurn()
    //{
    //    //Check all enemies who need to play
    //    //Play there actions
    //    //Check there movement 
    //    //If all enemies in list has played, turn state for Player 
    //}
    //
    //public void EndPlayerTurn()
    //{
    //    state = TurnState.ENEMYTURN;
    //}
}
