using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_VisualShotRemaining : MonoBehaviour
{
    public List<Image> Shots = new List<Image>();
    [SerializeField] private TurnBasedPlayer TBP;
    [SerializeField] private Image baseImage;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < TBP.shotRemaining; i++)
        {
            Shots.Add(Instantiate(baseImage, transform));
        }

        Shots[0].transform.localPosition = new Vector3(0, 1.06f, 0);

        for (int i = 1; i < Shots.Count; i++)
        {
            Shots[i].transform.localPosition = new Vector3(0, 1.06f, 0);

            //float posX = .25f * Shots.Count - i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
