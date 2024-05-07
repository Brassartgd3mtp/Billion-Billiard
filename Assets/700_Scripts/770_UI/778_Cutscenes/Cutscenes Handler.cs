using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutscenesHandler : MonoBehaviour
{
    public List<CutscenesList> packs;
    public Image ScreenImage;
    public TextMeshProUGUI TextBox;
    [HideInInspector] public bool CurrentlyLoadingCutscene;

    // Start is called before the first frame update
    void Start()
    {
        TextBox.text = string.Empty;
        StartCoroutine(LoadCutscene(CutscenesCurrent.PackIndex, 0));
    }

    public IEnumerator LoadCutscene(int pack, int cutscene)
    {
        if (CutscenesCurrent.CutsceneIndex < packs[pack].cutscenes.Count)
        {
            CurrentlyLoadingCutscene = true;

            TextBox.text = string.Empty;

            ScreenImage.sprite = packs[pack].cutscenes[cutscene];

            char[] chars = packs[pack].diaglogues[cutscene].ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                TextBox.text += chars[i];
                yield return new WaitForSeconds(.05f);
            }

            CurrentlyLoadingCutscene = false;

            yield break;
        }
        else
        {
            CutscenesCurrent.CutsceneIndex = 0;
            SceneManager.LoadScene(LevelSelectorData.CurrentLevelIndex);
        }
    }
}
