using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeCam : MonoBehaviour
{
    [Tooltip("Distance à laquelle la caméra peut aller.\nValeur par défaut : 10")]
    public float CameraSpeed = 50;

    [SerializeField] CinemachineVirtualCamera camFollow;
    [SerializeField] CinemachineVirtualCamera camTranslate;

    CinemachineFramingTransposer camFT;
    CinemachineTransposer camT;

    InputAction.CallbackContext freeCam;
    bool mouseFreeCamEnabled = false;

    private void Awake()
    {
        camT = camTranslate.GetCinemachineComponent<CinemachineTransposer>();
        camFT = camTranslate.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void FreeCam(InputAction.CallbackContext context)
    {
        freeCam = context;

        if (mouseFreeCamEnabled || Gamepad.current != null)
        {
            camTranslate.gameObject.SetActive(true);
            camFollow.gameObject.SetActive(false);
        }
        
        if (context.canceled)
        {
            camT.m_FollowOffset.x = 0;
            camT.m_FollowOffset.z = -6.8f;

            camTranslate.gameObject.SetActive(false);
            camFollow.gameObject.SetActive(true);
        }
    }

    public void StartFreeCam(InputAction.CallbackContext context)
    {
        if (Gamepad.current == null)
        {
            mouseFreeCamEnabled = true;

            if (context.canceled)
            {
                //MoveTowards de retour avec départ la pos actuelle et arrivée la pos du centre
                mouseFreeCamEnabled = false;

                StartCoroutine(CameraReturn());
            }
        }
    }

    private IEnumerator CameraReturn()
    {
        while (camT.m_FollowOffset != new Vector3(0, 18.8f, -6.8f))
        {
            camT.m_FollowOffset.x = Mathf.MoveTowards(camT.m_FollowOffset.x, 0, Time.deltaTime * 300);
            camT.m_FollowOffset.z = Mathf.MoveTowards(camT.m_FollowOffset.z, -6.8f, Time.deltaTime * 300);

            yield return new WaitForSeconds(.01f);
        }

        camTranslate.gameObject.SetActive(false);
        camFollow.gameObject.SetActive(true);

        yield break;
    }

    private void Update()
    {
        if (Gamepad.current != null)
        {
            camT.m_FollowOffset.x += freeCam.ReadValue<Vector2>().x * CameraSpeed * Time.deltaTime * 5;
            camT.m_FollowOffset.z += freeCam.ReadValue<Vector2>().y * CameraSpeed * Time.deltaTime * 5;
        }
        
        else if (mouseFreeCamEnabled)
        {
            camT.m_FollowOffset.x += freeCam.ReadValue<Vector2>().x * CameraSpeed * Time.deltaTime;
            camT.m_FollowOffset.z += freeCam.ReadValue<Vector2>().y * CameraSpeed * Time.deltaTime;
        }

        camT.m_FollowOffset.x = Mathf.Clamp(camT.m_FollowOffset.x, -16, 16);
        camT.m_FollowOffset.z = Mathf.Clamp(camT.m_FollowOffset.z, -18, 2.5f);
    }
}
