using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for any dropdown that is part of the hit chance calculator
/// </summary>
public class HitChanceDropdown : AbsHitChanceUIElement
{
    //  Properties & fields

    private Dropdown _thisDropdown;

    //  Methods

    public override void Setup(HitChanceUISetupData setupData)
    {
        _thisDropdown = GetComponent<Dropdown>();
        HitChanceDDData data = (HitChanceDDData)setupData;
        _thisDropdown.onValueChanged.AddListener(data.UpdateAction);
        _thisDropdown.AddOptions(data.Options);
        _thisDropdown.SetValueWithoutNotify(setupData.DefaultValue);
    }
}
