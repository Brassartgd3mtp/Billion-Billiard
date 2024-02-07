using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    public List<GameObject> buttons;

    public void Start()
    {
        CheckUnlockedLevel();
    }
    public void CheckUnlockedLevel()
    {
        if (LevelData.Unlocked_Lv_1 == true)
        {
            buttons[0].SetActive(true);
        }
    }

    public void AnimationMovePlayerToLevel()
    {

    }
}
