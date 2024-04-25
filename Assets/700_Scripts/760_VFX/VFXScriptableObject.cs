using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObstacleType = Obstacle.ObstacleType;

[CreateAssetMenu(fileName = "VFXDatabase", menuName = "VFXScriptableObjects/VFXDatabase")]

public class VFXScriptableObject : ScriptableObject
{
    public GameObject prefabParticleConcrete;
    public GameObject prefabParticleRubber;
    public GameObject prefabParticleFelt;
    public GameObject prefabParticleIce;
    public GameObject prefabParticleVelcro;
    public GameObject prefabParticleNPC;
    public GameObject prefabParticleProps;
    public GameObject prefabParticleBumper;
    public GameObject prefabParticleIceAngle;
    public GameObject prefabParticleDefault;
    public int soundObstacleId;

    public GameObject GetObstacleType(ObstacleType _type)
    {
        switch (_type)
        {
            case ObstacleType.Concrete:
                soundObstacleId = 4;
                return prefabParticleConcrete;

            case ObstacleType.Rubber:
                soundObstacleId = 25;
                return prefabParticleRubber;

            case ObstacleType.Felt:
                soundObstacleId = 23;
                return prefabParticleFelt;

            case ObstacleType.Ice:
                soundObstacleId = 24;
                return prefabParticleIce;

            case ObstacleType.Velcro:
                soundObstacleId = 27;
                return prefabParticleVelcro;

            case ObstacleType.NPC:
                soundObstacleId = 30;
                return prefabParticleNPC;

            case ObstacleType.Props:
                return prefabParticleProps;

            case ObstacleType.Bumper:
                soundObstacleId = 9;
                return prefabParticleBumper;

            case ObstacleType.IceAngle:
                soundObstacleId = 24;
                return prefabParticleIceAngle;

            default:
                return prefabParticleDefault;
        }
    }
}