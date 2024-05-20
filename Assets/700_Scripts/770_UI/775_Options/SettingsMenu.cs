using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    Resolution[] resolutions;

    public Toggle toggleFullscreen;

    // public Dropdown resolutionDropdown;

    private void Start()
    {
        //resolutions = Screen.resolutions;
        //
        ////resolutionDropdown.ClearOptions();
        //
        //List<string> options = new List<string>();
        //
        //int currentResolutionIndex = 0;
        //
        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    string option = resolutions[i].width + "x" + resolutions[i].height;
        //    options.Add(option);
        //
        //    if (resolutions[i].width == Screen.currentResolution.width &&
        //        resolutions[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = i;
        //    }
        //
        //}
        //
        //resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();

        Screen.fullScreen = PlayerOptionsRuntimeSave.FullscreenMode;
        toggleFullscreen.isOn = PlayerOptionsRuntimeSave.FullscreenMode;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        PlayerOptionsRuntimeSave.FullscreenMode = Screen.fullScreen;
    }
}
