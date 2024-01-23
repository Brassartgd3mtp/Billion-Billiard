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

    InputAction.CallbackContext freeCam;
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
            freeCam = context;

            camTranslate.gameObject.SetActive(true);
            camFollow.gameObject.SetActive(false);
        }
        //else if (isActive)
        //{
        //    camT.m_FollowOffset.x = context.ReadValue<Vector2>().x;
        //    camT.m_FollowOffset.z = -6.8f + context.ReadValue<Vector2>().y;
        //
        //    camTranslate.gameObject.SetActive(true);
        //    camFollow.gameObject.SetActive(false);
        //}
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
                camT.m_FollowOffset.x = 0;
                camT.m_FollowOffset.z = -6.8f;

                isActive = false;
                camTranslate.gameObject.SetActive(false);
                camFollow.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        camT.m_FollowOffset.x = Mathf.MoveTowards(camT.m_FollowOffset.x, freeCam.ReadValue<Vector2>().x * 9.5f, Time.deltaTime * CameraSpeed);
        camT.m_FollowOffset.x = Mathf.Clamp(camT.m_FollowOffset.x, -16, 16);

        camT.m_FollowOffset.z = Mathf.MoveTowards(camT.m_FollowOffset.z, -6.8f + freeCam.ReadValue<Vector2>().y * 5.2f, Time.deltaTime * CameraSpeed);
        camT.m_FollowOffset.z = Mathf.Clamp(camT.m_FollowOffset.z, -18, 2.5f);
    }
}
