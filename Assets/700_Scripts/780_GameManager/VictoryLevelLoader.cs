using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VictoryLevelLoader : MonoBehaviour
{
    [SerializeField] LevelChargeLoader levelChargeLoader;

    public void LoadScene(int sceneIndex)
    {
        levelChargeLoader.LoadLevel(sceneIndex);
        Time.timeScale = 1.0f;
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1.0f;

    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}