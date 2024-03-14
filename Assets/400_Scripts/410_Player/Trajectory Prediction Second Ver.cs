using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrajectoryPredictionSecVer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    //[SerializeField] private LineRenderer reflectedLine;

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
        }
        else
            lineRenderer.SetPosition(1, Vector3.forward * playerController.ThrowStrength / 4);

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

    Vector3 ReflectPosition(Vector3 position, Vector3 normal, Vector3 hitPoint)
    {
        Vector3 reflection = Vector3.Reflect(position - hitPoint, normal);
        return hitPoint + reflection;
    }
}
