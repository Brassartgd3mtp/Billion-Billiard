using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;


    private void Start()
    {
        SetLevel(PlayerOptionsRuntimeSave.AudioVolume);
    }

    public void SetLevel()
    {
        PlayerOptionsRuntimeSave.AudioVolume = slider.value;
        mixer.SetFloat("MusicVol", Mathf.Log10(slider.value) * 20);
    }

    public void SetLevel(float volume)
    {
        PlayerOptionsRuntimeSave.AudioVolume = volume;
        mixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
    }
}
