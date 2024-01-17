using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeCam : MonoBehaviour
{
    [Tooltip("Distance � laquelle la cam�ra peut aller.\nValeur recommand�e : 2")]
    public float ClampToPlayer;

    CinemachineFramingTransposer camFT;
    void Awake()
    {
        camFT = GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void FreeCam(InputAction.CallbackContext context)
    {
        camFT.m_ScreenX = -context.ReadValue<Vector2>().x / ClampToPlayer + 0.5f;
        camFT.m_ScreenY = context.ReadValue<Vector2>().y / ClampToPlayer + 0.5f;
    }
}
