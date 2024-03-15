using UnityEngine;
using UnityEngine.InputSystem;

public class NoClip : MonoBehaviour
{
    public int Speed;

    public bool ModeOn;

    PlayerController playerController;
    Rigidbody playerRb;
    public SphereCollider PlayerCollider;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        ModeOn = false;

        playerController = FindAnyObjectByType<PlayerController>();

        playerRb = playerController.GetComponent<Rigidbody>();

        PlayerCollider = playerController.GetComponent<SphereCollider>();

        InputManager.NoClipEnable(this);
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        if (ModeOn)
        {
            direction = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);

            if (context.canceled)
                playerRb.velocity = Vector3.zero;
        }
    }

    public void NoClipMode(InputAction.CallbackContext context)
    {
        if (ModeOn)
        {
            ModeOn = true;
            InputManager.Actions.Gamepad.Disable();
            InputManager.Actions.MouseKeyboard.Disable();
            PlayerCollider.enabled = false;
        }
        else
        {
            ModeOn = false;
            InputManager.Actions.Gamepad.Enable();
            InputManager.Actions.MouseKeyboard.Enable();
            PlayerCollider.enabled = true;
        }
    }

    private void Update()
    {
        if (ModeOn)
            playerRb.velocity = direction * Speed;
    }

    private void OnDisable()
    {
        InputManager.NoClipDisable(this);
    }
}
