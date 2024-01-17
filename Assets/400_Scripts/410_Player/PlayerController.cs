using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
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
    private InputAction noClip;

    private Vector3 strenghtToScale;
    private Quaternion pivotToRotation;
    private float angle;

    private TurnBasedPlayer turnBasedPlayer;
    private Rigidbody rb;
    public bool isShooted;
    Vector3 lastVel;

    [Header("Bouce Multipliers")]
    [Tooltip("La valeur de Bounce des murs en béton")] public float ConcreteBounce = 1;
    [Tooltip("La valeur de Bounce des murs en caoutchouc")] public float RubberBounce = 1;
    [Tooltip("La valeur de Bounce des ennemis")] public float NPCBounce = 1;

    public Vector3 posBeforeHit;
    [SerializeField] private ParticleSystem myParticleSystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        turnBasedPlayer = GetComponent<TurnBasedPlayer>();

        InputActionMap Gamepad = ActionAsset.FindActionMap("Gamepad");

        ArrowDirection = Gamepad.FindAction("Arrow Direction");

        ArrowStrenght = Gamepad.FindAction("Strenght Modifier");

        Gamepad.FindAction("Throw Player").performed += ThrowPlayer;
    }

    private void ThrowPlayer(InputAction.CallbackContext ctx)
    {
        turnBasedPlayer.isShooted = true;
        TurnBasedSystem.Instance.Check();
        myParticleSystem.Play();
        posBeforeHit = transform.position;
        Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        rb.AddForce(forceDirection * ThrowStrenght, ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        pivotToRotation = Pivot.transform.rotation;
        strenghtToScale = Pivot.transform.localScale;
        myParticleSystem = GetComponentInChildren<ParticleSystem>();
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
            Vector3 reflect = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);

            switch (obstacle.obstacleType)
            {
                case Obstacle.ObstacleType.Concrete:
                    rb.velocity = reflect * Mathf.Max(speed * ConcreteBounce, 0f);
                    StartCoroutine(Haptic(0f, .5f, .2f));
                    break;
                case Obstacle.ObstacleType.Rubber:
                    StartCoroutine(Haptic(0f, .5f, .2f));
                    rb.velocity = reflect * Mathf.Max(speed * RubberBounce, 0f);
                    break;
                case Obstacle.ObstacleType.NPC:
                    StartCoroutine(Haptic(0f, .5f, .2f));
                    break;
            }
        }
    }

    /// <summary>
    /// Crée une vibration dans la manette.
    /// </summary>
    /// <param name="lowfreq_strenght"></param>
    /// <param name="highfreq_strenght"></param>
    /// <param name="timer"></param>
    /// <returns>Coroutine</returns>
    IEnumerator Haptic(float lowfreq_strenght, float highfreq_strenght, float timer)
    {
        Gamepad.current.SetMotorSpeeds(lowfreq_strenght, highfreq_strenght);
        yield return new WaitForSeconds(timer);
        InputSystem.ResetHaptics();
        yield break;
    }
}
