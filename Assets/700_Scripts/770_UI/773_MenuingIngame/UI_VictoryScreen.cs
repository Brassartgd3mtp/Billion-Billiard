using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;

    private void OnEnable()
    {
        if (SwapControls.state == CurrentState.Gamepad)
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject);

            if (EventSystem.current.currentSelectedGameObject != firstButton)
                EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (SwapControls.state == CurrentState.MouseKeyboard)
    //    {
    //        Debug.Log(EventSystem.current.currentSelectedGameObject);
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.Confined;
    //        Cursor.visible = true;
    //    }
    //}
}
