using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some basic info stored as enums regarding abilities
/// </summary>
public class AbilityInfo
{
    public enum DamageTypeCategory
    {
        Normal = 0,
        Bleed,
        Channeled
    }

    public enum AbilityTypeCategory
    {
        Basic = 0,
        Threshold,
        Ultimate,
        Spec
    }

    public enum CombatStyle
    {
        Melee = 0,
        Range,
        Magic,
        Defense,
        Constitution
    }

    public enum WeaponStyle
    {
        Twohand = 0,
        DualWield
    }
}
