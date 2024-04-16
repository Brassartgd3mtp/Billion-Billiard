using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShotRemaining : MonoBehaviour
{
    [SerializeField] private List<Image> Shots = new List<Image>(3);
    [SerializeField] private Image baseImage;
    [HideInInspector] public List<Animator> shotsAnimations = new List<Animator>(3);
    public int lastIndex;
    static float currentShot;

    public LevelManager levelManager;

    public void Initialize(int _totalShots)
    {
        float angle = Mathf.PI / 2;

        for (int i = 0; i < _totalShots; i++)
        {
            Shots.Add(Instantiate(baseImage, transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)), Quaternion.Euler(90, 0, 0), transform));

            angle -= 45 * Mathf.Deg2Rad;

            shotsAnimations.Add(Shots[i].GetComponent<Animator>());

            lastIndex = i;
        }

        shotsAnimations[0].SetBool("isFirstIndex", true);
    }

    /// <summary>
    /// S'exécute à la fin d'un cooldown dans <c>TurnBasedPlayer</c>
    /// </summary>
    public void PassiveUpdateShots()
    {
        if (lastIndex < 2)
        {
            shotsAnimations[lastIndex + 1].SetBool("ToShot", false);

            if (shotsAnimations[lastIndex].GetInteger("ShotsLeft") >= 0)
                shotsAnimations[lastIndex + 1].Play("Shots.Reload", 0);

            lastIndex++;
        }
    }

    public void Shot()
    {
        if (lastIndex > -1)
        {
            if (lastIndex > 0 && shotsAnimations[lastIndex].GetInteger("ShotsLeft") < 3)
                lastIndex--;

            if (lastIndex < 2)
            {
                if (levelManager.levelType == LevelType.HoleInOne)
                {
                    shotsAnimations[lastIndex].SetBool("ToShot", true);
                }
                else
                {
                    currentShot = GetCurrentAnimTime(shotsAnimations[lastIndex + 1]);
                    shotsAnimations[lastIndex].SetBool("ToShot", false);
                    shotsAnimations[lastIndex + 1].SetBool("ToShot", true);

                    shotsAnimations[lastIndex].Play("Shots.Reload", 0, currentShot);
                }
            }
            else shotsAnimations[lastIndex].Play("Shots.Reload", 0);
        }
        else
            Debug.LogWarning("Can't perform Shot, there is no shots left !");
    }

    static float GetCurrentAnimTime(Animator anim)
    {
        AnimatorStateInfo stateInfo;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            return stateInfo.normalizedTime;
        }
        else
            return 0f;
    }
}
