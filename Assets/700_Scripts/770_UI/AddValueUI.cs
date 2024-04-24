using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddValueUI : MonoBehaviour
{
    public GameObject PopUpText;
    public GameObject AddedValueEmptyParent;
    public Shake ShakeScript;
    public AudioSource audioSource;
    void Start()
    {
        ShakeScript = GetComponentInChildren<Shake>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {

    }
    public void updateUI(int value)
    {
        if(PopUpText.TryGetComponent(out TextMeshProUGUI textValue))
        {
            textValue.text = $"+{value}";
            textValue.fontSize = (value + 23 + value * 1.5f);
            if(textValue.fontSize > 64)
            {
                textValue.fontSize = 64;
            }
            GameObject newText = Instantiate(PopUpText,AddedValueEmptyParent.transform);

            ShakeScript.shakeIntensity = ShakeScript.baseShakeIntensity + value * 0.1f;
            ShakeScript.shakeIntensity = ShakeScript.baseShakeIntensity + value * 0.1f;
            ShakeScript.StartShake();
        }
    }
    //public void SoundMoney(float pitchvalue)
    //{
    //    audioSource.pitch = pitchvalue;
    //    AudioManager.Instance.PlaySound(22, audioSource);
    //}
}
