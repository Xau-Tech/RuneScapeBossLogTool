using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General option data
/// </summary>
public class OptionData
{
    //  Properties & fields
    public static string[] Options { get { return _OPTIONS; } }

    private readonly static string[] _OPTIONS = { "Resolution", "BossSync", "RSVersion" };

    //  Methods
    public static bool IsOption(string name)
    {
        for(int i = 0; i < Options.Length; ++i)
        {
            if (name.ToLower().CompareTo(_OPTIONS[i].ToLower()) == 0)
                return true;
        }

        return false;
    }
}
