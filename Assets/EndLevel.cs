using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public int nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
