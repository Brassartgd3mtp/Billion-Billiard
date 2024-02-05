using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        Concrete,
        Rubber,
        NPC,
        Props
    }

    public ObstacleType obstacleType = ObstacleType.Rubber;        
}
