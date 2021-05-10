using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Modifies the final accuracy value by some percentage
public class AccuracyModifier : AbsEffect
{
    private double modifier;

    public AccuracyModifier() : base("")
    {
        modifier = 0.0d;
        effectType = EffectTypes.Accuracy;
    }
    public AccuracyModifier(double value) : base("")
    {
        modifier = value;
        effectType = EffectTypes.Accuracy;
    }
    public AccuracyModifier(string modName, double value) : base(modName)
    {
        modifier = value;
        effectType = EffectTypes.Accuracy;
    }

    public static AccuracyModifier operator +(AccuracyModifier firstMod, AccuracyModifier secondMod)
    {
        return new AccuracyModifier(firstMod.modifier + secondMod.modifier);
    }

    //  Return as a percentage used to multiply with
    public double GetModifier()
    {
        return 1.0d + modifier;
    }
}