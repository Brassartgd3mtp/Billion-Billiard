using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;

    [Header("Source")]
    [SerializeField] float impulseForce = 1f;
    [SerializeField] float sourceDuration = .2f;
    [SerializeField] Vector3 velocity = new Vector3(0, -1, 0);

    [Header("Listener")]
    [SerializeField] float amplitude = 1f;
    [SerializeField] float frequency = 1f;
    [SerializeField] float listenerDuration = 1f;
    //[SerializeField] CinemachineVirtualCamera vCam;

    [Header("References")]
    [SerializeField] CinemachineImpulseSource source;
    [SerializeField] CinemachineImpulseListener listener;
    CinemachineImpulseDefinition definition;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        definition = source.m_ImpulseDefinition;
        //Shake();
    }

    private void Start()
    {
        definition.m_ImpulseDuration = sourceDuration;
        source.m_DefaultVelocity = velocity;

        listener.m_ReactionSettings.m_AmplitudeGain = amplitude;
        listener.m_ReactionSettings.m_FrequencyGain = frequency;
        listener.m_ReactionSettings.m_Duration = listenerDuration;
    }

    public void Shake(float force)
    {
        //StartCoroutine(ShakeCoroutine());
        source.GenerateImpulseWithForce(force * impulseForce);
    }

    //public IEnumerator ShakeCoroutine()
    //{
    //    Debug.Log("shake");
    //
    //    vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    //    yield return new WaitForSeconds(duration);
    //    vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    //    Debug.Log("fin screenshake"); 
    //}
}
