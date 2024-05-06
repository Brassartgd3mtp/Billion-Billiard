using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGestionPlayer : MonoBehaviour
{
    public Animator MyAnimator;
    public GameObject Pelvis_Int;
    public GameObject FXVictory01;
    public GameObject FXVictory02;
    public GameObject FXVictory03;


    private void Start()
    {
        MyAnimator = GetComponent<Animator>();
    }

    private void PlayFXVictory()
    {
        Instantiate(FXVictory01, Pelvis_Int.transform.position, Quaternion.identity);
        Instantiate(FXVictory02, Pelvis_Int.transform.position, Quaternion.identity);
        Instantiate(FXVictory03, transform.position, Quaternion.identity);

    }

    private void ChangeIdleBoolFalse()
    {
        MyAnimator.SetBool("Idle_Change", false);
    }

    private void ChangeIdleBoolTrue()
    {
        MyAnimator.SetBool("Idle_Change", true);
    }
}
