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
        // Lancer la coroutine pour �mettre des particules � intervalles r�guliers
        StartCoroutine(PlayParticleSystemRoutine());
    }

    IEnumerator PlayParticleSystemRoutine()
    {
        while(true)
        {
            // G�n�rer un intervalle al�atoire entre min et max
            float randomInterval = Random.Range(minEmissionInterval, maxEmissionInterval);

            // Attendre pendant l'intervalle sp�cifi�
            yield return new WaitForSeconds(randomInterval);

            // Jouer le syst�me de particules
            if (particleSystemToPlay != null)
            {
                particleSystemToPlay.Play();
            }
        }
    }
}