using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//  Script for any InputField that is part of the hit chance calculator
public class HitChanceIF : AbsHitChanceUIEle
{
    private InputField thisIF;

    public override void Setup(HitChanceUISetupData setupData)
    {
        //  null check
        if (!(thisIF = GetComponent<InputField>()))
            throw new System.Exception($"AbsHitChanceIF.cs is not attached to an input field gameobject!");
        else
        {
            //  Ensure proper setup data type
            if (setupData is IFSetupData)
            {
                thisIF.onEndEdit.AddListener(((IFSetupData)setupData).updateAction);
                thisIF.SetTextWithoutNotify(setupData.defaultValue + "");
            }
            else
                throw new System.Exception($"Setup failed - improper setup data type!");
        }
    }
}