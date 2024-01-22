using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeCam : MonoBehaviour
{
    [Tooltip("Distance à laquelle la caméra peut aller.\nValeur recommandée : 2")]
    public float CameraSpeed;

    [SerializeField] CinemachineVirtualCamera camFollow;
    [SerializeField] CinemachineVirtualCamera camTranslate;

    CinemachineFramingTransposer camFT;
    CinemachineTransposer camT;

    InputAction.CallbackContext ctx;
    bool isActive = false;

    private void Awake()
    {
        camT = camTranslate.GetCinemachineComponent<CinemachineTransposer>();
        camFT = camTranslate.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void FreeCam(InputAction.CallbackContext context)
    {
        if (Gamepad.current != null || isActive)
        {
            ctx = context;

            camTranslate.gameObject.SetActive(true);
            camFollow.gameObject.SetActive(false);
        }
    }

    public void CancelFreeCam(InputAction.CallbackContext context)
    {
        camTranslate.gameObject.SetActive(false);
        camFollow.gameObject.SetActive(true);
    }

    public void StartFreeCam(InputAction.CallbackContext context)
    {
        if (Gamepad.current == null)
        {
            isActive = true;

            if (context.canceled)
            {
                isActive = false;
                camTranslate.gameObject.SetActive(false);
                camFollow.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        camT.m_FollowOffset.x = Mathf.MoveTowards(camT.m_FollowOffset.x, ctx.ReadValue<Vector2>().x * 9.5f, Time.deltaTime * CameraSpeed);
        camT.m_FollowOffset.x = Mathf.Clamp(camT.m_FollowOffset.x, -6, 6);

        camT.m_FollowOffset.z = Mathf.MoveTowards(camT.m_FollowOffset.z, -7 + ctx.ReadValue<Vector2>().y * 5.2f, Time.deltaTime * CameraSpeed);
        camT.m_FollowOffset.z = Mathf.Clamp(camT.m_FollowOffset.z, -12, -2);
    }
}
