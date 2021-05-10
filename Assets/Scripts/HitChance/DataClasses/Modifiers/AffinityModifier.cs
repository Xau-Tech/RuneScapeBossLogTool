using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Modifies the affinity values of a boss making it easier to hit
public class AffinityModifier : AbsEffect
{
    private int modifier;

    private readonly int MAXVALUE = 10;

    public AffinityModifier() : base("")
    {
        modifier = 0;
        effectType = EffectTypes.Affinity;
    }
    public AffinityModifier(int modifier) : base("")
    {
        this.modifier = modifier;
        effectType = EffectTypes.Affinity;
    }
    public AffinityModifier(string name, int modifier) : base(name)
    {
        this.modifier = modifier;
        effectType = EffectTypes.Affinity;
    }

    public static AffinityModifier operator +(AffinityModifier firstMod, AffinityModifier secondMod)
    {
        return new AffinityModifier(firstMod.modifier + secondMod.modifier);
    }

    //  Maximum affinity modifier is 10 so return the smaller between 10 and the current modifier value
    public int ModValue()
    {
        return Mathf.Min(MAXVALUE, modifier);
    }
}
