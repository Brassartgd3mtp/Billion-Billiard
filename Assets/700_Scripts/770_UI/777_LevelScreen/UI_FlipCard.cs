using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_FlipCard : MonoBehaviour
{

    public UnityEvent OnCardFlipped;

    [SerializeField] private GameObject recto;
    [SerializeField] private GameObject verso;
    public void FlipCard()
    {
        if(recto.activeSelf)
        {
            recto.SetActive(false);
            verso.SetActive(true);
        }
        else if (verso.activeSelf)
        {
            verso.SetActive(false);
            recto.SetActive(true);
        }

        if(!Globals.HasClickedOnCard)
        {
            Globals.HasClickedOnCard = true;
        }

        OnCardFlipped.Invoke();
    }

    

}
