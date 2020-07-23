using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Option to choose whether user is playing RS3 or OSRS
public class RSVersionOption : GenericOption
{
    private readonly static string[] choices = { "RS3", "OSRS" };
    private readonly static string name = "RSVersion";

    public RSVersionOption(IDisplayOption displayInterface) : base(displayInterface)
    {
        SetName(in name);
        SetChoices(in choices);
        SetValue(in choices[0]);    //  RS3
        SetOptionType(OptionData.OptionTypes.Dropdown);
    }

    public static string Name()
    {
        return name;
    }

    public static OptionData.OptionTypes OptionType()
    {
        return OptionData.OptionTypes.Dropdown;
    }

    public override void Apply()
    {
        if (ProgramState.CurrentState == ProgramState.states.Loading)
            return;

        EventManager.Instance.RSVersionChanged();
    }

    //  Apply callback function if there is unsaved data or active drops
    private void FireApplyEvent(UserResponse response, string originalValue)
    {
        if (response.userResponse)
            EventManager.Instance.RSVersionChanged();
        else
            base.SetValue(in originalValue);
    }

    public override void DisplayChoice()
    {
        displayInterface.DisplayChoice(GetValue());
    }

    public override void PopulateChoices()
    {
        displayInterface.PopulateChoices(GetChoices());
    }
}
