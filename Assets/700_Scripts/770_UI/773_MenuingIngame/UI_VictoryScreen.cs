using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VictoryScreen : MonoBehaviour
{
    [Header("References")]

    private StarRating starRatingScript;
    private LevelTimer timerScript;

    [Header("Objects")]

    [SerializeField] private GameObject firstButton;

    private void OnEnable()
    {
        timerScript = FindAnyObjectByType<LevelTimer>();
        timerScript.TimeStarted = false;
        starRatingScript = FindAnyObjectByType<StarRating>();
        starRatingScript.HasWon = true;
        starRatingScript.numberOfStars++;
        starRatingScript.CalculateStarRating();
        

        if (SwapControls.state == CurrentState.Gamepad)
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject);

            if (EventSystem.current.currentSelectedGameObject != firstButton)
                EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (SwapControls.state == CurrentState.MouseKeyboard)
    //    {
    //        Debug.Log(EventSystem.current.currentSelectedGameObject);
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.Confined;
    //        Cursor.visible = true;
    //    }
    //}
}
