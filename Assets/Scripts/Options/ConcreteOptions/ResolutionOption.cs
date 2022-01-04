using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Option to choose the resolution the app will display at
/// </summary>
public class ResolutionOption : GenericOption
{
    //  Properties & fields
    public readonly static string NAME = "Resolution";
    public readonly static Enums.OptionTypes OPTIONTYPE = Enums.OptionTypes.Dropdown;

    private readonly static string[] _CHOICES = { "640x360", "960x540", "1280x720", "1920x1080" };

    //  Constructor
    public ResolutionOption(IDisplayOption displayInterface) : base(displayInterface)
    {
        base.Name = NAME;
        base.OptionChoices = _CHOICES;
        base.Value = _CHOICES[2];
        base.OptionType = OPTIONTYPE;
    }

    //  Methods
    public override void Apply()
    {
        //  Parsing - format should be [HorizontalRes]x[VerticalRes]
        string[] res = base.Value.Split('x');
        int[] values = new int[2];

        //  Output should be two values
        if(res.Length != 2)
        {
            //  Set to default
            values[0] = 1280;
            values[1] = 720;
        }
        else
        {
            for(int i = 0; i < res.Length; ++i)
            {
                if(!int.TryParse(res[i], out values[i]))
                {
                    values[0] = 1280;
                    values[1] = 720;
                    break;
                }
            }
        }

        //  Set res
        Screen.SetResolution(values[0], values[1], Screen.fullScreen);
        Debug.Log($"Setting screen resolution to x = {values[0]}, y = {values[1]}.");
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
