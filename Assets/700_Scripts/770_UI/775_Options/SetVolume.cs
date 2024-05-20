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
        slider.value = PlayerOptionsRuntimeSave.AudioVolume * 10;
    }

    public void SetLevel()
    {
        PlayerOptionsRuntimeSave.AudioVolume = slider.value / 10;
        mixer.SetFloat("MusicVol", Mathf.Log(PlayerOptionsRuntimeSave.AudioVolume, 2) * 20);
    }

    public void SetLevel(float volume)
    {
        PlayerOptionsRuntimeSave.AudioVolume = volume;
        mixer.SetFloat("MusicVol", Mathf.Log(volume, 2) * 20);
    }
}
