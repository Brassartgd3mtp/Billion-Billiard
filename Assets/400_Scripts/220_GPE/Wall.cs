using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum ObstacleType
    {
        Concrete,
        Rubber,
        NPC
    }

    public ObstacleType obstacleType = ObstacleType.Rubber;
}
