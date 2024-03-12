using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObstacleType = Obstacle.ObstacleType;

[CreateAssetMenu(fileName = "VFXDatabase", menuName = "VFXScriptableObjects/VFXDatabase")]

public class VFXScriptableObject : ScriptableObject
{
    public GameObject prefabParticleConcrete;
    public string ConcreteSound;
    public GameObject prefabParticleRubber;
    public string RubberSound;
    public GameObject prefabParticleFelt;
    public string FeltSound;
    public GameObject prefabParticleIce;
    public string IceSound;
    public GameObject prefabParticleNPC;
    public string NPCSound;
    public GameObject prefabParticleProps;
    public string PropsSound;
    public GameObject prefabParticleBumper;
    public string BumperSound;
    public GameObject prefabParticleDefault;






    public GameObject GetObstacleType(ObstacleType _type)
    {
        switch (_type)
        {
            case ObstacleType.Concrete:
                AudioManager2.Instance.PlaySDFX(ConcreteSound);
                return prefabParticleConcrete;

            case ObstacleType.Rubber:
                AudioManager2.Instance.PlaySDFX(RubberSound);
                return prefabParticleRubber;

            case ObstacleType.Felt:
                AudioManager2.Instance.PlaySDFX(FeltSound);
                return prefabParticleFelt;


            case ObstacleType.Ice:
                AudioManager2.Instance.PlaySDFX(IceSound);
                return prefabParticleIce;

            case ObstacleType.NPC:
                AudioManager2.Instance.PlaySDFX(NPCSound);
                return prefabParticleNPC;

            case ObstacleType.Props:
                AudioManager2.Instance.PlaySDFX(PropsSound);
                return prefabParticleProps;

            case ObstacleType.Bumper:
                AudioManager2.Instance.PlaySDFX(BumperSound);
                return prefabParticleBumper;

            default:
                return prefabParticleDefault;
        }
    }
}