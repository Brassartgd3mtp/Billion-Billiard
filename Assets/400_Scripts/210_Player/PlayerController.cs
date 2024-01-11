using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Strenght Value")]
    public int StrenghMultiplier = 40;

    [Header("Input Values")]
    public float ThrowStrenght;
    [Tooltip("Modifiez cette variable pour augmenter ou baisser la force de propulsion")]
    public Vector2 PivotValue;

    [Header("References")]
    public InputActionAsset ActionAsset;
    public GameObject Pivot;

    private InputAction ArrowDirection;
    private InputAction ArrowStrenght;

    private Vector3 strenghtToScale;
    private Quaternion pivotToRotation;
    private float angle;

    private Rigidbody rb;
    private Vector3 lastVel;

    [Header("Bouce Multipliers")]
    [Tooltip("La valeur de Bounce des murs en b�ton")] public float ConcreteBounce = 1;
    [Tooltip("La valeur de Bounce des murs en caoutchouc")] public float RubberBounce = 1;
    [Tooltip("La valeur de Bounce des ennemis")] public float NPCBounce = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        InputActionMap ActionMap = ActionAsset.FindActionMap("Gamepad");

        ArrowDirection = ActionMap.FindAction("Arrow Direction");

        ArrowStrenght = ActionMap.FindAction("Strenght Modifier");

        ActionMap.FindAction("Throw Player").performed += ThrowPlayer;
    }

    private void ThrowPlayer(InputAction.CallbackContext ctx)
    {
        Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        Debug.Log(forceDirection);
        rb.AddForce(forceDirection * ThrowStrenght, ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        pivotToRotation = Pivot.transform.rotation;
        strenghtToScale = Pivot.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ThrowStrenght = ArrowStrenght.ReadValue<float>() * StrenghMultiplier;
        PivotValue = ArrowDirection.ReadValue<Vector2>();

        angle = Mathf.Atan2(PivotValue.x, PivotValue.y) * Mathf.Rad2Deg;
        Pivot.transform.rotation = pivotToRotation;
        Pivot.transform.rotation = Quaternion.Euler(0, angle, 0);

        strenghtToScale.z = ThrowStrenght / (StrenghMultiplier / 5);
        Pivot.transform.localScale = strenghtToScale;

        lastVel = rb.velocity;
    }

    private void OnEnable()
    {
        ActionAsset.FindActionMap("Gamepad").Enable();
    }

    private void OnDisable()
    {
        ActionAsset.FindActionMap("Gamepad").Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            float speed = lastVel.magnitude;
            Vector3 direction = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);

            switch (obstacle.obstacleType)
            {
                case Obstacle.ObstacleType.Concrete:
                    rb.velocity = direction * Mathf.Max(speed * ConcreteBounce, 0f);
                    break;
                case Obstacle.ObstacleType.Rubber:
                    rb.velocity = direction * Mathf.Max(speed * RubberBounce, 0f);
                    break;
                case Obstacle.ObstacleType.NPC:
                    rb.velocity = direction * Mathf.Max(speed * NPCBounce, 0f);
                    break;
            }
        }
    }
}
