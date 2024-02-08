using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    [SerializeField] private Button LeftArrow, RightArrow;
    [SerializeField] private Button BTN_Play;
    [SerializeField] private List<GameObject> Panels;

    [SerializeField] private List<SO_Level> SO_Levels;

    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [SerializeField] private float scrollingSpeed;

    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private GameObject Content;

    public void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void NextPanel()
    {    
        LeftArrow.enabled = false;
        RightArrow.enabled = false;
        StartCoroutine(MovePanel(-1));

        //if(scrollRect !=  null) 
        //{
        //    if (scrollRect.horizontalNormalizedPosition >= 0)
        //    {
        //        scrollRect.horizontalNormalizedPosition += scrollingSpeed;
        //    }
        //}
    }

    public IEnumerator MovePanel(int xValue)
    {
        RectTransform rectTransform = Content.GetComponent<RectTransform>();
        Vector3 actualPos = rectTransform.transform.localPosition;
        Vector3 targetPos = actualPos + new Vector3((xValue * 800),0,0);
        while (rectTransform.localPosition !=  targetPos) 
        {
            rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, targetPos, scrollingSpeed * Time.deltaTime);
            Debug.Log("là");
            yield return null;    
        }
        LeftArrow.enabled = true;
        RightArrow.enabled = true;
        yield break;
    }

    public void PrevPanel() 
    {
        LeftArrow.enabled = false;
        RightArrow.enabled = false;
        StartCoroutine (MovePanel(1));
    }
}
