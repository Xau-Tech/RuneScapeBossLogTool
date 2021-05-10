using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Abstract modifier script
public abstract class AbsEffect
{
    //  Types of modifiers
    public enum EffectTypes { Level, Accuracy, HitChance, Affinity };

    protected EffectTypes effectType;

    private string effectName;

    public AbsEffect(string effectName)
    {
        this.effectName = effectName;
    }

    public string EffectName { get { return effectName; } }
    public EffectTypes EffectType { get { return effectType; } }
}
