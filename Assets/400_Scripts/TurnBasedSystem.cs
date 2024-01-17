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
    private static TurnBasedPlayer player;

    private static List<GameObject> Enemies = new List<GameObject>();

    public static TurnState state;

    public static void OnPlayerTurnStart()
    {
        state = TurnState.PLAYERTURN;
        OnStateValueChanged(state);
    }

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
                Debug.Log("Enemies Turn");
                break;
        }
    }

    public static event Action OnEnablePlayerInput;

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
