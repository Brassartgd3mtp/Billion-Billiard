using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LayerMask wallLayer;

    private void Start()
    {
        InputHandler.TrajectoryPredictionEnable(this);
    }

    RaycastHit hit;
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (playerController.ThrowStrength > 0 || PlayerController.rb.velocity == Vector3.zero)
        {
            if (Physics.Raycast(ray, out hit, 12, wallLayer))
            {
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));

                lineRenderer.positionCount = 3;

                lineRenderer.SetPosition(2, transform.InverseTransformPoint(hit.point + Vector3.Reflect(ray.direction, hit.normal) * 3));
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
    static Vector3 forward;
    private void SetLookDirection(Vector2 _lookDirection)
    {
        LookingDirection = _lookDirection;

        angle = SwapControls.state == CurrentState.Gamepad
            ? Mathf.Atan2(LookingDirection.x, LookingDirection.y) * Mathf.Rad2Deg
            : Mathf.Atan2(-LookingDirection.x, -LookingDirection.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        forward = transform.forward;
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

    public static Vector3 Forward()
    {
        return forward;
    }

    private void OnDisable()
    {
        InputHandler.TrajectoryPredictionDisable();
    }
}
