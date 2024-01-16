using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class NoClip : MonoBehaviour
{
    public InputActionAsset ActionAsset;
    public int Speed;

    public bool ModeOn;

    PlayerController playerController;
    Rigidbody playerRb;
    SphereCollider playerCollider;

    InputAction movePlayer;

    // Start is called before the first frame update
    void Start()
    {
        ModeOn = false;

        InputActionMap ActionMap = ActionAsset.FindActionMap("Cheat");

        ActionMap.FindAction("No-Clip").performed += NoClipMode;

        playerController = FindAnyObjectByType<PlayerController>();

        playerRb = playerController.GetComponent<Rigidbody>();
        movePlayer = ActionMap.FindAction("No-Clip Control");

        playerCollider = playerController.GetComponent<SphereCollider>();
    }

    private void NoClipMode(InputAction.CallbackContext context)
    {
        if (!ModeOn)
        {
            ModeOn = true;
            ActionAsset.FindActionMap("Gamepad").Disable();
            playerCollider.enabled = false;
        }
        else
        {
            ModeOn = false;
            ActionAsset.FindActionMap("Gamepad").Enable();
            playerCollider.enabled = true;
        }
    }

    private void Update()
    {
        if (ModeOn)
        {
            Vector3 direction = new Vector3(movePlayer.ReadValue<Vector2>().x, 0, movePlayer.ReadValue<Vector2>().y);
            playerRb.velocity = direction * Speed;
        }
    }
}
