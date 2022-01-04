using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static class for various runescape combat-related information
/// </summary>
public class CombatUtilities
{
    public static float SecondsPerHour = 3600.0f;
    public static float TicksPerHour { get { return SecondsPerHour / _TICK; } }
    //  Base procs that will effect standard degrade items per hour (total gcd + total enemy attacks) then multiplying by 6/7 to remove any instances where they occur in the same tick
    public static float BaseChargeProcsPerHour { get { return (GCDPerHour + EnemyAttacksPerHour) * (6.0f / 7.0f); } }
    //  Base time in seconds that will effect charge drain items per hour (6 ticks - 3.6s - is drained at a time with a 6 tick cd, so a user could drain a full 3600 of the 3600s per hour if they hit/took a hit every 6 ticks for an hour)
    //  Using charge procs because the two should be linked and readability is more important here than tiny efficiency improvements
    public static float BaseDrainPerHour { get { return (BaseChargeProcsPerHour / TicksPerHour) * SecondsPerHour; } }

    //  One tick is .6 seconds
    private static float _TICK = .6f;
    //  Global cooldown is 1.8 seconds
    private static float _GCD = 1.8f;
    //  Average monster attack speed is 2.4 seconds
    private static float _AVGENEMYATTSPEED = 2.4f;
    //  The number of global cooldowns per hour
    private static float GCDPerHour { get { return SecondsPerHour / _GCD; } }
    //  The number of attacks an average enemy will make per hour (2.4s - 4 tick - attack speed)
    private static float EnemyAttacksPerHour { get { return SecondsPerHour / _AVGENEMYATTSPEED; } }
}
