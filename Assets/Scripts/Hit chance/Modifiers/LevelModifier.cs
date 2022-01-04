using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modifier that affects the players level(s)
/// </summary>
public class LevelModifier : AbsEffect
{
    //  Properties & fields

    private double _percentMod;
    private int _integerMod;

    //  Constructor

    public LevelModifier() : base("")
    {
        _percentMod = 0.0d;
        _integerMod = 0;
        base.EffectType = Enums.EffectTypes.Level;
    }
    public LevelModifier(double percentMod, int integerMod) : base("")
    {
        _percentMod = percentMod;
        _integerMod = integerMod;
        base.EffectType = Enums.EffectTypes.Level;
    }
    public LevelModifier(string name, double percentMod, int integerMod) : base(name)
    {
        _percentMod = percentMod;
        _integerMod = integerMod;
        base.EffectType = Enums.EffectTypes.Level;
    }

    //  Methods

    public static LevelModifier operator +(LevelModifier firstMod, LevelModifier secondMod)
    {
        return new LevelModifier((firstMod._percentMod + secondMod._percentMod), (firstMod._integerMod + secondMod._integerMod));
    }

    public int BoostSkill(int skillLevel)
    {
        //  Base level * (1 + percentmod) + integer mod rounded down
        return (int)(skillLevel * (_percentMod + 1.0d) + _integerMod);
    }
}
