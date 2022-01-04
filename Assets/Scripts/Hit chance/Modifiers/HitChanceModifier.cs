using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modifies the final hit chance percentage by an additional value
/// </summary>
public class HitChanceModifier : AbsEffect
{
    //  Properties & fields

    public int Modifier { get { return _modifier; } }

    private int _modifier;

    //  Constructor

    public HitChanceModifier() : base("")
    {
        _modifier = 0;
        base.EffectType = Enums.EffectTypes.HitChance;
    }
    public HitChanceModifier(int value) : base("")
    {
        _modifier = value;
        base.EffectType = Enums.EffectTypes.HitChance;
    }
    public HitChanceModifier(string name, int value) : base(name)
    {
        _modifier = value;
        base.EffectType = Enums.EffectTypes.HitChance;
    }

    //  Methods

    public static HitChanceModifier operator +(HitChanceModifier firstMod, HitChanceModifier secondMod)
    {
        return new HitChanceModifier(firstMod._modifier + secondMod._modifier);
    }
}
