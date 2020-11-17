using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Option to choose resolution app will run at
public class ResolutionOption : GenericOption
{
    private readonly static string[] choices = {"640x360", "960x540", "1280x720", "1920x1080" };
    private readonly static string name = "Resolution";

    public ResolutionOption(IDisplayOption displayInterface) : base(displayInterface)
    {
        SetName(in name);
        SetChoices(in choices);
        SetValue(in choices[2]);    //  1280x720
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
        //  Parsing - format should be [HorizontalRes]x[VerticalRes]
        string[] res = (GetValue()).Split('x');

        //  Output should be two values
        if(res.Length != 2)
            throw new System.Exception($"Resolution value {GetValue()} is not in the right format!");

        int[] values = new int[2];
        for(int i = 0; i < res.Length; ++i)
        {
            if (!int.TryParse(res[i], out values[i]))
                throw new Exception("One of the values in the selected resolution cannot be converted to an integer!");
        }

        //  Set res while maintaining previous fullscreen choice
        Screen.SetResolution(values[0], values[1], Screen.fullScreen);
        Debug.Log($"Setting screen resolution to x = {values[0]}, y = {values[1]}.");
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
