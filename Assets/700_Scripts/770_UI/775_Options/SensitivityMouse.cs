using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityController : MonoBehaviour
{
    public Slider sensitivitySlider;
    public float minSensitivity = 1f;
    public float maxSensitivity = 10f;
    public float defaultSensitivity = 5f;

    private void Start()
    {
        // Initialize slider with default sensitivity
        sensitivitySlider.value = defaultSensitivity;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        // Clamp sensitivity within min and max bounds
        float clampedSensitivity = Mathf.Clamp(sensitivity, minSensitivity, maxSensitivity);

        // Set the mouse sensitivity directly
        MouseLook.SetSensitivity(clampedSensitivity);
    }
}

public static class MouseLook
{
    private static float sensitivity = 5f; // Default sensitivity

    public static void SetSensitivity(float newSensitivity)
    {
        sensitivity = newSensitivity;
    }

    public static float GetSensitivity()
    {
        return sensitivity;
    }
}
