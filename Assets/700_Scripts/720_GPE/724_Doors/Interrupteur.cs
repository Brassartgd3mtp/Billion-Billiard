using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interrupteur : MonoBehaviour
{
    private bool contactPNJ = false;
    private new Collider collider;
    public Animation MyAnimation;
    public bool ChangeColor = false;
    public bool ContactPNJ
    {
        get { return contactPNJ; }
        set
        {
            contactPNJ = value;
            collider.isTrigger = true;
            MyAnimation.enabled = false;
            ChangeColor = true;
            SoundButton();
        }
    }

    private void Start()
    {
        collider = GetComponent<Collider>();
        MyAnimation = GetComponentInChildren<Animation>();

    }

    private void SoundButton()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(26, audioSource);
    }
}