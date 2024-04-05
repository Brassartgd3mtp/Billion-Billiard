using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : MonoBehaviour
{
    [SerializeField] Vector3 finalPos;
    [SerializeField, Range(1, 50)] float speed;

    Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, finalPos, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
