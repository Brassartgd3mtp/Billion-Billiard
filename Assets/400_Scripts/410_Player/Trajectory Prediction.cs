using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    public int predictionSteps = 30;
    public float predictionStepInterval = 0.1f;
    public GameObject predictionPointPrefab;

    private GameObject[] predictionPoints;
    PlayerController playerController;
    Rigidbody ballRigidbody;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();

        predictionPoints = new GameObject[predictionSteps];

        for (int i = 0; i < predictionSteps; i++)
        {
            predictionPoints[i] = Instantiate(predictionPointPrefab, transform.position, Quaternion.identity, transform);
        }
    }

    void Update()
    {
        Vector3 velocity = CalculateVelocity();
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < predictionSteps; i++)
        {
            float time = i * predictionStepInterval;
            Vector3 newPosition = currentPosition + velocity * time;
            newPosition.y = 0;
            predictionPoints[i].transform.position = newPosition;
        }
    }


    Vector3 CalculateVelocity()
    {
        return transform.forward * playerController.ThrowStrength / 2;
    }
}
