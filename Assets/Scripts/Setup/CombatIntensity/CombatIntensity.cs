using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatIntensity
{
    public enum CombatIntensityLevels { Low = 0, Average, High, Maximum };

    public CombatIntensity(CombatIntensityLevels intensity)
    {
        this.IntensityLevel = intensity;
    }

    public float degradePerHour { get { return ratesOfStandardDegrade[(int)IntensityLevel]; } }
    public float drainPerHour { get { return ratesOfChargeDrain[(int)IntensityLevel]; } }
    public CombatIntensityLevels IntensityLevel { get; set; }

    //  Standard degrade rate represents how many charges are lost per hour
    private readonly float[] ratesOfStandardDegrade = new float[] { 1800.0f, 2700.0f, 3600.0f, 6000.0f };
    //  Charge drain represents how many seconds of charge are drained per hour
    private readonly float[] ratesOfChargeDrain = new float[] { 1080.0f, 1620.0f, 2160.0f, 3600.0f };
    private static readonly string[] intensityLevelNames = new string[] { "Low", "Average", "High", "Maximum" };
    private static readonly sbyte numOptions = 4;

    public static List<string> CombatIntensityNames()
    {
        List<string> names = new List<string>();
        names.AddRange(intensityLevelNames);

        return names;
    }
}
