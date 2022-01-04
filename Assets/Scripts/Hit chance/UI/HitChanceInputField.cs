using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for any input field that is part of the hit chance calculator
/// </summary>
public class HitChanceInputField : AbsHitChanceUIElement
{
    //  Properties & fields

    private InputField _thisIf;

    //  Methods

    public override void Setup(HitChanceUISetupData setupData)
    {
        _thisIf = GetComponent<InputField>();
        HitChanceIFData data = (HitChanceIFData)setupData;
        _thisIf.onEndEdit.AddListener(data.UpdateAction);
        _thisIf.SetTextWithoutNotify(setupData.DefaultValue + "");
    }
}
