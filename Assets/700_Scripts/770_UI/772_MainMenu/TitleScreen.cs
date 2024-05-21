using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class TitleScreen : MonoBehaviour
{
    [Header("PARAMETER CIRCLE WIPE")]
    public float targetValue = 1.0f;
    public float transitionDuration = 1.0f;
    private Image ImageCircleWipe;

    [Header("PARAMETER BLINK TEXT")]
    public float blinkInterval = 0.5f;
    private TextMeshProUGUI textPressKey;
    private bool isBlinking = false;

    [Header("PARAMETER GAMEOBJECT")]
    public GameObject TextButtonClick;
    public GameObject titleScreenPanel;
    public GameObject mainMenuPanel;

    void Start()
    {
        InputHandler.TitleScreeSkipEnable(this);

        mainMenuPanel.SetActive(false);

        ImageCircleWipe = gameObject.GetComponentInChildren<Image>();
        textPressKey = GetComponentInChildren<TextMeshProUGUI>();

        ImageCircleWipe.material = new Material(ImageCircleWipe.material);
        ImageCircleWipe.material.SetFloat("_Circle_Size", targetValue);

        if (textPressKey != null)
            StartBlinking();
    }

    public void Skip(InputAction.CallbackContext context)
    {
        mainMenuPanel.SetActive(true);
        StartCoroutine(TitleScreenWipe());
    }

    public void StartBlinking()
    {
        isBlinking = true;
        StartCoroutine(Blink());
    }

    public void StopBlinking()
    {
        isBlinking = false;
        SetAlpha(1f);
        StopCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (isBlinking)
        {
            SetAlpha(textPressKey.alpha == 1f ? 0f : 1f);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = textPressKey.color;
        color.a = alpha;
        textPressKey.color = color;
    }

    IEnumerator TitleScreenWipe()
    {
        TextButtonClick.SetActive(false);

        float startValue = ImageCircleWipe.material.GetFloat("_Circle_Size");
        float endValue = (startValue == 1.0f) ? 0.0f : 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / transitionDuration);
            ImageCircleWipe.material.SetFloat("_Circle_Size", newValue);
            yield return null;
        }

        ImageCircleWipe.material.SetFloat("_Circle_Size", endValue);

        titleScreenPanel.SetActive(false);
    }

    private void OnDisable()
    {
        InputHandler.TitleScreeSkipDisable();
    }
}