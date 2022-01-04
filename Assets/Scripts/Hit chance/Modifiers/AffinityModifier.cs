using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modifier the affinity values of the boss
/// </summary>
public class AffinityModifier : AbsEffect
{
    //  Properties & fields

    public int ModValue { get { return Mathf.Min(_MAXVALUE, _modifier); } }

    private int _modifier;
    private readonly int _MAXVALUE = 10;

    //  Constructor

    public AffinityModifier() : base("")
    {
        _modifier = 0;
        base.EffectType = Enums.EffectTypes.Affinity;
    }
    public AffinityModifier(int modifier) : base("")
    {
        _modifier = modifier;
        base.EffectType = Enums.EffectTypes.Affinity;
    }
    public AffinityModifier(string name, int modifier) : base(name)
    {
        _modifier = modifier;
        base.EffectType = Enums.EffectTypes.Affinity;
    }

    //  Methods

    public static AffinityModifier operator +(AffinityModifier firstMod, AffinityModifier secondMod)
    {
        return new AffinityModifier(firstMod._modifier + secondMod._modifier);
    }
}
