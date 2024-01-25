using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObstacleType = Obstacle.ObstacleType;

[CreateAssetMenu(fileName = "VFXDatabase", menuName = "VFXScriptableObjects/VFXDatabase")]

public class VFXScriptableObject : ScriptableObject
{
    public GameObject prefabParticleConcrete;
    public GameObject prefabParticleRubber;
    public GameObject prefabParticleNPC;
    public GameObject prefabParticleDefault;

    public GameObject GetObstacleType(ObstacleType _type)
    {
        switch (_type)
        {
            case ObstacleType.Concrete:
                //if(blablabla)
                return prefabParticleConcrete;
            case ObstacleType.Rubber:
                return prefabParticleRubber;
            case ObstacleType.NPC:
                return prefabParticleNPC;
            default:
                return prefabParticleDefault;
        }
    }
}