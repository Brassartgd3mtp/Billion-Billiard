using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustStormManager : MonoBehaviour
{
    public ParticleSystem particleSystemToPlay;
    public float minEmissionInterval = 1f;
    public float maxEmissionInterval = 3f;
    void Start()
    {
        // Lancer la coroutine pour émettre des particules à intervalles réguliers
        StartCoroutine(PlayParticleSystemRoutine());
    }

    IEnumerator PlayParticleSystemRoutine()
    {
        while(true)
        {
            // Générer un intervalle aléatoire entre min et max
            float randomInterval = Random.Range(minEmissionInterval, maxEmissionInterval);

            // Attendre pendant l'intervalle spécifié
            yield return new WaitForSeconds(randomInterval);

            // Jouer le système de particules
            if (particleSystemToPlay != null)
            {
                particleSystemToPlay.Play();
            }
        }
    }
}