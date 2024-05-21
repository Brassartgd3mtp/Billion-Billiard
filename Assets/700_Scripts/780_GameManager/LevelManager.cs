using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public LevelType levelType = LevelType.HoleInOne;
    private Scene currentScene;
    public float HIOCooldown = 1.5f;

    public UI_ShotRemaining uI_ShotRemaining;
    [SerializeField] private GameObject victoryScreen;

    [Header("PARAMETER CIRCLE WIPE")]

    public Image ImageCircleWipe;
    public float transitionDuration = 1.0f;
    public float targetValue = 1.0f;
    public bool WipeClose = false;
    //public Animator AnimatorCircleWipe;
    //public bool isIn = true;
    //public float CircleSize = 0;
    //private readonly int CircleSizeId = Shader.PropertyToID("_Circle_Size");



    private void Start()
    {
        if (levelType == LevelType.HoleInOne)
        {
            currentScene = SceneManager.GetActiveScene();

            TurnBasedPlayer.Instance.shotRemaining = 1;
            TurnBasedPlayer.Instance.nbrOfShots = 1;

            TurnBasedPlayer.Instance.PassiveReloadEnabled = false;
            TurnBasedPlayer.Instance.PassiveReloadCooldown = 1;

            StartCoroutine(HoleInOne());
        }

        //AnimatorCircleWipe = gameObject.GetComponent<Animator>();
        ImageCircleWipe = gameObject.GetComponent<Image>();

        ImageCircleWipe.material = new Material(ImageCircleWipe.material);
        ImageCircleWipe.material.SetFloat("_Circle_Size", targetValue);
    }

    void Update()
    {
        if (WipeClose == true && ImageCircleWipe.material.GetFloat("_Circle_Size") == 1)
        {
            StartCoroutine(CircleSizeProgress());
            //SoundTransitionClose();
        }
        else if (WipeClose == false && ImageCircleWipe.material.GetFloat("_Circle_Size") == 0)
        {
            StartCoroutine(CircleSizeProgress());
            //SoundTransitionOpen();
        }

        //ImageCircleWipe.materialForRendering.SetFloat(CircleSizeId, CircleSize);
    }

    private IEnumerator CircleSizeProgress()
    {
        float startValue = ImageCircleWipe.material.GetFloat("_Circle_Size");
        float endValue = (startValue == 1.0f) ? 0.0f : 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / transitionDuration);
            ImageCircleWipe.material.SetFloat("_Circle_Size", newValue);
            yield return null;
        }

        ImageCircleWipe.material.SetFloat("_Circle_Size", endValue);
    }

    //public void AnimateIn()
    //{
    //    AnimatorCircleWipe.SetTrigger("In");
    //    isIn = true;
    //    SoundTransitionOpen();
    //}

   //public void AnimateOut()
   //{
   //    AnimatorCircleWipe.SetTrigger("Out");
   //    isIn = false;
   //    SoundTransitionClose();
   //}



    IEnumerator HoleInOne()
    {
        if (PlayerController.rb.velocity.magnitude > 0)
        {
            while (true)
            {
                if (PlayerController.rb.velocity.magnitude <= 0.1f
                && TurnBasedPlayer.Instance.shotRemaining == 0
                && !victoryScreen.activeSelf)
                {
                    yield return new WaitForSeconds(1f);

                    if (PlayerController.rb.velocity.magnitude <= 0.1f)
                    {
                        //AnimateOut();
                        WipeClose = true;
                        yield return new WaitForSeconds(HIOCooldown);
                        SceneManager.LoadScene(currentScene.buildIndex);
                    }
                    else
                    {
                        yield return new WaitForSeconds(.1f);
                        StartCoroutine(HoleInOne());
                    }

                    yield break;
                }
                else
                    yield return null;
            }
        }
        else if (!victoryScreen.activeSelf)
        {
            yield return new WaitForSeconds(.1f);
            StartCoroutine(HoleInOne());
            yield break;
        }
    }

    private void SoundTransitionOpen()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(20, audioSource);
    }

    private void SoundTransitionClose()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(21, audioSource);
    }
}
public enum LevelType
{
    HoleInOne,
    Exploration
}