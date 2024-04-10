using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public LevelType levelType = LevelType.HoleInOne;
    private Scene currentScene;
    public float HIOCooldown = 1.5f;

    private void Start()
    {
        if (levelType == LevelType.HoleInOne)
        {
            currentScene = SceneManager.GetActiveScene();

            TurnBasedPlayer.Instance.shotRemaining = 1;
            TurnBasedPlayer.Instance.nbrOfShots = 1;

            TurnBasedPlayer.Instance.PassiveReloadEnabled = false;
            TurnBasedPlayer.Instance.PassiveReloadCooldown = 1;

            StartCoroutine(HoleInOne());
        }
    }

    //private void Update()
    //{
    //    if (levelType == LevelType.HoleInOne)
    //    {
    //        if (PlayerController.rb.velocity.magnitude <= 0
    //        && TurnBasedPlayer.Instance.shotRemaining == 0)
    //        {
    //            SceneManager.LoadScene(currentScene.buildIndex);
    //        }
    //    }
    //}

    IEnumerator HoleInOne()
    {
        if (PlayerController.rb.velocity.magnitude > 0)
        {
            while (true)
            {
                if (PlayerController.rb.velocity.magnitude <= 0.1f
                && TurnBasedPlayer.Instance.shotRemaining == 0)
                {
                    yield return new WaitForSeconds(HIOCooldown);
                    SceneManager.LoadScene(currentScene.buildIndex);
                    yield break;
                }
                else
                    yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(.1f);
            StartCoroutine(HoleInOne());
            yield break;
        }
    }
}

public enum LevelType
{
    HoleInOne,
    Exploration
}