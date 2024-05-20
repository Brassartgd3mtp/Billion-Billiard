using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.UI;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting;

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
    public float MouseSensitivity;
    [HideInInspector] public Vector2 MouseStart;
    private Vector2 MouseEnd;

    [Header("Gamepad Values"), Range(0f, 2f)]
    [SerializeField] private float gaugeSpeed;
    [SerializeField] private float deadZone;
    [SerializeField] private float currentJoystickPos;

    [Header("Ice Bounce Angle"), Range(0f, 180f)]
    [SerializeField] float iceAngle = 45f;

    [Header("References")]
    public LineRenderer PowerLineRenderer;
    public LineRenderer PowerLineRendererOutline;
    [SerializeField] private GameObject gaugeObject;
    [SerializeField] private Image gaugeFill;
    [SerializeField] private UI_ShotRemaining shotRemaining;
    [SerializeField] private TrajectoryPrediction trajectoryPrediction;

    private float angle;
    public static Rigidbody rb;

    [Space(20)]
    public bool isShooted;

    public static Vector3 posBeforeHit;

    private TurnBasedPlayer turnBasedPlayer;

    [Header("FX Parameter")]
    [SerializeField] private ParticleSystem speedEffect;
    [SerializeField] private ParticleSystem speedLineEffect;
    [SerializeField] private GameObject speedEffectDirection;
    [SerializeField] private VisualEffect smokePoof;
    AudioSource audioSource;

    [Header("Other")]
    public PlayerParameters playerParameters;
    [SerializeField] private Animator MyAnimator;
    public static PlayerController Instance;
    float timeSinceThrow = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        rb = GetComponent<Rigidbody>();

        turnBasedPlayer = GetComponent<TurnBasedPlayer>();

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        smokePoof = GetComponentInChildren<VisualEffect>();
        speedEffect = GetComponentInChildren<ParticleSystem>();
        MyAnimator = GetComponentInChildren<Animator>();

        MouseStart = new Vector2(Screen.width / 2, Screen.height / 2);

        InputHandler.PlayerControllerEnable(this);
        playerParameters = GetComponent<PlayerParameters>();

        MouseSensitivity = PlayerOptionsRuntimeSave.MouseSensitivity;
    }

    /// <summary>
    /// Applique une force � notre bille vers son forward ainsi que les VFX la concernant
    /// </summary>
    /// <param name="context"></param>
    public void Throw(InputAction.CallbackContext context)
    {
        if (ThrowStrength > 0.2f)
        {
            if (SwapControls.state == CurrentState.MouseKeyboard)
            {
                DoThrow();
            }
            else if (currentJoystickPos > deadZone)
            {
                DoThrow();
            }
            else
            {
                ThrowStrength = 0;
            }
        }
    }

    void DoThrow()
    {
        ScreenShake.instance.Shake(ThrowStrength / StrengthFactor);

        rb.drag = 1;

        SoundShot();
        StartCoroutine(Haptic(ThrowStrength / StrengthFactor, ThrowStrength / StrengthFactor, .4f));

        timeSinceThrow = 0;
        staticThrowStrength = ThrowStrength;

        isShooted = true;
        RespawnPlayer();

        rb.AddForce(trajectoryPrediction.transform.forward * ThrowStrength, ForceMode.Impulse);
        rb.rotation = trajectoryPrediction.transform.rotation;

        smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        smokePoof.SetFloat("SmokeSize", ThrowStrength / StrengthFactor);
        smokePoof.Play();

        var emissionSpeedEffect = speedEffect.emission;
        emissionSpeedEffect.rateOverTime = ThrowStrength / StrengthFactor * 200f;

        speedEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var durationSpeedEffect = speedEffect.main;
        durationSpeedEffect.duration = ThrowStrength / StrengthFactor;

        speedEffect.Play();

        turnBasedPlayer.ShotCount();

        ThrowStrength = 0;
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

            default:
                Debug.Log("Collision is either Ice or not recognized.");
                break;
        }
    }

    [HideInInspector] public bool iceLock = false;
    float iceAngleDynamic;
    private bool isColliding = false;
    public bool IsColliding
    {
        get { return isColliding; }
        private set
        {
            isColliding = value;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            IsColliding = true;

            currentCollision = collision.collider;
            float speed = lastVel.magnitude;
            Vector3 reflect = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
            Quaternion newRot = Quaternion.LookRotation(reflect);

            if (obstacle.obstacleType == Obstacle.ObstacleType.Ice)
            {
                //Vector3 ortho = new Vector3(1, 0, 1);
                //
                //Vector3 projection = Vector3.Dot(normale, ortho) * ortho;
                //Vector3 contact2 = normale - projection;

                Vector3 normale = collision.contacts[0].normal;
                iceAngleDynamic = Vector3.SignedAngle(transform.forward, normale, Vector3.up);
                iceAngleDynamic = Mathf.Abs(iceAngleDynamic);
                //iceAngleDynamic = iceAngleDynamic > 90 ? 180 - iceAngleDynamic : iceAngleDynamic;

                if (iceAngleDynamic > iceAngle && !iceLock)
                {
                    //Debug.Log(iceAngleDynamic);
                    //Debug.Break();
                    StartCoroutine(Haptic(WallValues.IceLFH, WallValues.IceHFH, WallValues.IceTH));
                    rb.velocity = reflect * Mathf.Max(speed * WallValues.IceBounce, 0f);
                }
            }

            rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);

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
            Quaternion newRot;
            Vector3 contactPoint = collision.contacts[0].normal;

            if ((rb.velocity.x < .5f || rb.velocity.z < .5f) && timeSinceThrow < .1f && !stayOnce)
            {
                stayOnce = true;

                lastVel = transform.forward * staticThrowStrength / rb.mass;

                speed = lastVel.magnitude - timeSinceThrow;
                reflect = Vector3.Reflect(lastVel.normalized, contactPoint);
                newRot = Quaternion.LookRotation(reflect);

                if (timeSinceThrow != 0)
                    rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);

                SwitchObstacle(obstacle, speed, reflect);
            }

            if (obstacle.obstacleType == Obstacle.ObstacleType.Ice)
            {
                if (iceAngleDynamic >= iceAngle)
                {
                    rb.drag = .2f;
                    iceLock = true;
                }
            }
            else if (obstacle.obstacleType == Obstacle.ObstacleType.IceAngle)
            {
                rb.drag = 0;
                iceLock = true;
            }
        }
    }

    private Collider currentCollision = null;
    private void OnCollisionExit(Collision collision)
    {
        stayOnce = false;
        velcroLock = false;
        iceLock = false;

        if (currentCollision == collision.collider)
        {
            IsColliding = false;
            currentCollision = null;
        }

        rb.drag = 1;
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

        angle = SwapControls.state == CurrentState.Gamepad
            ? Mathf.Atan2(LookingDirection.x, LookingDirection.y) * Mathf.Rad2Deg
            : Mathf.Atan2(-LookingDirection.x, -LookingDirection.y) * Mathf.Rad2Deg;

        if (rb.velocity == Vector3.zero)
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
            currentJoystickPos = Mathf.Abs(context.ReadValue<Vector2>().x) + Mathf.Abs(context.ReadValue<Vector2>().y);
            SetLookDirection(context.ReadValue<Vector2>());
        }
        if (context.canceled)
        {
            currentJoystickPos = Mathf.Abs(context.ReadValue<Vector2>().x) + Mathf.Abs(context.ReadValue<Vector2>().y);
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
            SoundGauge();
        }
        if (context.canceled)
        {
            gaugeObject.SetActive(false);
            isGaugeActive = false;
            gaugeFill.fillAmount = 0;
            audioSource.Stop();

        }
    }

    private void Update()
    {
        gaugeTime += Time.deltaTime * gaugeSpeed * 100;

        if (isGaugeActive)
        {
            ThrowStrength = Mathf.PingPong(gaugeTime, 40);
            gaugeFill.fillAmount = ThrowStrength / StrengthFactor;
            audioSource.pitch = 1 + ThrowStrength / 30;
        }
        else
            gaugeTime = 0;

        PowerLineRenderer.SetPosition(1, Vector3.back * ThrowStrength / 8);
        PowerLineRendererOutline.SetPosition(1, Vector3.back * ThrowStrength / 7.9f);


        if (rb.velocity.magnitude < .7f)
        {
            MyAnimator.SetBool("Player_Roll", false);
        }
        else
        {
            MyAnimator.SetBool("Player_Roll", true);
        }

        if (ThrowStrength == 0f)
        {
            MyAnimator.SetBool("PreparationShoot_1", false);
            MyAnimator.SetBool("PreparationShoot_2", false);
        }
        else
        {
            MyAnimator.SetBool("PreparationShoot_1", false);
            MyAnimator.SetBool("PreparationShoot_2", true);
        }

        if (playerParameters.speed > 20)
        {
            var emissionSpeedLineEffect = speedLineEffect.emission;
            emissionSpeedLineEffect.rateOverTime = playerParameters.speed * 4f;

            if (!speedLineEffect.isPlaying)
            {
                speedLineEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                var durationSpeedLineEffect = speedLineEffect.main;
                durationSpeedLineEffect.duration = playerParameters.speed;

                speedLineEffect.Play();
            }
        }
        else if (playerParameters.speed <= 15 && speedLineEffect.isPlaying)
        {
            speedLineEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }


    [HideInInspector] public bool dragEnabled = false;
    /// <summary>
    /// Quand la souris effectue un drag on rend le curseur invisible et il est restreint de se d�placer dans l'�cran
    /// On appelle la m�thode SetLookDirection avec son vecteur qui va dans la direction oppos�e
    /// <param name="context"></param>
    public void MouseStrenght(InputAction.CallbackContext context)
    {
        float dynamicMouseSensitivity = (.01f + ThrowStrength) / 5 * MouseSensitivity;

        dynamicMouseSensitivity = Mathf.Min(dynamicMouseSensitivity, MouseSensitivity);

        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            MouseEnd = context.ReadValue<Vector2>();
            ThrowStrength = Vector2.Distance(MouseStart, MouseEnd) * dynamicMouseSensitivity;
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
        audioSource.Stop();
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

            Cursor.lockState = CursorLockMode.Confined;

            dragEnabled = false;

            Throw(context);

            ThrowStrength = 0;
            MouseEnd = Vector2.zero;
            //SetLookDirection(Vector2.zero);
        }
    }

    private void RespawnPlayer()
    {
        if (playerParameters.speed == 0)
        {
            posBeforeHit = transform.position;
        }
    }

    private void OnDisable()
    {
        InputHandler.PlayerControllerDisable();
    }
    private void SoundGauge()
    {
        audioSource.loop = true;
        AudioManager.Instance.PlaySoundLoop(18, audioSource);
    }
    private void SoundShot()
    {
        audioSource.loop = false;
        audioSource.Stop();
        audioSource.pitch = 1;
        AudioManager.Instance.PlaySound(16, audioSource);
        audioSource.loop = true;
    }
}