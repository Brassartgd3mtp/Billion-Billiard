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
    private EndLevelProgressBar progressBarScript;

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

    [Header("Golden Star GameObjects")]

    [SerializeField] private GameObject GO_GoldenStar1;
    [SerializeField] private GameObject GO_GoldenStar2;
    [SerializeField] private GameObject GO_GoldenStar3;
    [SerializeField] Animator anim1;
    [SerializeField] Animator anim2;
    [SerializeField] Animator anim3;

    [Header("Texts")]

    [SerializeField] private TextMeshProUGUI TXT_score;
    [SerializeField] private TextMeshProUGUI TXT_timer;
    [SerializeField] private TextMeshProUGUI TXT_highscore;
    [SerializeField] private TextMeshProUGUI TXT_pb;

    private void OnEnable()
    {
        GO_GoldenStar1.SetActive(false);
        GO_GoldenStar2.SetActive(false);
        GO_GoldenStar3.SetActive(false);

        
        calculationsScript = FindAnyObjectByType<ScoringCalculations>();
        timerScript = FindAnyObjectByType<LevelTimer>();
        starRatingScript = FindAnyObjectByType<StarRating>();
        progressBarScript = FindAnyObjectByType<EndLevelProgressBar>();

        timerScript.TimeStarted = false; //stop the timer
        starRatingScript.HasWon = true; 
        starRatingScript.numberOfStars++; //to give the player the first star
        starRatingScript.CalculateStarRating();

        progressBarScript.BarCanMove = true; 

        
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
        
        StartCoroutine(AnimationsCoroutine()); // display the stars in the current score

        //display the star from the highscore
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

    private IEnumerator AnimationsCoroutine() //display the star and use waitforseconds to play the animations one by one
    {
        switch (starRatingScript.numberOfStars)
        {
            case 0:
                break;
            case 1:
                GO_GoldenStar1.SetActive(true);
                anim1.SetBool("hasStar", true);
                break;
            case 2:
                GO_GoldenStar1.SetActive(true);
                anim1.SetBool("hasStar", true);
                yield return new WaitForSeconds(1.5f);

                GO_GoldenStar2.SetActive(true);
                anim2.SetBool("hasStar", true);
                break;
            case 3:
                GO_GoldenStar1.SetActive(true);
                anim1.SetBool("hasStar", true);
                yield return new WaitForSeconds(1.5f);

                GO_GoldenStar2.SetActive(true);
                anim2.SetBool("hasStar", true);
                yield return new WaitForSeconds(1.5f);

                GO_GoldenStar3.SetActive(true);
                anim3.SetBool("hasStar", true);
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
