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
        [InspectorName("B\u00E9ton")] Concrete,
        /// <summary>
        /// Rubber wall type, default bounce value : .98
        /// </summary>
        [InspectorName("Caoutchouc")] Rubber,
        /// <summary>
        /// Felt wall type, default bounce value : .5
        /// </summary>
        [InspectorName("Feutre")] Felt,
        /// <summary>
        /// Ice wall type, Player will follow the shape.
        /// </summary>
        [InspectorName("Glace")] Ice,
        /// <summary>
        /// Ice sliding wall type, Player will follow the shape.
        /// </summary>
        [InspectorName("Glace Angulée")] IceAngle,
        /// <summary>
        /// Velcro wall type, Player will stop on it.
        /// </summary>
        [InspectorName("Velcro")] Velcro,
        /// <summary>
        /// NPC GPE, default Unity physics bouce.
        /// </summary>
        [InspectorName("PNJ")] NPC,
        /// <summary>
        /// Props wall type, trigger object.
        /// </summary>
        [InspectorName("Props")] Props,
        /// <summary>
        /// Bumper GPE, default bounce value : 1.15
        /// </summary>
        [InspectorName("Bumper")] Bumper
    }

    public ObstacleType obstacleType = ObstacleType.Rubber;
}