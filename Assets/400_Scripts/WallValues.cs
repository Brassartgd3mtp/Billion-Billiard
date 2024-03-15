using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallValues
{
    #region Bounce
    public static float ConcreteBounce = .8f;
    public static float RubberBounce = .98f;
    public static float FeltBounce = .5f;
    public static float BumperBounce = 1.25f;
    public static float VelcroBounce = 0f;
    #endregion

    #region Haptics
    /* Les vibrations de la manette :
     * 
     * LFH = Low Frequency Haptics
     * HFH = High Frenquency Haptics
     * TH = Timer Haptics
     * 
     * Les manettes ont deux modes de vibration, les fr�quences hautes et les fr�quences basses.
     * La fr�quence change le temps entre deux vibrations.
     * la valeur qu'on lui assigne change l'intensit� des vibrations.
     * 
     * Le Timer est la dur�e totale des vibrations. � la fin de ce timer, les vibrations s'arr�tent.
     */
    public static float ConcreteLFH = 0f;
    public static float ConcreteHFH = 1f;
    public static float ConcreteTH = .2f;

    public static float RubberLFH = 0f;
    public static float RubberHFH = 1f;
    public static float RubberTH = .2f;

    public static float FeltLFH = 0f;
    public static float FeltHFH = 1f;
    public static float FeltTH = .2f;

    public static float BumperLFH = 0f;
    public static float BumperHFH = 1f;
    public static float BumperTH = .2f;

    public static float PawnLFH = 0f;
    public static float PawnHFH = 1f;
    public static float PawnTH = .2f;
    #endregion
}