using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleShotRemaining : MonoBehaviour
{
    public List<ParticleSystem> Shots = new List<ParticleSystem>(3);
    private List<Vector3> shotsPos = new List<Vector3>();
    [SerializeField] private ParticleSystem baseParticle;
    ParticleSystem lastParticle;
    int lastIndex;

    public void Initialize(int _totalShots)
    {
        float angle = Mathf.PI/2;

        for (int i = 0; i < _totalShots; i++)
        {
            Shots.Add(Instantiate(baseParticle, transform.position + new Vector3(Mathf.Cos(angle), 0.5f, Mathf.Sin(angle)), Quaternion.identity, transform));

            angle -= 45 * Mathf.Deg2Rad;

            shotsPos.Add(Shots[i].transform.position);

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

        lastParticle.TriggerSubEmitter(0);

        StartCoroutine(Delay(lastParticle, .15f));

        lastIndex--;
    }

    IEnumerator Delay(ParticleSystem p, float t)
    {
        yield return new WaitForSeconds(t);
        p.gameObject.SetActive(false);
    }
}
