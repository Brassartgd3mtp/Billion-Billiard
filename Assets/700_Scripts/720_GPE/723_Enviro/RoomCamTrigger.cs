using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamTrigger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    PlayerRoomCam playerRoomCam;

    private void Start()
    {
        playerRoomCam = FindAnyObjectByType<PlayerRoomCam>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && playerRoomCam.camCurrentRoom != virtualCamera)
        {
            playerRoomCam.camCurrentRoom = virtualCamera;
            Debug.Log(virtualCamera);
        }
    }
}
