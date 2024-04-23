using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGestionPlayer : MonoBehaviour
{
    public Animator MyAnimator;

       private void ChangeIdleBoolFalse()
    {
        MyAnimator.SetBool("Idle_Change", false);
    }

    private void ChangeIdleBoolTrue()
    {
        MyAnimator.SetBool("Idle_Change", true);
    }
}
