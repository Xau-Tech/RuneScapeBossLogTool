using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Modifies the final hit chance percentage by some addition value
public class HitChanceModifier : AbsEffect
{
    private int modifier;

    public HitChanceModifier() : base("")
    {
        modifier = 0;
        effectType = EffectTypes.HitChance;
    }
    public HitChanceModifier(int value) : base("")
    {
        modifier = value;
        effectType = EffectTypes.HitChance;
    }
    public HitChanceModifier(string name, int value) : base(name)
    {
        modifier = value;
        effectType = EffectTypes.HitChance;
    }

    public int Modifier { get { return modifier; } }

    public static HitChanceModifier operator +(HitChanceModifier firstMod, HitChanceModifier secondMod)
    {
        return new HitChanceModifier(firstMod.modifier + secondMod.modifier);
    }
}
