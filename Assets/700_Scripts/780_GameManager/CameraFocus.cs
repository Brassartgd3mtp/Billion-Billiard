using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        player = FindAnyObjectByType<PlayerController>().gameObject;

        virtualCamera.Follow = player.transform;
    }
}
