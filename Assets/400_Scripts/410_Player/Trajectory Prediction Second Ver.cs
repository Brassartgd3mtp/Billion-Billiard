using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrajectoryPredictionSecVer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LineRenderer reflectedLine;

    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    RaycastHit hit;
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, playerController.ThrowStrength / 4))
        {
            Vector3 contactPos = new Vector3(0, 0, Vector3.Distance(hit.point, transform.position));
            lineRenderer.SetPosition(1, contactPos);

            reflectedLine.startWidth = .5f;
            reflectedLine.SetPosition(0, contactPos);
            reflectedLine.SetPosition(1, Vector3.Reflect(ray.direction, hit.normal) + hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, Vector3.forward * playerController.ThrowStrength / 4);

            reflectedLine.SetPosition(0, Vector3.zero);
            reflectedLine.SetPosition(1, Vector3.zero);
        }

        lineRenderer.startWidth = 1 / (40 / playerController.ThrowStrength);
    }

    //public void Predict(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        
    //    }
    //}

    //public void CancelPredict(InputAction.CallbackContext context)
    //{
    //    
    //}

    //Vector3 CalculateVelocity()
    //{
    //    return transform.forward * playerController.ThrowStrength / 2;
    //}
}
