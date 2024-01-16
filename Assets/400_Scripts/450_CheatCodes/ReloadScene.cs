using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    Scene activeScene;
    public InputActionAsset ActionAsset;
    // Start is called before the first frame update
    void Start()
    {
        InputActionMap ActionMap = ActionAsset.FindActionMap("Cheat");

        ActionMap.FindAction("Reload Scene").performed += Reload;

        activeScene = SceneManager.GetActiveScene();
        Debug.Log(activeScene.name);
    }

    private void Reload(InputAction.CallbackContext context)
    {
        Debug.Log("Reload Scene !");
        SceneManager.LoadScene(activeScene.name);
    }

    private void OnEnable()
    {
        ActionAsset.FindActionMap("Cheat").Enable();
    }

    private void OnDisable()
    {
        ActionAsset.FindActionMap("Cheat").Disable();
    }
}
