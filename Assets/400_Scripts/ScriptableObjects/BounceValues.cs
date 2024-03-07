using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bounce", menuName = "BounceObject", order = 1)]
public class BounceValues : ScriptableObject
{
    public float Concrete = .8f;
    public float Rubber = .98f;
    public float Felt = .5f;
    public float Bumper = 1.25f;
}