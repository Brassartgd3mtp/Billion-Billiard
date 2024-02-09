using Cinemachine;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [Header("Strenght Value")]
    public int StrenghtMultiplier = 40;

    [Header("Gamepad Values")]
    public float GamepadThrowStrenght;
    public Vector2 PivotValue;

    [Header("Mouse Values")]
    public float MouseThrowStrenght;
    public Vector2 MouseStart;
    public Vector2 MouseEnd;

    [Header("References")]
    public GameObject Pivot;

    private Vector3 strenghtToScale;
    private Quaternion pivotToRotation;
    private float angle;

    public static Rigidbody rb;
    private Vector3 lastVel;

    [Header("Bouce Multipliers")]
    [Tooltip("La valeur de Bounce des murs en beton")] public float ConcreteBounce = 1;
    [Tooltip("La valeur de Bounce des murs en caoutchouc")] public float RubberBounce = 1;
    [Tooltip("La valeur de Bounce des murs en feutre")] public float FeltBounce = 1;

    [Space(20)]
    public bool isShooted;

    public Vector3 posBeforeHit;

    private TurnBasedPlayer turnBasedPlayer;

    [Header("VFX Parameter")]
    [SerializeField] private ParticleSystem speedEffect;
    [SerializeField] private GameObject speedEffectDirection;
    [SerializeField] private VisualEffect smokePoof;

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

        smokePoof = GetComponentInChildren<VisualEffect>();
        speedEffect = GetComponentInChildren<ParticleSystem>();

        MouseStart = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public void GamepadThrow(InputAction.CallbackContext context)
    {
        isShooted = true;

        posBeforeHit = transform.position;
        Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        rb.AddForce(-forceDirection * GamepadThrowStrenght, ForceMode.Impulse);

        smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        smokePoof.SetFloat("SmokeSize", GamepadThrowStrenght / StrenghtMultiplier);
        smokePoof.Play();

        var emissionSpeedEffect = speedEffect.emission;
        emissionSpeedEffect.rateOverTime = GamepadThrowStrenght / StrenghtMultiplier * 200f;


        speedEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var durationSpeedEffect = speedEffect.main;
        durationSpeedEffect.duration = GamepadThrowStrenght / StrenghtMultiplier;

        speedEffectDirection.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        speedEffect.Play();

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
            strenghtToScale.z = GamepadThrowStrenght / (StrenghtMultiplier / 5);
        else
            strenghtToScale.z = MouseThrowStrenght / (StrenghtMultiplier / 5);

        Pivot.transform.localScale = strenghtToScale;

        lastVel = rb.velocity;

        if (lastVel.magnitude > 0f)
        {
            Vector3 direction = rb.velocity.normalized;
        
            Quaternion newRot = Quaternion.LookRotation(direction);
        
            rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);
        }
        else
            rb.rotation = Quaternion.Euler(0f, -angle, 0f);
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
                    StartCoroutine(Haptic(0f, 1f, .2f));
                    break;

                case Obstacle.ObstacleType.Rubber:
                    StartCoroutine(Haptic(0f, 1f, .2f));
                    rb.velocity = reflect * Mathf.Max(speed * RubberBounce, 0f);
                    break;

                case Obstacle.ObstacleType.Felt:
                    StartCoroutine(Haptic(0f, 1f, .2f));
                    rb.velocity = reflect * Mathf.Max(speed * FeltBounce, 0f);
                    break;

                case Obstacle.ObstacleType.NPC:
                    StartCoroutine(Haptic(0f, 1f, .2f));
                    break;
            }
        }
    }

    /// <summary>
    /// Effectue une vibration dans la manette.
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
        }
    }

    public void GamepadStrenght(InputAction.CallbackContext context)
    {
        PivotValue = context.ReadValue<Vector2>();

        GamepadThrowStrenght = context.ReadValue<Vector2>().magnitude * StrenghtMultiplier;

        if (context.canceled)
        {
            PivotValue = new Vector2(0, 0);
            GamepadThrowStrenght = 0;
        }
    }

    public bool dragEnabled = false;
    public void MouseStrenght(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            MouseEnd = context.ReadValue<Vector2>();
            MouseThrowStrenght = Vector2.Distance(MouseStart, MouseEnd) / 5;
            MouseThrowStrenght = Mathf.Clamp(MouseThrowStrenght, 0, StrenghtMultiplier);
        }
    }

    public void MouseStartDrag(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            dragEnabled = true;
        }

        if (context.canceled && MouseEnd != Vector2.zero)
        {
            Cursor.lockState = CursorLockMode.Confined;
            dragEnabled = false;

            isShooted = true;

            posBeforeHit = transform.position;
            Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.AddForce(-forceDirection * MouseThrowStrenght, ForceMode.Impulse);

            smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //smokePoof.SetFloat("SmokeSize", MouseThrowStrenght / StrenghtMultiplier);

            smokePoof.Play();

            var emissionSpeedEffect = speedEffect.emission;
            emissionSpeedEffect.rateOverTime = GamepadThrowStrenght / StrenghtMultiplier * 200f;

            var durationSpeedEffect = speedEffect.main;
            durationSpeedEffect.duration = GamepadThrowStrenght / StrenghtMultiplier;


            speedEffectDirection.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            speedEffect.Play();

            turnBasedPlayer.ShotCount();

            MouseThrowStrenght = 0;
            MouseEnd = Vector2.zero;
        }
    }

    public void MouseCancelThrow(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            dragEnabled = false;

            MouseThrowStrenght = 0;
            MouseEnd = Vector2.zero;
        }
    }

    public void MouseThrow(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            dragEnabled = false;

            isShooted = true;

            
            posBeforeHit = transform.position;
            Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.AddForce(-forceDirection * MouseThrowStrenght, ForceMode.Impulse);

            turnBasedPlayer.ShotCount();

            MouseThrowStrenght = 0;
            MouseEnd = Vector2.zero;
        }
    }
}