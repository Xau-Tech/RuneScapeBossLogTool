using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Data holder for setting up an input field as part of the hit chance calculator
/// </summary>
public class HitChanceIFData : HitChanceUISetupData
{
    //  Properties & fields

    public UnityAction<string> UpdateAction;

    //  Constructor

    public HitChanceIFData(UnityAction<string> updateAction, int defaultValue)
    {
        this.UpdateAction = updateAction;
        base.DefaultValue = defaultValue;
    }
}
