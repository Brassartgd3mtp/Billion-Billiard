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
                return prefabParticleRubber;

            case ObstacleType.Felt:
                soundObstacleId = 7;
                return prefabParticleFelt;

            case ObstacleType.Ice:
                return prefabParticleIce;

            case ObstacleType.Velcro:
                return prefabParticleVelcro;

            case ObstacleType.NPC:
                return prefabParticleNPC;

            case ObstacleType.Props:
                return prefabParticleProps;

            case ObstacleType.Bumper:
                soundObstacleId = 9;
                return prefabParticleBumper;

            default:
                return prefabParticleDefault;
        }
    }
}