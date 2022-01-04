using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Option swaps between RS3 and OSRS
/// </summary>
public class RSVersionOption : GenericOption
{
    //  Properties & fields
    public readonly static string NAME = "RSVersion";
    public readonly static Enums.OptionTypes OPTIONTYPE = Enums.OptionTypes.Dropdown;

    private readonly static string[] _CHOICES = { "RS3", "OSRS" };

    //  Constructor
    public RSVersionOption(IDisplayOption displayInterface) : base(displayInterface)
    {
        base.Name = NAME;
        base.OptionChoices = _CHOICES;
        base.Value = _CHOICES[0];
        base.OptionType = OPTIONTYPE;
    }

    public override void Apply()
    {
        EventManager.Instance.RSVersionChanged();
    }

    public override void DisplayChoice()
    {
        base._displayInterface.DisplayChoice(base.Value);
    }

    public override void PopulateChoices()
    {
        base._displayInterface.PopulateChoices(base.GetOptionChoices());
    }
}
