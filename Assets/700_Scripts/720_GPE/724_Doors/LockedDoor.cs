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

    public string Ball_LockedDoor_Hit;
    public string LockedDoor_Open;

    public bool Unlock
    {
        get { return unlock; }
        set
        {
            unlock = value;
            //AudioManager2.Instance.PlaySDFX(LockedDoor_Open);
            //myAnimator.SetBool("Unlock", unlock);
            //IconPadlockLocked.SetActive(false);
            //IconPadlockUnlocked.SetActive(true);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer > 0)
    //    {
    //        AudioManager2.Instance.PlaySDFX(Ball_LockedDoor_Hit);
    //    }
    //}

    //private void Start()
    //{
    //    myAnimator = GetComponent<Animator>();
    //}
}
