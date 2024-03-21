using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Skip : MonoBehaviour
{
    public GameObject DisplayToSkip;
    private bool CanSkip = true;
    // Start is called before the first frame update
    void Start()
    {
        DisplayToSkip.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSkip)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                DisplayToSkip.SetActive(false);
            }
        }
    }
}
