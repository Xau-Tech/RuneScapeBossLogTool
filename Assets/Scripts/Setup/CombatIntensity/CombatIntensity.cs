using System.Collections.Generic;

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
    private static readonly float[] ratesOfStandardDegrade = new float[] { 1200.0f, 1800.0f, 3000.0f, 6000.0f };
    //  Charge drain represents how many seconds of charge are drained per hour
    private static readonly float[] ratesOfChargeDrain = new float[] { 480.0f, 720.0f, 1200.0f, 2400.0f };
    private static readonly string[] intensityLevelNames = new string[] { "Low", "Average", "High", "Maximum" };

    public static List<string> CombatIntensityNames()
    {
        List<string> names = new List<string>();
        names.AddRange(intensityLevelNames);

        return names;
    }

    public static string GetInfo()
    {
        string result = "The values of each combat intensity is as follows:\n\n";

        for(int i = 0; i < intensityLevelNames.Length; ++i)
        {
            result += $"{intensityLevelNames[i]}: {ratesOfStandardDegrade[i] / 60.0f} charge/min";

            if (i != intensityLevelNames.Length - 1)
                result += "\n";
        }

        return result;
    }
}
