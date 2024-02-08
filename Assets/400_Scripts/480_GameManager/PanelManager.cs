using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public SO_Level SO_Level;

    [Header("UI In Scene")]
    public Image image;
    public TMPro.TextMeshProUGUI description, name;

    public void Start()
    {
        image.sprite = SO_Level.Image;
        description.text = SO_Level.LevelDescription;
        name.text = SO_Level.LevelName;
    }
}
