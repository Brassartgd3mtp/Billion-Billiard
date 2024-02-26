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
    public float ThrowStrength;
    public Vector2 LookingDirection;

    [Header("Mouse Values")]
    //public float MouseThrowStrenght;
    public Vector2 MouseStart;
    public Vector2 MouseEnd;

    [Header("References")]
    //public GameObject Pivot;
    public LineRenderer PowerLineRenderer;

    private Vector3 strenghtToScale;
    private Quaternion pivotToRotation;
    [SerializeField]private float angle;

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
        //pivotToRotation = Pivot.transform.rotation;
        //strenghtToScale = Pivot.transform.localScale;

        smokePoof = GetComponentInChildren<VisualEffect>();
        speedEffect = GetComponentInChildren<ParticleSystem>();

        MouseStart = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    /// <summary>
    /// Applique une force à notre bille vers son forward ainsi que les VFX la concernant
    /// </summary>
    /// <param name="context"></param>
    public void Throw(InputAction.CallbackContext context)
    {
        isShooted = true;

        posBeforeHit = transform.position;
        //Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        rb.AddForce(transform.forward * ThrowStrength, ForceMode.Impulse);

        smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        smokePoof.SetFloat("SmokeSize", ThrowStrength / StrenghtMultiplier);
        smokePoof.Play();

        var emissionSpeedEffect = speedEffect.emission;
        emissionSpeedEffect.rateOverTime = ThrowStrength / StrenghtMultiplier * 200f;


        speedEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var durationSpeedEffect = speedEffect.main;
        durationSpeedEffect.duration = ThrowStrength / StrenghtMultiplier;

        speedEffectDirection.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        speedEffect.Play();

        turnBasedPlayer.ShotCount();
    }

    // Update is called once per frame
    void Update()
    {      
        lastVel = rb.velocity;      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            float speed = lastVel.magnitude;
            Vector3 reflect = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
            Quaternion newRot = Quaternion.LookRotation(reflect);

            rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);
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
    /// <summary>
    /// Met en place un vecteur en fonction de là où le joueur regarde et oriente la bille dans la direction du vecteur
    /// Affiche la jauge de puissance en fonction du vecteur, de la puissance de lancer et divisé par 10 pour un meilleur rendu 
    /// </summary>
    /// <param name="_lookDirection"></param>
    private void SetLookDirection(Vector2 _lookDirection)
    {
        LookingDirection = _lookDirection;
        ThrowStrength = _lookDirection.magnitude * StrenghtMultiplier;
        if(ThrowStrength > 0f)
        {
            angle = Mathf.Atan2(LookingDirection.x, LookingDirection.y) * Mathf.Rad2Deg;
            rb.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        PowerLineRenderer.SetPosition(1, Vector3.back * ThrowStrength / 10);
    }

    /// <summary>
    /// Quand le joystick gauche est actif alors on appelle la méthode SetLookDirection avec son vecteur qui va dans la direction opposée
    /// </summary>
    /// <param name="context"></param>
    public void GamepadStrenght(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            SetLookDirection(-context.ReadValue<Vector2>());
        }

        if (context.canceled)
        {
            SetLookDirection(Vector2.zero);
        }
    }

    public bool dragEnabled = false;

    /// <summary>
    /// Quand la souris effectue un drag on rend le curseur invisible et il est restreint de se déplacé dans l'écran
    /// On appelle la méthode SetLookDirection avec son vecteur qui va dans la direction opposée
    /// <param name="context"></param>
    public void MouseStrenght(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

            // Set a better magnitude for the direction here
            SetLookDirection(-(context.ReadValue<Vector2>() - MouseStart).normalized); 
            //LookingDirection = -(context.ReadValue<Vector2>() - MouseStart).normalized;
        }
    }

    /// <summary>
    /// Quand le clic gauche de la souris est enfoncé on met le curseur au centre l'écran et on active le fait de pouvoir drag sa souris
    /// </summary>
    /// <param name="context"></param>
    public void MouseStartDrag(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            dragEnabled = true;
        }
    }

    /// <summary>
    /// Quand le clic droit de la souris est activé alors que le clic gauche est enfoncé on reset le curseur (visible,position,drag non actif,pas de contrainte de déplacement)
    /// On appelle la méthode SetLookDirection pour remettre le vecteur à zéro
    /// </summary>
    /// <param name="context"></param>
    public void MouseCancelThrow(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            dragEnabled = false;

            //MouseThrowStrenght = 0;
            MouseEnd = Vector2.zero;
            SetLookDirection(Vector2.zero);
        }
    }

    /// <summary>
    /// Quand le clic gauche est laché on reset les contraintes du curseur, on appelle la méthode Throw et on reset le vecteur de la méthode SetLookDirection à zéro
    /// </summary>
    /// <param name="context"></param>
    public void MouseThrow(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            dragEnabled = false;

            Throw(context);

            //MouseThrowStrenght = 0;
            MouseEnd = Vector2.zero;
            SetLookDirection(Vector2.zero);
        }
    }
}