using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeCam : MonoBehaviour
{
    [Tooltip("Distance à laquelle la caméra peut aller.\nValeur par défaut : 10")]
    public float CameraSpeed = 50;

    [SerializeField] CinemachineBrain camBrain;
    [SerializeField] CinemachineVirtualCamera camFollow;
    [SerializeField] CinemachineVirtualCamera camFullView;

    CinemachineTransposer camFVT;
    CinemachineBlendDefinition camBlendDef;

    InputAction.CallbackContext freeCam;
    bool isFreeCamActive = false;

    private void Awake()
    {
        camFVT = camFullView.GetCinemachineComponent<CinemachineTransposer>();
        camBlendDef = camBrain.m_DefaultBlend;
    }

    private void Start()
    {
        InputHandler.FreeCamEnable(this);
    }

    public void FreeCam(InputAction.CallbackContext context)
    {
        freeCam = context;
    }

    public void StartFreeCam(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            camFollow.gameObject.SetActive(false);
            camFullView.gameObject.SetActive(true);
            camBlendDef.m_Time = 0f;

            isFreeCamActive = true;
        }
        
        if (context.canceled)
        {
            camBlendDef.m_Time = .2f;
            StartCoroutine(CameraReturn());

            isFreeCamActive = false;
        }
    }

    private IEnumerator CameraReturn()
    {
        while (camFVT.m_FollowOffset != new Vector3(0, 50, 0))
        {
            camFVT.m_FollowOffset.x = Mathf.MoveTowards(camFVT.m_FollowOffset.x, 0, Time.deltaTime * 300);
            camFVT.m_FollowOffset.z = Mathf.MoveTowards(camFVT.m_FollowOffset.z, 0, Time.deltaTime * 300);
    
            yield return new WaitForSeconds(.01f);
        }

        camFullView.gameObject.SetActive(false);
        camFollow.gameObject.SetActive(true);
    
        yield break;
    }
    
    private void Update()
    {
        if (Gamepad.current != null && isFreeCamActive)
        {
            camFVT.m_FollowOffset.x += freeCam.ReadValue<Vector2>().x * CameraSpeed * Time.deltaTime * 5;
            camFVT.m_FollowOffset.z += freeCam.ReadValue<Vector2>().y * CameraSpeed * Time.deltaTime * 5;
        }
        
        else if (isFreeCamActive)
        {
            camFVT.m_FollowOffset.x += freeCam.ReadValue<Vector2>().x * CameraSpeed * Time.deltaTime;
            camFVT.m_FollowOffset.z += freeCam.ReadValue<Vector2>().y * CameraSpeed * Time.deltaTime;
        }
    
        camFVT.m_FollowOffset.x = Mathf.Clamp(camFVT.m_FollowOffset.x, -300, 300);
        camFVT.m_FollowOffset.z = Mathf.Clamp(camFVT.m_FollowOffset.z, -300, 300f);
    }

    private void OnDisable()
    {
        InputHandler.FreeCamDisable(this);
    }
}
