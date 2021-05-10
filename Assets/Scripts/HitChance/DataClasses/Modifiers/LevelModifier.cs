using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Modifier that changes the user's combat level(s)
public class LevelModifier : AbsEffect
{
    private double percentMod;
    private int integerMod;

    public LevelModifier() : base("")
    {
        percentMod = 0.0d;
        integerMod = 0;
        effectType = EffectTypes.Level;
    }
    public LevelModifier(double percentMod, int integerMod) : base("")
    {
        this.percentMod = percentMod;
        this.integerMod = integerMod;
        effectType = EffectTypes.Level;
    }
    public LevelModifier(string modName, double percentMod, int integerMod) : base(modName)
    {
        this.percentMod = percentMod;
        this.integerMod = integerMod;
        effectType = EffectTypes.Level;
    }

    public static LevelModifier operator +(LevelModifier firstMod, LevelModifier secondMod)
    {
        return new LevelModifier((firstMod.percentMod + secondMod.percentMod), (firstMod.integerMod + secondMod.integerMod));
    }

    //  
    public int BoostSkill(int skillLevel)
    {
        //  Base level * (1 + percent modifier) + integer modifier rounded down
        return (int)(skillLevel * (percentMod + 1.0d) + integerMod);
    }
}
