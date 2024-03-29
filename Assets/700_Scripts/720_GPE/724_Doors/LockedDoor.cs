using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockedDoor : MonoBehaviour
{
    private bool unlock = false;

    //private Animator myAnimator;

    public GameObject IconPadlockUnlocked;
    public GameObject IconPadlockLocked;

    public bool Unlock
    {
        get { return unlock; }
        set
        {
            unlock = value;
            //myAnimator.SetBool("Unlock", unlock);
            IconPadlockLocked.SetActive(false);
            IconPadlockUnlocked.SetActive(true);
        }
    }

    //private void Start()
    //{
    //    myAnimator = GetComponent<Animator>();
    //}
}
