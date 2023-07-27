using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Some basic info stored as enums and a few getters regarding abilities
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
        Any = 0,
        Twohand,
        DualWield
    }

    //  Use the below to fill in UI elements
    public List<string> AbilityTypes { get { return m_AbilityTypes; } }
    public List<string> CombatStyles { get { return m_CombatStyles; } }
    public List<string> WeaponStyles { get { return m_WeaponStyles; } }

    private readonly List<string> m_DamageTypes, m_AbilityTypes, m_CombatStyles, m_WeaponStyles;

    public AbilityInfo()
    {
        m_DamageTypes = new();
        foreach(var name in Enum.GetNames(typeof(DamageTypeCategory)))
        {
            m_DamageTypes.Add(name);
        }

        m_AbilityTypes = new();
        foreach (var name in Enum.GetNames(typeof(AbilityTypeCategory)))
        {
            m_AbilityTypes.Add(name);
        }

        m_CombatStyles = new();
        foreach (var name in Enum.GetNames(typeof(CombatStyle)))
        {
            m_CombatStyles.Add(name);
        }

        m_WeaponStyles = new();
        foreach(var name in Enum.GetNames(typeof(WeaponStyle)))
        {
            m_WeaponStyles.Add(name);
        }
    }
}