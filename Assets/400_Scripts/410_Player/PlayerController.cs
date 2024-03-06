using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    [Header("Strenght Value")]
    public int StrengthMultiplier = 40;
    public float ThrowStrength;
    public Vector2 LookingDirection;

    [Header("Mouse Values")]
    private Vector2 MouseStart;
    private Vector2 MouseEnd;

    [Header("References")]
    public LineRenderer PowerLineRenderer;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        turnBasedPlayer = GetComponent<TurnBasedPlayer>();
    }

    void Start()
    {
        smokePoof = GetComponentInChildren<VisualEffect>();
        speedEffect = GetComponentInChildren<ParticleSystem>();

        MouseStart = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    /// <summary>
    /// Applique une force � notre bille vers son forward ainsi que les VFX la concernant
    /// </summary>
    /// <param name="context"></param>
    public void Throw(InputAction.CallbackContext context)
    {
        if (ThrowStrength > 0.1f)
        {
            isShooted = true;
            posBeforeHit = transform.position;

            //Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.AddForce(transform.forward * ThrowStrength, ForceMode.Impulse);

            smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            smokePoof.SetFloat("SmokeSize", ThrowStrength / StrengthMultiplier);
            smokePoof.Play();

            var emissionSpeedEffect = speedEffect.emission;
            emissionSpeedEffect.rateOverTime = ThrowStrength / StrengthMultiplier * 200f;

            speedEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            var durationSpeedEffect = speedEffect.main;
            durationSpeedEffect.duration = ThrowStrength / StrengthMultiplier;

            speedEffectDirection.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            speedEffect.Play();

            turnBasedPlayer.ShotCount();
        }
    }

    Vector3 lastVel;
    private void Update()
    {
        lastVel = rb.velocity;
    }

    PhysicMaterial pm;
    private void OnCollisionEnter(Collision collision)
    {
        pm = collision.collider.material;
        Debug.Log(rb.velocity.magnitude);

        Vector3 reflect = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
        Quaternion newRot = Quaternion.LookRotation(reflect);
        rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);

        StartCoroutine(Haptic(0f, 1f, .2f));

        switch (pm.name)
        {
            case "Bumper (Instance)":
                if (rb.velocity.magnitude > 20f)
                    rb.AddForce(lastVel.normalized * 10f, ForceMode.Impulse);
                break;

            //Ajouter d'autres exceptions si n�cessaire
        }

        //if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        //{
        //    float speed = lastVel.magnitude;
        //    Vector3 reflect = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
        //    Quaternion newRot = Quaternion.LookRotation(reflect);
        //    
        //    rb.rotation = Quaternion.Euler(0f, newRot.eulerAngles.y, 0f);
        //    switch (obstacle.obstacleType)
        //    {
        //        case Obstacle.ObstacleType.Concrete:
        //            rb.velocity = reflect * Mathf.Max(speed * ConcreteBounce, 0f);
        //            StartCoroutine(Haptic(0f, 1f, .2f));
        //            break;
        //    
        //        case Obstacle.ObstacleType.Rubber:
        //            StartCoroutine(Haptic(0f, 1f, .2f));
        //            rb.velocity = reflect * Mathf.Max(speed * RubberBounce, 0f);
        //            break;
        //    
        //        case Obstacle.ObstacleType.Felt:
        //            StartCoroutine(Haptic(0f, 1f, .2f));
        //            rb.velocity = reflect * Mathf.Max(speed * FeltBounce, 0f);
        //            break;
        //    
        //        case Obstacle.ObstacleType.NPC:
        //            StartCoroutine(Haptic(0f, 1f, .2f));
        //            break;
        //    }
        //
        //}
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
    /// Met en place un vecteur en fonction de l� o� le joueur regarde et oriente la bille dans la direction du vecteur
    /// Affiche la jauge de puissance en fonction du vecteur, de la puissance de lancer et divis� par 10 pour un meilleur rendu 
    /// </summary>
    /// <param name="_lookDirection"></param>
    private void SetLookDirection(Vector2 _lookDirection)
    {
        LookingDirection = _lookDirection;
        if (ThrowStrength > 0f)
        {
            angle = Mathf.Atan2(LookingDirection.x, LookingDirection.y) * Mathf.Rad2Deg;
            rb.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        PowerLineRenderer.SetPosition(1, Vector3.back * ThrowStrength / 5);
    }

    /// <summary>
    /// Quand le joystick gauche est actif alors on appelle la m�thode SetLookDirection avec son vecteur qui va dans la direction oppos�e
    /// </summary>
    /// <param name="context"></param>
    public void GamepadStrenght(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetLookDirection(-context.ReadValue<Vector2>());
            ThrowStrength = context.ReadValue<Vector2>().magnitude * StrengthMultiplier;
            
        }

        if (context.canceled)
        {
            ThrowStrength = 0;
            SetLookDirection(Vector2.zero);
        }
    }

    private bool dragEnabled = false;
    /// <summary>
    /// Quand la souris effectue un drag on rend le curseur invisible et il est restreint de se d�plac� dans l'�cran
    /// On appelle la m�thode SetLookDirection avec son vecteur qui va dans la direction oppos�e
    /// <param name="context"></param>
    public void MouseStrenght(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            MouseEnd = context.ReadValue<Vector2>();
            ThrowStrength = Vector2.Distance(MouseStart, MouseEnd);
            ThrowStrength = Mathf.Clamp(ThrowStrength, 0, StrengthMultiplier);

            // Set a better magnitude for the direction here
            SetLookDirection(-(context.ReadValue<Vector2>() - MouseStart).normalized); 
            //LookingDirection = -(context.ReadValue<Vector2>() - MouseStart).normalized;
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

        if (context.canceled && MouseEnd != Vector2.zero)
        {
            speedEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            Cursor.lockState = CursorLockMode.Confined;
            dragEnabled = false;

            isShooted = true;

            posBeforeHit = transform.position;
            Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.AddForce(-forceDirection * ThrowStrenght, ForceMode.Impulse);

            smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //smokePoof.SetFloat("SmokeSize", ThrowStrenght / StrenghtMultiplier);

            smokePoof.Play();

            var emissionSpeedEffect = speedEffect.emission;
            emissionSpeedEffect.rateOverTime = ThrowStrenght / StrenghtMultiplier * 200f;

            var durationSpeedEffect = speedEffect.main;
            durationSpeedEffect.duration = ThrowStrenght / StrenghtMultiplier;


            speedEffectDirection.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            speedEffect.Play();

            turnBasedPlayer.ShotCount();

            ThrowStrenght = 0;
            MouseEnd = Vector2.zero;
        }
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
            SetLookDirection(Vector2.zero);
        }
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
            SetLookDirection(Vector2.zero);
        }
    }
}