using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShotRemaining : MonoBehaviour
{
    private static List<Image> Shots = new List<Image>(3);
    private static List<Animator> shotsAnimations = new List<Animator>(3);
    [SerializeField] private Image baseImage;
    private static int lastIndex;

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
    }

    public void PassiveUpdateShots()
    {
        for (int i = 0; i < Shots.Count; i++)
        {
            if (!shotsAnimations[i].GetBool("ToShot"))
                continue;
            else
            {
                Shots[i].gameObject.SetActive(true);
                lastIndex++;
                return;
            }
        }
    }

    public void Death()
    {
        shotsAnimations[lastIndex].SetBool("Reload", true);

        lastIndex--;
    }

    public static void ToShot()
    {
        shotsAnimations[lastIndex].SetBool("ToShot", true);
    }

    public void Reload()
    {

    }
}
