using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeCam : MonoBehaviour
{
    PlayerController pc;

    InputAction FreeCam;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();

        FreeCam = pc.ActionAsset.FindActionMap("Gamepad").FindAction("FreeCam");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
