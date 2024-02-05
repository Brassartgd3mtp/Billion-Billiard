using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonTextAssigner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textMeshPro;
    public string buttonText;

    void Start()
    {
        // Assurez-vous que le texte Mesh Pro est défini
        if (textMeshPro == null)
        {
            textMeshPro = GameObject.FindWithTag("ButtonText").GetComponent<TextMeshProUGUI>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change le texte du TextMeshPro lorsque le curseur est sur le bouton
        if (textMeshPro != null && !string.IsNullOrEmpty(buttonText))
        {
            textMeshPro.text = buttonText;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Rétablit le texte par défaut lorsque le curseur quitte le bouton
        if (textMeshPro != null && !string.IsNullOrEmpty(buttonText))
        {
            textMeshPro.text = "";
        }
    }
}
