using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelUI", menuName = "Level Creation UI")
    ]
public class SO_Level : ScriptableObject
{
    public string LevelName;
    public string LevelDescription;
    public Sprite Image;
    public Sprite BackgroundImage;

    public LevelData LevelData;

    public int LevelID;
    public int CutsceneIndex;

    //public IEnumerator LoadScene(Image _progressBar)
    //{
    //    AsyncOperation _op = SceneManager.LoadSceneAsync(LevelID); 
    //    while (_op != null && !_op.isDone) 
    //    {
    //        _progressBar.fillAmount = _op.progress;
    //        yield return null;
    //    }
    //    //yield return SceneManager.LoadSceneAsync(LevelID);
    //     SceneManager.LoadScene(LevelID);
    //}

    public void LoadLevel()
    {
        if (!CutscenesCurrent.isCutsceneFirstTime[CutsceneIndex])
        {
            CutscenesCurrent.PackIndex = CutsceneIndex;
            LevelSelectorData.CurrentLevelIndex = LevelID;
            SceneManager.LoadScene(12);
        }
        else
            SceneManager.LoadScene(LevelID);
    }
}
