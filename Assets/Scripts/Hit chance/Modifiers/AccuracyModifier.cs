using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modifies the final accuracy value by a percentage
/// </summary>
public class AccuracyModifier : AbsEffect
{
    //  Properties & fields

    public double Modifier { get { return _modifier + 1.0d; } }

    private double _modifier;

    //  Constructor

    public AccuracyModifier() : base("")
    {
        _modifier = 0.0d;
        base.EffectType = Enums.EffectTypes.Accuracy;
    }
    public AccuracyModifier(double modifier) : base("")
    {
        _modifier = modifier;
        base.EffectType = Enums.EffectTypes.Accuracy;
    }
    public AccuracyModifier(string name, double modifier) : base(name)
    {
        _modifier = modifier;
        base.EffectType = Enums.EffectTypes.Accuracy;
    }

    //  Methods

    public static AccuracyModifier operator +(AccuracyModifier firstMod, AccuracyModifier secondMod)
    {
        return new AccuracyModifier(firstMod._modifier + secondMod._modifier);
    }
}
