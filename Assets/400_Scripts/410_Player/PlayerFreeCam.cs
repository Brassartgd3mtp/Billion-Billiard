using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeCam : MonoBehaviour
{
    [Tooltip("Distance � laquelle la cam�ra peut aller.\nValeur recommand�e : 2")]
    public float ClampToPlayer;

    PlayerController pc;
    InputAction FreeCam;
    CinemachineFramingTransposer camFT;

    // Start is called before the first frame update
    void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
        camFT = GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();

        FreeCam = pc.ActionAsset.FindActionMap("Gamepad").FindAction("FreeCam");
    }

    // Update is called once per frame
    void Update()
    {
        camFT.m_ScreenX = -FreeCam.ReadValue<Vector2>().x / ClampToPlayer + 0.5f;
        camFT.m_ScreenY = FreeCam.ReadValue<Vector2>().y / ClampToPlayer + 0.5f;
        Debug.Log(FreeCam.ReadValue<Vector2>());
    }
}
