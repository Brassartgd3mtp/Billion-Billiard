using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TurnState { START, PLAYERTURN, ENEMYTURN }

public class TurnBasedSystem : MonoBehaviour
{
    public static TurnBasedSystem Instance;

    public List<GameObject> Players = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    public TurnState state;



    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {
        OnStateValueChanged(TurnState.START);
    }

    public void OnStateValueChanged(TurnState state)
    {
        switch (state)
        {
            case TurnState.START:
                break;

            case TurnState.PLAYERTURN:
                AllowPlayerTurn();
                Debug.Log("Player Turn");
                break;

            case TurnState.ENEMYTURN:
                Debug.Log("Enemies Turn");
                break;
        }
    }
    
    public void AllowPlayerTurn()
    {
        // Player can use his controls
        // Check when player use his actions
        // Check when the player's velocity is null
        // If all players in list has played, turn state for Enemies
    }

    public void Check()
    {
        StartCoroutine(CheckPlayerMovement());
    }
    public IEnumerator CheckPlayerMovement()
    {
        Debug.Log("Play");
        foreach (GameObject players in Players)
        {
            players.TryGetComponent(out TurnBasedPlayer turnBasedPlayer);
            yield return new WaitUntil(() => turnBasedPlayer.isMoving == false);
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Fin tour joueur");
        state = TurnState.ENEMYTURN;

        yield break ;
    }

    public void AllowEnemyTurn()
    {
        //Check all enemies who need to play
        //Play there actions
        //Check there movement 
        //If all enemies in list has played, turn state for Player 
    }

    public void EndPlayerTurn()
    {
        state = TurnState.ENEMYTURN;
    }
}
