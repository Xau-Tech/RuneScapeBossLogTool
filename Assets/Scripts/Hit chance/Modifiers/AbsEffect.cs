using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract modifier for hit chance calculations
/// </summary>
public abstract class AbsEffect
{
    //  Properties & fields

    public string EffectName { get; private set; }
    public Enums.EffectTypes EffectType { get; protected set; }

    //  Constructor

    public AbsEffect(string effectName)
    {
        this.EffectName = effectName;
    }
}
