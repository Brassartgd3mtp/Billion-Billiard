using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockedDoor : MonoBehaviour
{
    private bool unlock = false;
    public float DelayOpen = 0.5f;

    private Animator myAnimator;

    public bool Unlock
    {
        get { return unlock; }
        set
        {
            unlock = value;
            //myAnimator.SetBool("Unlock", unlock);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
}
