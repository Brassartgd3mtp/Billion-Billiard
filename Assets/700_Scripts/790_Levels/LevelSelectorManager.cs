using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;

    [SerializeField] private Button LeftArrow, RightArrow;
    [SerializeField] private Button BTN_Play;

    [SerializeField] private List<GameObject> Panels;
    [SerializeField] private List<SO_Level> SO_Levels;
    [SerializeField] private GameObject ActualPanel;
    private static int PanelIndex = 0;

    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [SerializeField] private float scrollingSpeed;

    [SerializeField] private GameObject Content;


    [Header("Background")]
 
    [SerializeField] private Image backgroundImage;
 //   [SerializeField] private bool updatingImage = false;
 //   private float baseTimer = 0.2f;
  //  private float timer;
  //  [SerializeField] private Image fadingImage;

    public void Start()
    {
        ActualPanel = Panels[PanelIndex];
        StartCoroutine(MovePanel(-PanelIndex));
   //     timer = baseTimer;
    }



    public void NextPanel()
    {
        PanelIndex++;
        ActualPanel = Panels[PanelIndex];
        LeftArrow.enabled = false;
        RightArrow.enabled = false;
        StartCoroutine(MovePanel(-1));
    }

    public void PrevPanel() 
    {
        if (PanelIndex > 0) 
        {
            PanelIndex--;
            ActualPanel = Panels[PanelIndex];
            LeftArrow.enabled = false;
            RightArrow.enabled = false;
            StartCoroutine(MovePanel(1));
        }
    }

  //  private void Update()
  //  {
    //    if(updatingImage) 
     //   {
    //        baseTimer -= Time.deltaTime;
     //       if (baseTimer >= 0)
     //       {
     //           DoFading(baseTimer);
     //       }
     //       else Refade(baseTimer);
    //    }
    //    Debug.Log(fadingImage.color);
  //  }

    public IEnumerator MovePanel(int xValue)
    {
        RectTransform rectTransform = Content.GetComponent<RectTransform>();
        Vector3 actualPos = rectTransform.transform.localPosition;
        Vector3 targetPos = actualPos + new Vector3(xValue * 800,0,0);

        while (rectTransform.localPosition !=  targetPos) 
        {
            rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, targetPos, scrollingSpeed * Time.deltaTime);
            Debug.Log("là");
            yield return null;    
        }

        LeftArrow.enabled = true;
        RightArrow.enabled = true;

        UpdateBackgroundImage();
        CheckIfNextPanelIsLocked();
        yield break;
    }

    public void PlayButton()
    {
        if (ActualPanel.TryGetComponent(out PanelManager panelManager))
        {
            panelManager.SO_Level.LoadLevel();
        }
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CheckIfNextPanelIsLocked()
    {
        Panels[PanelIndex + 1].TryGetComponent(out PanelManager panelManagerNext);

        if (panelManagerNext.SO_Level.LevelData.isLocked)
        {
            Debug.Log("Locked");

            if (SwapControls.state == CurrentState.Gamepad)
                _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
            else
                _eventSystem.SetSelectedGameObject(null);

            RightArrow.gameObject.SetActive(false);
            RightArrow.enabled = false;
            _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
        }
        else if (RightArrow.enabled && !RightArrow.gameObject.activeInHierarchy)
        {
            RightArrow.gameObject.SetActive(true);
            RightArrow.enabled = true;
        }

        if (ActualPanel == Panels[0])
        {
            if (SwapControls.state == CurrentState.Gamepad)
                _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
            else
                _eventSystem.SetSelectedGameObject(null);

            _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
            LeftArrow.gameObject.SetActive(false);
            LeftArrow.enabled = false;
        }
        else if (ActualPanel != Panels[0])
        {
            LeftArrow.gameObject.SetActive(true);
            LeftArrow.enabled = true;
        }
    }

    private void UpdateBackgroundImage() // update the background image in the level selector using the variable BackgroundImage of the current SO
    {
      //  updatingImage = true;
        backgroundImage.sprite = SO_Levels[PanelIndex].BackgroundImage;
        
    }
 //   private void DoFading(float _timer)
  //  {
   //     {
   //         if(_timer > 0)
   //         {
   //             fadingImage.color -= new Color(0.01f, 0.01f, 0.01f, 0f);
   //             _timer -= Time.deltaTime;
    //        }
//
   //     }
 //   }

  //  private void Refade(float _timer)
   // {
    //    {
    //        if(_timer > 0)
    //        {
    //            fadingImage.color += new Color(0.01f, 0.01f, 0.01f, 0f);
    //            _timer -= Time.deltaTime;
     //       }
     //       else updatingImage = false;
   //     }
  //  }
}
