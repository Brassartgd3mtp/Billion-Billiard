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
    [SerializeField] private EventSystem _eventSystem;

    [SerializeField] private GameObject LeftArrow, RightArrow;
    [SerializeField] private Button BTN_Play;

    [SerializeField] public List<GameObject> Panels;
    [SerializeField] private List<SO_Level> SO_Levels;
    [SerializeField] private GameObject ActualPanel;
    private static int PanelIndex = 0;

    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [SerializeField] private float scrollingSpeed;

    [SerializeField] private GameObject Content;

    [SerializeField] private bool panelCanMoveLeft, panelCanMoveright;

    public GameObject XboxGamepad;
    public GameObject BackButton;


    [Header("Background")]
 
    [SerializeField] private Image backgroundImage;
    [SerializeField] Animator backgroundImageAnimator;

    public void Awake()
    {

    }
    public void Start()
    {
        InputHandler.MovePanelSelectorEnable(this);
        ActualPanel = Panels[PanelIndex];
        StartCoroutine(MovePanel(-PanelIndex));
    }

    private void Update()
    {
        if (SwapControls.state == CurrentState.Gamepad)
        {
            XboxGamepad.gameObject.SetActive(true);
            BackButton.gameObject.SetActive(false);
        }
        else
        {
            XboxGamepad.gameObject.SetActive(false);
            BackButton.gameObject.SetActive(true);
        }
    }

    public void NextPanel(InputAction.CallbackContext context)
    {
        if (panelCanMoveright)
        {
            PanelIndex++;
            ActualPanel = Panels[PanelIndex];
            //LeftArrow.enabled = false;
            //RightArrow.enabled = false;
            StartCoroutine(MovePanel(-1));
            backgroundImageAnimator.SetTrigger("MakeTransition");
        }
    }

    public void PrevPanel(InputAction.CallbackContext context) 
    {
        if (PanelIndex > 0 && panelCanMoveLeft) 
        {
            PanelIndex--;
            ActualPanel = Panels[PanelIndex];
            //LeftArrow.enabled = false;
            //RightArrow.enabled = false;
            StartCoroutine(MovePanel(1));
            backgroundImageAnimator.SetTrigger("MakeTransition");
        }
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
            //RightArrow.enabled = true;
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
        else if (ActualPanel != Panels[0])
        {
            panelCanMoveLeft = true;
            LeftArrow.gameObject.SetActive(true);
            //LeftArrow.enabled = true;
        }
    }

    private void UpdateBackgroundImage() // update the background image in the level selector using the variable BackgroundImage of the current SO
    {
        backgroundImage.sprite = SO_Levels[PanelIndex].BackgroundImage;
    }

    private void OnDisable()
    {
        InputHandler.MovePanelSelectorDisable();
    }
}
