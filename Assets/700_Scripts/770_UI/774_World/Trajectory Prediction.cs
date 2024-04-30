using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LayerMask wallLayer;
    private float maxDist = 12;
    private float sphereCollider;

    private void Start()
    {
        InputHandler.TrajectoryPredictionEnable(this);

        sphereCollider = playerController.GetComponent<SphereCollider>().radius;
    }

    RaycastHit hit;
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (playerController.ThrowStrength > 0 || PlayerController.rb.velocity == Vector3.zero)
        {
            //if (Physics.Raycast(ray, out hit, 12, wallLayer))
            //{
            //    lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
            //
            //    lineRenderer.positionCount = 3;
            //
            //    lineRenderer.SetPosition(2, transform.InverseTransformPoint(hit.point + Vector3.Reflect(ray.direction, hit.normal) * 3));
            //}
            //else
            //{
            //    lineRenderer.positionCount = 2;
            //
            //    lineRenderer.SetPosition(1, Vector3.forward * 12);
            //}
            //
            //if (Physics.BoxCast(transform.position + new Vector3(0, -.5f, 0), sphereCollider / 1.4f, transform.forward, out hit, Quaternion.identity, 12, wallLayer))
            //{
            //    Vector3 hitPointwoY = new Vector3(hit.point.x, 0, hit.point.z);
            //
            //    Vector3 hitNormwoY = new Vector3(hit.normal.x, 0, hit.normal.z);
            //
            //    lineRenderer.SetPosition(1, transform.InverseTransformPoint(hitPointwoY));
            //
            //    lineRenderer.positionCount = 3;
            //
            //    lineRenderer.SetPosition(2, transform.InverseTransformPoint(hitPointwoY + Vector3.Reflect(ray.direction, hitNormwoY) * 3));
            //}
            //else
            //{
            //    lineRenderer.positionCount = 2;
            //
            //    lineRenderer.SetPosition(1, Vector3.forward * 12);
            //}

            if (Physics.CapsuleCast(transform.position, transform.position, sphereCollider - .03f, transform.forward, out hit, maxDist, wallLayer))
            {
                Vector3 hitPoint = new Vector3(hit.point.x, .3f, hit.point.z);
                Vector3 hitPointnoY = new Vector3(hit.point.x, .3f, hit.point.z);
                Vector3 hitNormnoY = new Vector3(hit.normal.x, 0, hit.normal.z);

                float hitDist = maxDist - Vector3.Distance(hit.point, transform.position);
            
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(hitPoint));
            
                lineRenderer.positionCount = 3;
            
                lineRenderer.SetPosition(2, transform.InverseTransformPoint(hitPointnoY + Vector3.Reflect(ray.direction, hitNormnoY) * (2 + hitDist)));
            }
            else
            {
                lineRenderer.positionCount = 2;
            
                lineRenderer.SetPosition(1, Vector3.forward * 12);
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
