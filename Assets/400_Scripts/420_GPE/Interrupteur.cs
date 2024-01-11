using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interrupteur : MonoBehaviour
{
    [SerializeField] private LockedDoor lockedDoor;
    public LayerMask Player;

    private void OnCollisionEnter(Collision collision)
    {
        if ((Player.value & 1 << collision.gameObject.layer) > 0)
        {
            lockedDoor.Unlock = true;
            Destroy(gameObject);
        }
    }
}
