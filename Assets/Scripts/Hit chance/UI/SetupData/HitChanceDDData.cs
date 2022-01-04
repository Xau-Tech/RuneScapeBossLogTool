using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Data holder for setting up a dropdown as part of the hit chance calculator
/// </summary>
public class HitChanceDDData : HitChanceUISetupData
{
    //  Properties & fields

    public UnityAction<int> UpdateAction;
    public List<string> Options;

    //  Constructor

    public HitChanceDDData(UnityAction<int> updateAction, List<string> options)
    {
        this.UpdateAction = updateAction;
        this.Options = options;
        base.DefaultValue = 0;
    }
}
