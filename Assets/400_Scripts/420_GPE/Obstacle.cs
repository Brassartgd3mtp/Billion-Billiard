using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        /// <summary>
        /// Concrete wall type, default bounce value : .8
        /// </summary>
        [InspectorName("Béton")] Concrete,
        /// <summary>
        /// Rubber wall type, default bounce value : .98
        /// </summary>
        [InspectorName("Caoutchouc")] Rubber,
        /// <summary>
        /// Rubber wall type, default bounce value : .5
        /// </summary>
        [InspectorName("Feutre")] Felt,
        /// <summary>
        /// Rubber wall type, Player will follow the shape.
        /// </summary>
        [InspectorName("Glace")] Ice,
        /// <summary>
        /// Rubber wall type, default Unity physics bouce.
        /// </summary>
        [InspectorName("PNJ")] NPC
    }

    public ObstacleType obstacleType = ObstacleType.Rubber;
}
