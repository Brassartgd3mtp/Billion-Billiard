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
    [HideInInspector] public string CurrentDialogue;
    [SerializeField] LevelChargeLoader levelChargeLoader;

    // Start is called before the first frame update
    void Start()
    {
        TextBox.text = string.Empty;
        StartCoroutine(LoadCutscene(CutscenesCurrent.PackIndex, 0));

        switch (LevelSelectorData.CurrentLevelIndex)
        {
            case 11:
                levelChargeLoader.LoadLevelCutscene(1);
                LevelSelectorData.CurrentLevelIndex = 2;
                break;

            case 12:
                if (CutscenesCurrent.PackIndex == 7)
                {
                    levelChargeLoader.LoadLevelCutscene(1);
                    LevelSelectorData.CurrentLevelIndex = 10;
                }
                else
                    levelChargeLoader.LoadLevelCutscene(LevelSelectorData.CurrentLevelIndex);
                break;

            default:
                levelChargeLoader.LoadLevelCutscene(LevelSelectorData.CurrentLevelIndex);
                break;
        }
    }

    public IEnumerator LoadCutscene(int pack, int cutscene)
    {
        if (CutscenesCurrent.CutsceneIndex < packs[pack].cutscenes.Count)
        {
            TextBox.text = string.Empty;

            CurrentDialogue = packs[pack].diaglogues[cutscene];

            ScreenImage.sprite = packs[pack].cutscenes[cutscene];

            char[] chars = packs[pack].diaglogues[cutscene].ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (TextBox.text == CurrentDialogue)
                    break;

                TextBox.text += chars[i];
                yield return new WaitForSeconds(.05f);
            }

            yield break;
        }
        else if (!CutscenesCurrent.isCutsceneFirstTime[CutscenesCurrent.PackIndex])
        {
            CutscenesCurrent.CutsceneIndex = 0;
            CutscenesCurrent.isCutsceneFirstTime[CutscenesCurrent.PackIndex] = true;

            levelChargeLoader.operation.allowSceneActivation = true;
        }
        else
        {
            CutscenesCurrent.CutsceneIndex = 0;
            SceneManager.LoadScene(13);
        }
    }
}
