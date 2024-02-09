using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUI", menuName = "Level Creation UI")
    ]
public class SO_Level : ScriptableObject
{
    public string LevelName;
    public string LevelDescription;
    public Sprite Image;

    public LevelData LevelData;
}
