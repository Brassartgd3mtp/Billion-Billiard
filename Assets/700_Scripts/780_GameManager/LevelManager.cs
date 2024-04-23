using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public LevelType levelType = LevelType.HoleInOne;
    private Scene currentScene;
    public float HIOCooldown = 1.5f;

    [Header("PARAMETER CIRCLE WIPE")]

    public Animator AnimatorCircleWipe;
    public Image ImageCircleWipe;
    public bool isIn = true;
    public float CircleSize = 0;
    private readonly int CircleSizeId = Shader.PropertyToID("_Circle_Size");

    public UI_ShotRemaining uI_ShotRemaining;

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

        AnimatorCircleWipe = gameObject.GetComponent<Animator>();
        ImageCircleWipe = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        ImageCircleWipe.materialForRendering.SetFloat(CircleSizeId, CircleSize);

        //    if (levelType == LevelType.HoleInOne)
        //    {
        //        if (PlayerController.rb.velocity.magnitude <= 0
        //        && TurnBasedPlayer.Instance.shotRemaining == 0)
        //        {
        //            SceneManager.LoadScene(currentScene.buildIndex);
        //        }
        //    }
    }

    public void AnimateIn()
    {
        AnimatorCircleWipe.SetTrigger("In");
        isIn = true;
        SoundTransitionOpen();
    }

    public void AnimateOut()
    {
        AnimatorCircleWipe.SetTrigger("Out");
        isIn = false;
        SoundTransitionClose();
    }

    // Update is called once per frame

    IEnumerator HoleInOne()
    {
        if (PlayerController.rb.velocity.magnitude > 0)
        {
            while (true)
            {
                if (PlayerController.rb.velocity.magnitude <= 0.1f
                && TurnBasedPlayer.Instance.shotRemaining == 0)
                {
                    yield return new WaitForSeconds(1f);

                    if (PlayerController.rb.velocity.magnitude <= 0.1f)
                    {
                        AnimateOut();
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
        else
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