using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//  Abstract script for any UI element that is part of the hit chance calculator
public abstract class AbsHitChanceUIEle : MonoBehaviour
{
    abstract public void Setup(HitChanceUISetupData setupData);
}

//  Abstract data for any data used in setting up an AbsHitChanceUIEle
public abstract class HitChanceUISetupData
{
    public int defaultValue;
}

//  Data holder for setting up an InputField as part of the hit chance calculator
public class IFSetupData : HitChanceUISetupData
{
    //  Action called on a InputField's onEndEdit(string) event
    public UnityAction<string> updateAction;

    public IFSetupData(UnityAction<string> updateAction, int defaultValue)
    {
        this.updateAction = updateAction;
        this.defaultValue = defaultValue;
    }
}

//  Data holder for setting up a Dropdown as part of the hit chance calculator
public class DDSetupData : HitChanceUISetupData
{
    //  Action called on a Dropdown's onValueChanged(int) event
    public UnityAction<int> updateAction;
    public List<string> options;

    public DDSetupData(UnityAction<int> updateAction, List<string> options)
    {
        this.updateAction = updateAction;
        this.options = options;
        this.defaultValue = 0;      //  Default value for Dropdown's should always 0 for these
    }
}