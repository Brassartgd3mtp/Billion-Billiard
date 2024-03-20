using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public SO_Level SO_Level;

    public Image imageUnlocked, imageLocked, padLock;
    public TMPro.TextMeshProUGUI description, title;

    public void Start()
    {
        imageUnlocked.sprite = SO_Level.Image;
        description.text = SO_Level.LevelDescription;
        title.text = SO_Level.LevelName;

        if (SO_Level.LevelData.isLocked)
        {
            title.enabled = false;
            description.enabled = false;
            padLock.enabled = true;
            imageUnlocked.enabled = false;
            imageLocked.enabled = true;
        } else
        {
            title.enabled = true;
            description.enabled = true;
            padLock.enabled = false;
            imageUnlocked.enabled = true;
            imageLocked.enabled = false;
        }
    }
}
