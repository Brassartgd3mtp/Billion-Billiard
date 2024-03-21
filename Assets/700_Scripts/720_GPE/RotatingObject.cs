using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    private Vector3 _rotation = new Vector3(0,1,0);
    public float Speed;

    void Update()
    { 
        transform.Rotate(_rotation * Time.deltaTime * Speed);
    }
}
