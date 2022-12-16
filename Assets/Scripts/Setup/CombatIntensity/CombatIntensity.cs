using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class CombatIntensity
{
    //  Properties & fields

    public float DegradePerHour { get { return _DEGRADERATES[(int)IntensityLevel]; } }
    public float DrainPerHour { get { return _DRAINRATES[(int)IntensityLevel]; } }
    public Enums.CombatIntensityLevels IntensityLevel { get; set; }

    //  Standard degrade rate represents how many charges are lost per hour - capped at 1 charge per tick
    private static float[] _DEGRADERATES = new float[]
    {
        CombatUtilities.BaseChargeProcsPerHour * .4f,
        CombatUtilities.BaseChargeProcsPerHour * .6f,
        CombatUtilities.BaseChargeProcsPerHour,
        CombatUtilities.TicksPerHour
    };
    //  Charge drain represents a percentage of charge drain used with 100% being maximum
    //  Formula currently being used I've generalized to f(x) = (120x)/(20 + x) where x is the degrade rate/min
    //  ie the average of 30 charges/min -> (120 * 30)/(20 + 30) = 72%
    private static float[] _DRAINRATES = new float[]
    {
        //CombatUtilities.BaseDrainPerHour * .4f,
        //CombatUtilities.BaseDrainPerHour * .6f,
        //CombatUtilities.BaseDrainPerHour,
        //CombatUtilities.SecondsPerHour
        .60f,
        .72f,
        .8571f,
        1.0f
    };
    private static readonly string[] _INTENSITYLEVELNAMES = new string[] { "Low", "Average", "High", "Maximum" };

    //  Constructor

    public CombatIntensity(Enums.CombatIntensityLevels intensity)
    {
        this.IntensityLevel = intensity;
    }

    //  Methods

    public static List<string> GetCombatIntensityNames()
    {
        List<string> names = new List<string>();
        names.AddRange(_INTENSITYLEVELNAMES);
        return names;
    }

    public static string GetInfo()
    {
        string result = "The values of each combat intensity is as follows:\n\n";

        for(int i = 0; i < _INTENSITYLEVELNAMES.Length; ++i)
        {
            result += $"{_INTENSITYLEVELNAMES[i]}: {_DEGRADERATES[i] / 60.0f} charge/min";

            if (i != _INTENSITYLEVELNAMES.Length - 1)
                result += "\n";
        }

        return result;
    }
}
