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
    public GameObject prefabParticleNPC;
    public GameObject prefabParticleProps;
    public GameObject prefabParticleDefault;

    public GameObject GetObstacleType(ObstacleType _type)
    {
        switch (_type)
        {
            case ObstacleType.Concrete:
                return prefabParticleConcrete;
            case ObstacleType.Rubber:
                return prefabParticleRubber;
            case ObstacleType.Felt:
                return prefabParticleFelt;
            case ObstacleType.Ice:
                return prefabParticleIce;
            case ObstacleType.NPC:
                return prefabParticleNPC;
            case ObstacleType.Props:
                return prefabParticleProps;
            default:
                return prefabParticleDefault;
        }
    }
}