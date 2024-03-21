using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerControllerd : MonoBehaviour
{
    [Header("Strenght Value")]
    public int StrenghtMultiplier = 40;

    [Space]
    public float ThrowStrenght;

    private Vector2 PivotValue;
    private Vector2 MouseStart;
    private Vector2 MouseEnd;

    [Header("References")]
    public GameObject Pivot;
    public WallValues BounceValues;

    private Vector3 strenghtToScale;
    private Quaternion pivotToRotation;
    private float angle;

    public static Rigidbody rb;
    private Vector3 lastVel;

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
        rb.AddForce(-forceDirection * ThrowStrenght, ForceMode.Impulse);

        smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        smokePoof.SetFloat("SmokeSize", ThrowStrenght / StrenghtMultiplier);
        smokePoof.Play();

        var emissionSpeedEffect = speedEffect.emission;
        emissionSpeedEffect.rateOverTime = ThrowStrenght / StrenghtMultiplier * 200f;


        speedEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var durationSpeedEffect = speedEffect.main;
        durationSpeedEffect.duration = ThrowStrenght / StrenghtMultiplier;

        speedEffectDirection.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        speedEffect.Play();

        turnBasedPlayer.ShotCount();
    }

    // Update is called once per frame
    void Update()
    {
        angle =
            SwapControls.state == CurrentState.Gamepad ?
            Mathf.Atan2(PivotValue.x, PivotValue.y) * Mathf.Rad2Deg :
            Mathf.Atan2(MouseEnd.x - MouseStart.x, MouseEnd.y - MouseStart.y) * Mathf.Rad2Deg;

        Pivot.transform.rotation = pivotToRotation;
        Pivot.transform.rotation = Quaternion.Euler(0, angle, 0);

        strenghtToScale.z = ThrowStrenght / (StrenghtMultiplier / 5);

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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
    //    {
    //        float speed = lastVel.magnitude;
    //        Vector3 reflect = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
    //
    //        switch (obstacle.obstacleType)
    //        {
    //            case Obstacle.ObstacleType.Concrete:
    //                rb.velocity = reflect * Mathf.Max(speed * BounceValues.Concrete, 0f);
    //                StartCoroutine(Haptic(0f, 1f, .2f));
    //                break;
    //
    //            case Obstacle.ObstacleType.Rubber:
    //                StartCoroutine(Haptic(0f, 1f, .2f));
    //                rb.velocity = reflect * Mathf.Max(speed * BounceValues.Rubber, 0f);
    //                break;
    //
    //            case Obstacle.ObstacleType.Felt:
    //                StartCoroutine(Haptic(0f, 1f, .2f));
    //                rb.velocity = reflect * Mathf.Max(speed * BounceValues.Felt, 0f);
    //                break;
    //
    //            case Obstacle.ObstacleType.NPC:
    //                StartCoroutine(Haptic(0f, 1f, .2f));
    //                break;
    //        }
    //    }
    //}

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

    public void GamepadStrenght(InputAction.CallbackContext context)
    {
        PivotValue = context.ReadValue<Vector2>();

        ThrowStrenght = context.ReadValue<Vector2>().magnitude * StrenghtMultiplier;

        if (context.canceled)
        {
            PivotValue = new Vector2(0, 0);
            ThrowStrenght = 0;
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
            ThrowStrenght = Vector2.Distance(MouseStart, MouseEnd);
            ThrowStrenght = Mathf.Clamp(ThrowStrenght, 0, StrenghtMultiplier);
        }
    }

    public void MouseStartDrag(InputAction.CallbackContext context)
    {
        if (context.started)
            Cursor.lockState = CursorLockMode.Locked;

        if (context.performed)
            dragEnabled = true;

        if (context.canceled && MouseEnd != Vector2.zero)
        {
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.lockState = CursorLockMode.Confined;
            dragEnabled = false;

            isShooted = true;

            posBeforeHit = transform.position;
            Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.AddForce(-forceDirection * ThrowStrenght, ForceMode.Impulse);

            smokePoof.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //smokePoof.SetFloat("SmokeSize", ThrowStrenght / StrenghtMultiplier);

            smokePoof.Play();

            if (!speedEffect.isPlaying)
            {
                var emissionSpeedEffect = speedEffect.emission;
                emissionSpeedEffect.rateOverTime = ThrowStrenght / StrenghtMultiplier * 200f;

                var durationSpeedEffect = speedEffect.main;
                durationSpeedEffect.duration = ThrowStrenght / StrenghtMultiplier;

                speedEffectDirection.transform.rotation = Quaternion.Euler(0f, angle, 0f);
                speedEffect.Play();
            }
            else
                speedEffect.Stop();

            turnBasedPlayer.ShotCount();

            ThrowStrenght = 0;
            MouseEnd = Vector2.zero;
        }
    }

    public void MouseCancelThrow(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            dragEnabled = false;

            ThrowStrenght = 0;
            MouseEnd = Vector2.zero;
        }
    }

    public void MouseThrow(InputAction.CallbackContext context)
    {
        if (dragEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            dragEnabled = false;
            
            posBeforeHit = transform.position;
            Vector3 forceDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.AddForce(-forceDirection * ThrowStrenght, ForceMode.Impulse);

            turnBasedPlayer.ShotCount();

            ThrowStrenght = 0;
            MouseEnd = Vector2.zero;
        }
    }
}