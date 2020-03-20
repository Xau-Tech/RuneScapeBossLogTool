using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Data for loading options
public class OptionData
{
    public enum OptionNames { Resolution, BossSync };
    public enum OptionTypes { Dropdown, Toggle };
    private static string[] options = { "Resolution", "BossSync" };
        

    public static string[] GetOptions()
    {
        return options;
    }

    public static bool IsOption(in string name)
    {
        for(int i = 0; i < options.Length; ++i)
        {
            if (name.ToLower().CompareTo(options[i].ToLower()) == 0)
                return true;
        }

        return false;
    }
}
