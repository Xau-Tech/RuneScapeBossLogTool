using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Option to maintain the same boss choice across all tabs if one is changed
/// </summary>
public class BossSyncOption : GenericOption
{
    //  Properties & fields
    public readonly static string NAME = "BossSync";
    public readonly static Enums.OptionTypes OPTIONTYPE = Enums.OptionTypes.Toggle;

    private readonly static string[] _CHOICES = { "false", "true" };

    //  Constructor
    public BossSyncOption(IDisplayOption displayInterface) : base(displayInterface)
    {
        base.Name = NAME;
        base.OptionChoices = _CHOICES;
        base.Value = _CHOICES[0];
        base.OptionType = OPTIONTYPE;
    }

    //  Methods

    public override void Apply() { }

    public override void DisplayChoice()
    {
        base._displayInterface.DisplayChoice(base.Value);
    }

    public override void PopulateChoices() { }
}
