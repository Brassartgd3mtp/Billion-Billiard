using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class NoClip : MonoBehaviour
{
    public int Speed;

    public bool ModeOn;

    PlayerController pc;
    Rigidbody playerRb;
    public SphereCollider PlayerCollider;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        ModeOn = false;

        pc = FindAnyObjectByType<PlayerController>();

        playerRb = pc.GetComponent<Rigidbody>();

        PlayerCollider = pc.GetComponent<SphereCollider>();
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

    private void Update()
    {
        if (ModeOn)
            playerRb.velocity = direction * Speed;
    }
}
