using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        Concrete,
        Rubber,
        Felt, 
        Ice,
        NPC
    }

    public ObstacleType obstacleType = ObstacleType.Rubber;        
}
