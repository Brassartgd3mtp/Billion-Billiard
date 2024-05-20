using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Option_Panel : MonoBehaviour
{
    public GameObject PauseFirstbutton;

    private void OnEnable()
    {
        InputHandler.SettingsMenuEnable(this);
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseFirstbutton);
    }

    public void OnCloseButtonClick(InputAction.CallbackContext context)
    {
        Debug.Log("Disabling option panel");
        OnCloseButtonClick();
    }

    private void OnDisable()
    {
        InputHandler.SettingsMenuDisable();
    }
}
