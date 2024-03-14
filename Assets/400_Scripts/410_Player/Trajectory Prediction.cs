using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrajectoryPrediction : MonoBehaviour
{
    public int predictionSteps = 30;
    public float predictionStepInterval = 0.1f;
    public GameObject predictionPointPrefab;

    private GameObject[] predictionPoints;
    PlayerController playerController;

    Vector3 currentPos;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        predictionPoints = new GameObject[predictionSteps];

        Vector3 pointsPos = new Vector3(transform.position.x, 0f, transform.position.z);

        for (int i = 0; i < predictionSteps; i++)
        {
            predictionPoints[i] = Instantiate(predictionPointPrefab, pointsPos, Quaternion.identity, transform);
        }
    }

    private void Update()
    {
        currentPos = transform.position;
    }

    public void Predict(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            for (int i = 0; i < predictionSteps; i++)
            {
                float time = i * predictionStepInterval;
                Vector3 newPosition = currentPos + CalculateVelocity() * time;
                newPosition.y = .5f;

                if (i > 0)
                {
                    float maxDist = Vector3.Distance(predictionPoints[i - 1].transform.position, predictionPoints[i].transform.position);

                    RaycastHit hit;
                    if (Physics.Raycast(predictionPoints[i - 1].transform.position, newPosition - predictionPoints[i - 1].transform.position, out hit, maxDist))
                    {
                        newPosition = ReflectPosition(newPosition, hit.normal, hit.point);
                    }
                }

                predictionPoints[i].transform.position = newPosition;
            }
        }
    }

    public void CancelPredict(InputAction.CallbackContext context)
    {
        foreach (GameObject point in predictionPoints)
        {
            point.transform.localPosition = new Vector3(0, -.5f, 0);
        }
    }

    Vector3 CalculateVelocity()
    {
        return transform.forward * playerController.ThrowStrength / 2;
    }

    Vector3 ReflectPosition(Vector3 position, Vector3 normal, Vector3 hitPoint)
    {
        Vector3 reflection = Vector3.Reflect(position - hitPoint, normal);
        return hitPoint + reflection;
    }
}
