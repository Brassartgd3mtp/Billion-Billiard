using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleShotRemaining : MonoBehaviour
{
    public List<ParticleSystem> Shots = new List<ParticleSystem>();
    [SerializeField] private ParticleSystem baseParticle;

    public void Initialize(int _totalShots)
    {
        float angle = Mathf.PI/2;

        for (int i = 0; i < _totalShots; i++)
        {
            Shots.Add(Instantiate(baseParticle, transform.position + new Vector3(Mathf.Cos(angle), 0.5f, Mathf.Sin(angle)), Quaternion.identity, transform));

            angle -= 45 * Mathf.Deg2Rad;
        }
    }

    public void Death()
    {
        ParticleSystem lastParticle = Shots.Last();
        ParticleSystem lastSubParticle = Shots.Last().gameObject.GetComponentInChildren<ParticleSystem>();

        lastParticle.TriggerSubEmitter(0);
        Shots.Remove(lastParticle);

        Destroy(lastSubParticle.gameObject, .15f);
    }
}
