using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask wallLayer;

    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
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
}
