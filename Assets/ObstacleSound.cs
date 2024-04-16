using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    public AudioScriptable audioScriptable;
    public VFXScriptableObject vFXScriptableObject;

    private void PlaySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(vFXScriptableObject.soundObstacleId, audioSource);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            PlaySound();
        }
    }


}
