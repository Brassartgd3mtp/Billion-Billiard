using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVFX : MonoBehaviour
{
    public float dureeDeVieFX = 5f;

    void Start()
    {
        StartCoroutine(DetruireApresDelai());

    }

    IEnumerator DetruireApresDelai()
    {
        yield return new WaitForSeconds(dureeDeVieFX);

        Destroy(gameObject);
    }
}
