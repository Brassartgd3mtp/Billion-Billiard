using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
    public GameObject playButton;

    private void Start()
    {
        //InputHandler.Actions.Gamepad.Disable();
        //InputHandler.Actions.MouseKeyboard.Disable();

        if (SwapControls.state == CurrentState.MouseKeyboard)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (SwapControls.state == CurrentState.Gamepad)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(playButton);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
