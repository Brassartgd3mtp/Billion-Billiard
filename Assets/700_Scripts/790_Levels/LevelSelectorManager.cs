using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LevelSelectorManager : MonoBehaviour
{
    //private LevelChargeLoader LevelChargeLoader;

    [SerializeField] private EventSystem _eventSystem;

    [SerializeField] private GameObject LeftArrow, RightArrow;
    [SerializeField] private Button BTN_Play;

    public List<GameObject> Panels;
    public List<SO_Level> SO_Levels;
    [SerializeField] private GameObject ActualPanel;

    //[SerializeField] private Vector3 ContentRectTransform;
    //[SerializeField] private Vector3 savedContentRectTransform;

    public int PanelIndex = 0;

    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [SerializeField] private float scrollingSpeed;

    [SerializeField] private GameObject Content;

    [SerializeField] private bool panelCanMoveLeft, panelCanMoveright;

    [Header("Background")]
 
    [SerializeField] private Image backgroundImage;
    [SerializeField] Animator backgroundImageAnimator;

    LevelChargeLoader levelChargeLoader;

    public void Start()
    {
        InputHandler.MovePanelSelectorEnable(this);

        if (LevelSelectorData.CurrentLevelIndex == 0)
            LevelSelectorData.CurrentLevelIndex = PanelIndex + 2;
        else
            PanelIndex = LevelSelectorData.CurrentLevelIndex - 2;

        ActualPanel = Panels[PanelIndex];

        CheckIfNextPanelIsLocked();
        UpdateBackgroundImage();

        RectTransform rectTransform = Content.GetComponent<RectTransform>();

        if (LevelSelectorData.rectTransformData != Vector3.zero)
        {
            rectTransform.position = LevelSelectorData.rectTransformData;        
        }
        //StartCoroutine(MovePanel(-PanelIndex));

        levelChargeLoader = GetComponent<LevelChargeLoader>();
    }

    public void NextPanel()
    {
        if (panelCanMoveright)
        {
            PanelIndex++;
            ActualPanel = Panels[PanelIndex];
            LevelSelectorData.CurrentLevelIndex = PanelIndex + 2;
            StartCoroutine(MovePanel(-1));
            backgroundImageAnimator.SetTrigger("MakeTransition");
            GoRightSound();
        }
    }
    public void NextPanel(InputAction.CallbackContext context)
    {
        NextPanel();
    }

    public void PrevPanel()
    {
        if (PanelIndex > 0 && panelCanMoveLeft)
        {
            PanelIndex--;
            ActualPanel = Panels[PanelIndex];
            LevelSelectorData.CurrentLevelIndex = PanelIndex + 2;
            StartCoroutine(MovePanel(1));
            backgroundImageAnimator.SetTrigger("MakeTransition");
            GoLeftSound();
        }
    }
    public void PrevPanel(InputAction.CallbackContext context) 
    {
        PrevPanel();
    }

    public IEnumerator MovePanel(int xValue)
    {
        RectTransform rectTransform = Content.GetComponent<RectTransform>();
        Vector3 actualPos = rectTransform.transform.localPosition;
        Vector3 targetPos = actualPos + new Vector3(xValue * 800,0,0);
        panelCanMoveLeft = false;
        panelCanMoveright = false;

        while (rectTransform.localPosition !=  targetPos) 
        {
            rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, targetPos, scrollingSpeed * Time.deltaTime);
            yield return null;    
        }
        
        //LeftArrow.enabled = true;
        //RightArrow.enabled = true;

        UpdateBackgroundImage();
        CheckIfNextPanelIsLocked();
        if (SwapControls.state == CurrentState.MouseKeyboard)
        {
            _eventSystem.SetSelectedGameObject(null);
        }
        yield break;
    }

    public void PlayButton()
    {
        if (ActualPanel.TryGetComponent(out PanelManager panelManager))
        {
            RectTransform rectTransform = Content.GetComponent<RectTransform>();
            Vector3 actualPos = rectTransform.transform.localPosition;

            LevelSelectorData.rectTransformData = rectTransform.position;

            //panelManager.SO_Level.LoadLevel();
            levelChargeLoader.LoadLevel(SO_Levels[PanelIndex].LevelID);
        }
    }
 

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CheckIfNextPanelIsLocked()
    {
        if (PanelIndex >= Panels.Count - 1) 
        {
            if (SwapControls.state == CurrentState.Gamepad)
                _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
            else
                _eventSystem.SetSelectedGameObject(null);

            RightArrow.gameObject.SetActive(false);
            //RightArrow.enabled = false;
            _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
            panelCanMoveLeft = true;
            LeftArrow.gameObject.SetActive(true);
            return;
        }

        Panels[PanelIndex + 1].TryGetComponent(out PanelManager panelManagerNext);

            if (panelManagerNext.SO_Level.LevelData.isLocked)
            {
                if (SwapControls.state == CurrentState.Gamepad)
                    _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
                else
                    _eventSystem.SetSelectedGameObject(null);

                RightArrow.gameObject.SetActive(false);
                //RightArrow.enabled = false;
                _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
            }
            else //if (!RightArrow.enabled) //&& !RightArrow.gameObject.activeInHierarchy)
            {
                panelCanMoveright = true;
                RightArrow.gameObject.SetActive(true);
            }

            if (ActualPanel == Panels[0])
            {
                panelCanMoveright = true;
                if (SwapControls.state == CurrentState.Gamepad)
                    _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
                else
                    _eventSystem.SetSelectedGameObject(null); 

                _eventSystem.SetSelectedGameObject(BTN_Play.gameObject);
                LeftArrow.gameObject.SetActive(false);
                //LeftArrow.enabled = false;
            }
            else
            {
                panelCanMoveLeft = true;
                LeftArrow.gameObject.SetActive(true);
            }
    }

    private void UpdateBackgroundImage() // update the background image in the level selector using the variable BackgroundImage of the current SO
    {
        ActualPanel.TryGetComponent(out PanelManager panelManager);
        backgroundImage.sprite = panelManager.SO_Level.BackgroundImage;
    }

    private void OnDisable()
    {
        InputHandler.MovePanelSelectorDisable();
    }

    private void GoLeftSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(31, audioSource);
    }

    private void GoRightSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(32, audioSource);
    }
}
