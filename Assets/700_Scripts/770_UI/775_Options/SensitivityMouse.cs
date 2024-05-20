using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public Slider sensitivitySlider;
    public float minSensitivity = .1f;
    public float maxSensitivity = 1f;

    private void Start()
    {
        sensitivitySlider.value = PlayerOptionsRuntimeSave.MouseSensitivity * 10;
    }

    public void SetMouseSensitivity()
    {
        PlayerOptionsRuntimeSave.MouseSensitivity = sensitivitySlider.value / 10;

        if (playerController != null)
            playerController.MouseSensitivity = PlayerOptionsRuntimeSave.MouseSensitivity;
    }
}

//public static class MouseLook
//{
//    private static float sensitivity = 5f; // Default sensitivity
//
//    public static void SetSensitivity(float newSensitivity)
//    {
//        sensitivity = newSensitivity;
//    }
//
//    public static float GetSensitivity()
//    {
//        return sensitivity;
//    }
//}
