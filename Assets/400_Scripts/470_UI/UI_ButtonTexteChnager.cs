using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionDebug : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public string highlightedText;
    public TextMeshProUGUI buttonTextMeshPro;
    public void OnSelect(BaseEventData eventData)
    {
       
        buttonTextMeshPro.text = highlightedText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        buttonTextMeshPro.text = highlightedText;
    }
}
