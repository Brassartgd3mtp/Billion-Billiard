using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngotType
{
    Bronze,
    Silver,
    Gold,
    BigGold,
    Platinium
}

public class MoneyStats : MonoBehaviour
{
    public IngotType type;
    public int value;
    public float pitchValue;
}
