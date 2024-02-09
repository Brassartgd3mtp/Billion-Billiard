using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleReloadBoost : MonoBehaviour
{
    public float speedRotate = 0.25f;
    public void Update()
    {
        transform.Rotate(0, speedRotate, 0);
    }
}
