using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//  Script for any Dropdown that is part of the hit chance calculator
public class HitChanceDropdown : AbsHitChanceUIEle
{
    private Dropdown thisDropdown;

    public override void Setup(HitChanceUISetupData setupData)
    {
        //  null check
        if (!(thisDropdown = GetComponent<Dropdown>()))
            throw new System.Exception($"AbsHitChanceIF.cs is not attached to a dropdown gameobject!");
        else
        {
            //  Ensure proper setup data type
            if (setupData is DDSetupData)
            {
                DDSetupData data = (DDSetupData)setupData;
                thisDropdown.onValueChanged.AddListener(data.updateAction);
                thisDropdown.AddOptions(data.options);
                thisDropdown.SetValueWithoutNotify(setupData.defaultValue);
            }
            else
                throw new System.Exception($"Setup failed - improper setup data type!");
        }
    }
}
