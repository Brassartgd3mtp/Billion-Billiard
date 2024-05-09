using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChargeLoader : MonoBehaviour

{


    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));

    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //Le chargement Async permet de charger la scene en arriere plan, il faut ensuite refere le numero de l'index de la scene sur le bouton

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //Arrondi la valeurs du chargement de scene a 1 car unity considere que = 0 a 0.9 chargement de la scene et 0.9 a 1 activation de la scene

            slider.value = progress;
            //Pour faire evoluer le slider

            yield return null;
        }
    }
}
