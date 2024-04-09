using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShotRemaining : MonoBehaviour
{
    public List<Image> Shots = new List<Image>(3);
    [SerializeField] private Image baseImage;
    Image lastParticle;
    int lastIndex;

    public void Initialize(int _totalShots)
    {
        float angle = Mathf.PI / 2;

        for (int i = 0; i < _totalShots; i++)
        {
            Shots.Add(Instantiate(baseImage, transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)), Quaternion.Euler(90, 0, 0), transform));

            angle -= 45 * Mathf.Deg2Rad;

            lastParticle = Shots.Last();

            lastIndex = i;
        }
    }

    public void PassiveUpdateShots()
    {
        for (int i = 0; i < Shots.Count; i++)
        {
            if (Shots[i].gameObject.activeSelf)
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
        lastParticle = Shots[lastIndex];
        //
        //lastParticle.TriggerSubEmitter(0);

        lastParticle.gameObject.SetActive(false);

        lastIndex--;
    }
}
