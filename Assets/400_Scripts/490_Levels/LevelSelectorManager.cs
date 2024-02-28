using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    [SerializeField] private Button LeftArrow, RightArrow;
    [SerializeField] private Button BTN_Play;

    [SerializeField] private List<GameObject> Panels;
    [SerializeField] private List<SO_Level> SO_Levels;
    [SerializeField] private GameObject ActualPanel;
    private int PanelIndex;

    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [SerializeField] private float scrollingSpeed;

    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private GameObject Content;

    public void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void Start()
    {
        PanelIndex = 0;
        ActualPanel = Panels[PanelIndex];
        CheckIfNextPanelIsLocked();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        if(PanelIndex > 0) 
        {
        PanelIndex--;
        ActualPanel = Panels[PanelIndex];
        LeftArrow.enabled = false;
        RightArrow.enabled = false;
        StartCoroutine (MovePanel(1));
        }
    }
    public IEnumerator MovePanel(int xValue)
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();

        RectTransform rectTransform = Content.GetComponent<RectTransform>();
        Vector3 actualPos = rectTransform.transform.localPosition;
        Vector3 targetPos = actualPos + new Vector3((xValue * 800),0,0);
        while (rectTransform.localPosition !=  targetPos) 
        {
            rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, targetPos, scrollingSpeed * Time.deltaTime);
            Debug.Log("l�");
            yield return null;    
        }
        LeftArrow.enabled = true;
        RightArrow.enabled = true;

        CheckIfNextPanelIsLocked() ;
        yield break;
    }

    public void PlayButton()
    {
        ActualPanel.TryGetComponent(out LevelLoader levelLoader);
        SceneManager.LoadScene(levelLoader.levelID);
    }

    public void CheckIfNextPanelIsLocked()
    {
        Panels[PanelIndex + 1].TryGetComponent(out PanelManager panelManagerNext);
        if (panelManagerNext.SO_Level.LevelData.isLocked)
        {
            Debug.Log("Locked");
            RightArrow.gameObject.SetActive(false);
            RightArrow.enabled = false;
        } 
        else if (RightArrow.enabled && !RightArrow.gameObject.activeInHierarchy)
        {
            RightArrow.gameObject.SetActive(true);
            RightArrow.enabled = true;
        }
        if (ActualPanel == Panels[0])
        {
            LeftArrow.gameObject.SetActive(false);
            LeftArrow.enabled = false;
        } else if (ActualPanel != Panels[0])
        {
            LeftArrow.gameObject.SetActive(true);
            LeftArrow.enabled = true;
        }
    }


}
