using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Strenght Value")]
    public int StrenghMultiplier = 40;

    [Header("Gamepad Values")]
    public float GamepadThrowStrenght;
    public Vector2 PivotValue;

    [Header("Mouse Values")]
    public float MouseThrowStrenght;
    public Vector2 MouseStart;
    public Vector2 MouseEnd;
    Camera cam;

    [Header("References")]
    public GameObject Pivot;

    private Vector3 strenghtToScale;
    private Quaternion pivotToRotation;
    private float angle;

    private Rigidbody rb;
    private Vector3 lastVel;

    [Header("Bouce Multipliers")]
    [Tooltip("La valeur de Bounce des murs en béton")] public float ConcreteBounce = 1;
    [Tooltip("La valeur de Bounce des murs en caoutchouc")] public float RubberBounce = 1;

    [Space(20)]
    public bool isShooted;

    public Vector3 posBeforeHit;
    [SerializeField] private ParticleSystem myParticleSystem;

    private TurnBasedPlayer turnBasedPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        turnBasedPlayer = GetComponent<TurnBasedPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pivotToRotation = Pivot.transform.rotation;
        strenghtToScale = Pivot.transform.localScale;
        myParticleSystem = GetComponentInChildren<ParticleSystem>();
        cam = Camera.main;
    }

    public void ThrowPlayer(InputAction.CallbackContext context)
    {
        isShooted = true;

        myParticleSystem.Play();
        posBeforeHit = transform.position;
        Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        rb.AddForce(-forceDirection * GamepadThrowStrenght, ForceMode.Impulse);

        turnBasedPlayer.ShotCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
            angle = Mathf.Atan2(PivotValue.x, PivotValue.y) * Mathf.Rad2Deg;
        else
            angle = Mathf.Atan2(MouseEnd.x - MouseStart.x, MouseEnd.y - MouseStart.y) * Mathf.Rad2Deg;

        Pivot.transform.rotation = pivotToRotation;
        Pivot.transform.rotation = Quaternion.Euler(0, angle, 0);

        if (Gamepad.current != null)
            strenghtToScale.z = GamepadThrowStrenght / (StrenghMultiplier / 5);
        else
            strenghtToScale.z = MouseThrowStrenght / (StrenghMultiplier / 5);

        Pivot.transform.localScale = strenghtToScale;

        lastVel = rb.velocity;
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
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(lowfreq_strenght, highfreq_strenght);
            yield return new WaitForSeconds(timer);
            InputSystem.ResetHaptics();
            yield break;
        }
    }

    public void SetArrowDirection(InputAction.CallbackContext context)
    {
        PivotValue = context.ReadValue<Vector2>();

        if (context.canceled)
            PivotValue = new Vector2(0, 0);
    }

    public void ModifyStrenght(InputAction.CallbackContext context)
    {
        GamepadThrowStrenght = context.ReadValue<float>() * StrenghMultiplier;

        if (context.canceled)
            GamepadThrowStrenght = 0;
    }

    bool dragEnabled = false;
    public void MouseStrenght(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            MouseEnd = context.ReadValue<Vector2>();
            MouseThrowStrenght = Vector2.Distance(MouseStart, MouseEnd) / 5;
            MouseThrowStrenght = Mathf.Clamp(MouseThrowStrenght, 0, StrenghMultiplier);
        }
        else
            MouseStart = context.ReadValue<Vector2>();
    }

    public void MouseStartDrag(InputAction.CallbackContext context)
    {
        dragEnabled = true;

        if (context.canceled && MouseEnd != Vector2.zero)
        {
            isShooted = true;
            
            myParticleSystem.Play();
            posBeforeHit = transform.position;
            Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.AddForce(-forceDirection * MouseThrowStrenght, ForceMode.Impulse);
            
            turnBasedPlayer.ShotCount();

            dragEnabled = false;
            MouseThrowStrenght = 0;
            MouseEnd = Vector2.zero;
        }
    }
}
