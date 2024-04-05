using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Strenght Value")]
    public int StrengthFactor = 40;
    [SerializeField, Tooltip("Vélocité maximale de la balle.\nValeur par défaut : 80.")]
    private float maximumVelocity = 80;
    [Range(0f, 40f)]
    public float ThrowStrength;
    [HideInInspector] public Vector2 LookingDirection;
    private float staticThrowStrength;

    [Header("Mouse Values"), Range(0f, 1f)]
    [SerializeField] private float MouseSensitivity;
    private Vector2 MouseStart;
    private Vector2 MouseEnd;

    [Header("Gamepad Values"), Range(0f, 2f)]
    [SerializeField] private float gaugeSpeed;

    [Header("References")]
    public LineRenderer PowerLineRenderer;
    [SerializeField] private GameObject gaugeObject;
    [SerializeField] private Image gaugeFill;

    private float angle;
    public static Rigidbody rb;

    [Space(20)]
    public bool isShooted;

    public Vector3 posBeforeHit;

    private TurnBasedPlayer turnBasedPlayer;

    [Header("VFX Parameter")]
    [SerializeField] private ParticleSystem speedEffect;
    [SerializeField] private GameObject speedEffectDirection;
    [SerializeField] private VisualEffect smokePoof;
    [SerializeField] private Animator MyAnimator;

    float timeSinceThrow = 0;

    public string Player_Shot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        turnBasedPlayer = GetComponent<TurnBasedPlayer>();
    }

    void Start()
    {
        smokePoof = GetComponentInChildren<VisualEffect>();
        speedEffect = GetComponentInChildren<ParticleSystem>();
        MyAnimator = GetComponentInChildren<Animator>();

        MouseStart = new Vector2(Screen.width / 2, Screen.height / 2);

        InputHandler.PlayerControllerEnable(this);
    }

    /// <summary>
    /// Applique une force � notre bille vers son forward ainsi que les VFX la concernant
    /// </summary>
    /// <param name="context"></param>
    public void Throw(InputAction.CallbackContext context)
    {
        if (ThrowStrength > 0.2f)
        {
            StartCoroutine(Haptic(ThrowStrength / 40, ThrowStrength / 40, .4f));

            timeSinceThrow = 0;
            staticThrowStrength = ThrowStrength;

            isShooted = true;
            posBeforeHit = transform.position;

            rb.AddForce(transform.forward * ThrowStrength, ForceMode.Impulse);

            smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            smokePoof.SetFloat("SmokeSize", ThrowStrength / StrengthFactor);
            smokePoof.Play();

            var emissionSpeedEffect = speedEffect.emission;
            emissionSpeedEffect.rateOverTime = ThrowStrength / StrengthFactor * 200f;

            speedEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            var durationSpeedEffect = speedEffect.main;
            durationSpeedEffect.duration = ThrowStrength / StrengthFactor;

            speedEffectDirection.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            speedEffect.Play();

            AudioManager2.Instance.PlaySDFX(Player_Shot);

            turnBasedPlayer.ShotCount();

            ThrowStrength = 0;
        }
    }

    Vector3 lastVel;
    private void FixedUpdate()
    {
        lastVel = rb.velocity;

        if (lastVel.magnitude > 0)
            timeSinceThrow += Time.fixedDeltaTime;

        if (rb.velocity.magnitude > 0 && rb.velocity.magnitude < .1f)
        {
            rb.velocity = Vector3.zero;
        }

        //Clamp Speed
        rb.velocity =
            rb.velocity.magnitude < maximumVelocity ?
            rb.velocity :
            rb.velocity.normalized * maximumVelocity;
    }

    bool velcroLock = false;
    void SwitchObstacle(Obstacle obstacle, float speed, Vector3 reflect)
    {
        switch (obstacle.obstacleType)
        {
            case Obstacle.ObstacleType.Concrete:
                StartCoroutine(Haptic(WallValues.ConcreteLFH, WallValues.ConcreteHFH, WallValues.ConcreteTH));
                rb.velocity = reflect * Mathf.Max(speed * WallValues.ConcreteBounce, 0f);
                break;

            case Obstacle.ObstacleType.Rubber:
                StartCoroutine(Haptic(WallValues.RubberLFH, WallValues.RubberHFH, WallValues.RubberTH));
                rb.velocity = reflect * Mathf.Max(speed * WallValues.RubberBounce, 0f);
                break;

            case Obstacle.ObstacleType.Felt:
                StartCoroutine(Haptic(WallValues.FeltLFH, WallValues.FeltHFH, WallValues.FeltTH));
                rb.velocity = reflect * Mathf.Max(speed * WallValues.FeltBounce, 0f);
                break;

            case Obstacle.ObstacleType.NPC:
                StartCoroutine(Haptic(WallValues.PawnLFH, WallValues.PawnHFH, WallValues.PawnTH));
                break;

            case Obstacle.ObstacleType.Bumper:
                StartCoroutine(Haptic(WallValues.BumperLFH, WallValues.BumperHFH, WallValues.BumperTH));
                rb.velocity = reflect * Mathf.Max(speed * WallValues.BumperBounce, 0f);
                break;

            case Obstacle.ObstacleType.Velcro:
                if (!velcroLock)
                {
                    velcroLock = true;
                    StartCoroutine(Haptic(WallValues.VelcroLFH, WallValues.VelcroHFH, WallValues.VelcroTH));
                    rb.velocity = Vector3.zero;
                }
                break;
        }
    }

    //PhysicMaterial pm;
    private void OnCollisionEnter(Collision collision)
    {
        //pm = collision.collider.material;
        //
        //Vector3 reflect = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
        //Quaternion newRot = Quaternion.LookRotation(reflect);
        //rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);

        //StartCoroutine(Haptic(0f, 1f, .2f));

        //switch (pm.name)
        //{
        //    case "Bumper (Instance)":
        //        if (rb.velocity.magnitude > 20f)
        //            rb.AddForce(lastVel.normalized * 10f, ForceMode.Impulse);
        //        break;
        //
        //    //Ajouter d'autres exceptions si n�cessaire
        //}

        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            float speed = lastVel.magnitude;
            Vector3 reflect = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
            //Quaternion newRot = Quaternion.LookRotation(reflect);
            //
            //rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);

            SwitchObstacle(obstacle, speed, reflect);
        }
    }

    bool stayOnce = false;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            float speed;
            Vector3 reflect;
            //Quaternion newRot;
            Vector3 contactPoint = collision.contacts[0].normal;

            if ((rb.velocity.x < .5f || rb.velocity.z < .5f) && timeSinceThrow < .1f && !stayOnce)
            {
                stayOnce = true;

                lastVel = transform.forward * staticThrowStrength / rb.mass;

                speed = lastVel.magnitude - timeSinceThrow;
                reflect = Vector3.Reflect(lastVel.normalized, contactPoint);
                //newRot = Quaternion.LookRotation(reflect);

                //if (timeSinceThrow != 0)
                //    rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);

                SwitchObstacle(obstacle, speed, reflect);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        stayOnce = false;
        velcroLock = false;
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
        if (SwapControls.state == CurrentState.Gamepad)
        {
            Gamepad.current.SetMotorSpeeds(lowfreq_strenght, highfreq_strenght);
            yield return new WaitForSeconds(timer);
            InputSystem.ResetHaptics();
        }
    }

    /// <summary>
    /// Met en place un vecteur en fonction de l� o� le joueur regarde et oriente la bille dans la direction du vecteur
    /// Affiche la jauge de puissance en fonction du vecteur, de la puissance de lancer et divis� par 10 pour un meilleur rendu 
    /// </summary>
    /// <param name="_lookDirection"></param>
    private void SetLookDirection(Vector2 _lookDirection)
    {
        LookingDirection = _lookDirection;

        angle = Mathf.Atan2(-LookingDirection.x, -LookingDirection.y) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    /// <summary>
    /// Quand le joystick gauche est actif alors on appelle la m�thode SetLookDirection avec son vecteur qui va dans la direction oppos�e
    /// </summary>
    /// <param name="context"></param>
    public void GamepadDirection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //SetLookDirection(-context.ReadValue<Vector2>());
            SetLookDirection(context.ReadValue<Vector2>());
        }
    }

    float gaugeTime;
    bool isGaugeActive = false;
    public void GamepadStrengthGauge(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            gaugeObject.SetActive(true);
            isGaugeActive = true;
            //MyAnimator.SetBool("PreparationShoot", true);
        }
        if (context.canceled)
        {
            gaugeObject.SetActive(false);
            isGaugeActive = false;
            gaugeFill.fillAmount = 0;
            //MyAnimator.SetBool("PreparationShoot", false);

        }
    }

    private void Update()
    {
        gaugeTime += Time.deltaTime * gaugeSpeed * 100;

        if (isGaugeActive)
        {
            ThrowStrength = Mathf.PingPong(gaugeTime, 40);
            gaugeFill.fillAmount = ThrowStrength / StrengthFactor;
        }
        else
            gaugeTime = 0;

        //if (SwapControls.state == CurrentState.Gamepad)
        //    PowerLineRenderer.SetPosition(1, Vector3.back * 8);
        //else
            //PowerLineRenderer.SetPosition(1, Vector3.back * ThrowStrength / 5);
        PowerLineRenderer.SetPosition(1, Vector3.back * ThrowStrength / 8);
    }

    private bool dragEnabled = false;
    /// <summary>
    /// Quand la souris effectue un drag on rend le curseur invisible et il est restreint de se d�placer dans l'�cran
    /// On appelle la m�thode SetLookDirection avec son vecteur qui va dans la direction oppos�e
    /// <param name="context"></param>
    public void MouseStrenght(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            MouseEnd = context.ReadValue<Vector2>();
            ThrowStrength = Vector2.Distance(MouseStart, MouseEnd) * MouseSensitivity;
            ThrowStrength = Mathf.Clamp(ThrowStrength, 0, StrengthFactor);

            // Set a better magnitude for the direction here
            //SetLookDirection(-(context.ReadValue<Vector2>() - MouseStart).normalized);
            SetLookDirection((context.ReadValue<Vector2>() - MouseStart).normalized);
        }
    }

    /// <summary>
    /// Quand le clic gauche de la souris est enfonc� on met le curseur au centre l'�cran et on active le fait de pouvoir drag sa souris
    /// </summary>
    /// <param name="context"></param>
    public void MouseStartDrag(InputAction.CallbackContext context)
    {
        if (context.started)
            Cursor.lockState = CursorLockMode.Locked;

        if (context.performed)
            dragEnabled = true;
    }

    /// <summary>
    /// Quand le clic droit de la souris est activ� alors que le clic gauche est enfonc� on reset le curseur (visible,position,drag non actif,pas de contrainte de d�placement)
    /// On appelle la m�thode SetLookDirection pour remettre le vecteur � z�ro
    /// </summary>
    /// <param name="context"></param>
    public void MouseCancelThrow(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            dragEnabled = false;

            ThrowStrength = 0;
            MouseEnd = Vector2.zero;
        }
    }

    public void GamepadCancelThrow(InputAction.CallbackContext context)
    {
        gaugeObject.SetActive(false);
        gaugeFill.fillAmount = 0;
        isGaugeActive = false;

        ThrowStrength = 0;
    }

    /// <summary>
    /// Quand le clic gauche est lach� on reset les contraintes du curseur, on appelle la m�thode Throw et on reset le vecteur de la m�thode SetLookDirection � z�ro
    /// </summary>
    /// <param name="context"></param>
    public void MouseThrow(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.lockState = CursorLockMode.None;

            dragEnabled = false;

            Throw(context);

            ThrowStrength = 0;
            MouseEnd = Vector2.zero;
            //SetLookDirection(Vector2.zero);
        }
    }

    private void OnDisable()
    {
        InputHandler.PlayerControllerDisable();
    }
}