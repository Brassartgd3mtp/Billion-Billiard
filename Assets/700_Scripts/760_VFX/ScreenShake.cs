using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;

    [SerializeField] float amplitude = 5f; 
    [SerializeField] float duration = 1f;
    [SerializeField] CinemachineVirtualCamera vCam;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Shake();
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public IEnumerator ShakeCoroutine()
    {
        Debug.Log("shake");

        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(duration);
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        Debug.Log("fin screenshake"); 
    }
}
