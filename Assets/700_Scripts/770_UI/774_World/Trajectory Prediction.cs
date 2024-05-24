using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LayerMask wallLayer;
    private float maxDist = 12;
    private float sphereRadius;

    private void Start()
    {
        InputHandler.TrajectoryPredictionEnable(this);

        sphereRadius = playerController.GetComponent<SphereCollider>().radius;
    }

    RaycastHit hit;
    RaycastHit bounceHit;
    private void Update()
    {
        if (playerController.ThrowStrength > 0 || PlayerController.rb.velocity == Vector3.zero)
        {
            if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, maxDist, wallLayer))
            {
                Vector3 hitPoint = new Vector3(hit.point.x, .3f, hit.point.z);
                Vector3 hitNormnoY = new Vector3(hit.normal.x, 0, hit.normal.z);

                float hitDist = maxDist - hit.distance;
            
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(hitPoint));
            
                lineRenderer.positionCount = 3;

                Vector3 bounceForward = Vector3.Reflect(transform.forward, hitNormnoY);

                if (Physics.SphereCast(hitPoint, sphereRadius, bounceForward, out bounceHit, hitDist, wallLayer))
                {
                    Vector3 bouncePoint = new Vector3(bounceHit.point.x, .3f, bounceHit.point.z);
                    lineRenderer.SetPosition(2, transform.InverseTransformPoint(bouncePoint));
                }
                else
                    lineRenderer.SetPosition(2, transform.InverseTransformPoint(hitPoint + bounceForward * hitDist));
            }
            else
            {
                lineRenderer.positionCount = 2;
            
                lineRenderer.SetPosition(1, Vector3.forward * maxDist);
            }
        }
        else
        {
            lineRenderer.positionCount = 2;

            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }

    float angle;
    Vector2 LookingDirection;
    private void SetLookDirection(Vector2 _lookDirection)
    {
        LookingDirection = _lookDirection;

        angle = SwapControls.state == CurrentState.Gamepad
            ? Mathf.Atan2(LookingDirection.x, LookingDirection.y) * Mathf.Rad2Deg
            : Mathf.Atan2(-LookingDirection.x, -LookingDirection.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void MouseDirection(InputAction.CallbackContext context)
    {
        if (playerController.dragEnabled)
            SetLookDirection((context.ReadValue<Vector2>() - playerController.MouseStart).normalized);
    }

    public void GamepadDirection(InputAction.CallbackContext context)
    {
        SetLookDirection(context.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        InputHandler.TrajectoryPredictionDisable();
    }
}
