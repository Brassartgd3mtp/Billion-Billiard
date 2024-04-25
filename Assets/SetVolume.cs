using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer; 
    public void SetLevel(float slidervalue)

    {
        mixer.SetFloat ("MusicVol", Mathf.Log10 (slidervalue) *20);
    }
}
