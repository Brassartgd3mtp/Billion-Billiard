using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [Header("References")]

    private StarRating starRatingScript;
    private LevelTimer timerScript;
    private ScoringCalculations calculationsScript;

    [Header("Objects")]

    [SerializeField] private GameObject firstButton;

    [Header("Medal Images")]

    [SerializeField] private Image IMG_timerMedal;
    [SerializeField] private Image IMG_pbMedal;
    [SerializeField] private List<Sprite> medalImages = new List<Sprite>();

    [Header("Star Images")]

    [SerializeField] private Image IMG_star1;
    [SerializeField] private Image IMG_star2;
    [SerializeField] private Image IMG_star3;

    [SerializeField] private Image IMG_starBest1;
    [SerializeField] private Image IMG_starBest2;
    [SerializeField] private Image IMG_starBest3;

    [SerializeField] private Sprite SPR_goldStarSprite;

    [Header("Texts")]

    [SerializeField] private TextMeshProUGUI TXT_score;
    [SerializeField] private TextMeshProUGUI TXT_timer;
    [SerializeField] private TextMeshProUGUI TXT_highscore;
    [SerializeField] private TextMeshProUGUI TXT_pb;

    private void OnEnable()
    {
        calculationsScript = FindAnyObjectByType<ScoringCalculations>();
        timerScript = FindAnyObjectByType<LevelTimer>();
        timerScript.TimeStarted = false;
        starRatingScript = FindAnyObjectByType<StarRating>();
        starRatingScript.HasWon = true;
        starRatingScript.numberOfStars++;
        starRatingScript.CalculateStarRating();
        
        DisplayScore();
        DisplayMedals();
        DisplayStars();

        if (SwapControls.state == CurrentState.Gamepad)
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject);

            if (EventSystem.current.currentSelectedGameObject != firstButton)
                EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }

    private void DisplayScore()
    {
        Debug.Log(PlayerPrefs.GetString("PB" + SceneManager.GetActiveScene().buildIndex));
        TXT_score.text = "Score : " + calculationsScript.PlayerScore.ToString();
        TXT_timer.text = "Temps : " + string.Format("{0:00}:{1:00}", timerScript.Minutes, timerScript.Seconds);
        TXT_highscore.text = "Highscore : " + PlayerPrefs.GetFloat("hiscore" + SceneManager.GetActiveScene().buildIndex).ToString();
        TXT_pb.text = "Best time : " + PlayerPrefs.GetString("PB" + SceneManager.GetActiveScene().buildIndex);
    }

    private void DisplayMedals()
    {
        IMG_timerMedal.sprite = medalImages[timerScript.MedalValue];
        IMG_pbMedal.sprite = medalImages[PlayerPrefs.GetInt("MedalValue" + SceneManager.GetActiveScene().buildIndex)];
    }

    private void DisplayStars()
    {
        switch(starRatingScript.numberOfStars)
        {
            case 0:
                break;
            case 1:
                IMG_star1.sprite = SPR_goldStarSprite;
                break;
            case 2:
                IMG_star1.sprite = SPR_goldStarSprite;
                IMG_star2.sprite = SPR_goldStarSprite;
                break; 
            case 3:
                IMG_star1.sprite = SPR_goldStarSprite;
                IMG_star2.sprite = SPR_goldStarSprite;
                IMG_star3.sprite = SPR_goldStarSprite;
                break;
        }

        switch (PlayerPrefs.GetFloat("stars" + SceneManager.GetActiveScene().buildIndex))
        {
            case 0:
                break;
            case 1:
                IMG_starBest1.sprite = SPR_goldStarSprite;
                break;
            case 2:
                IMG_starBest1.sprite = SPR_goldStarSprite;
                IMG_starBest2.sprite = SPR_goldStarSprite;
                break;
            case 3:
                IMG_starBest1.sprite = SPR_goldStarSprite;
                IMG_starBest2.sprite = SPR_goldStarSprite;
                IMG_starBest3.sprite = SPR_goldStarSprite;
                break;
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
