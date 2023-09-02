using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ability
{
    public static Player_Ability Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = new();
            return m_Instance;
        }
    }
    public CombatLevel CombatLevel { get; set; }
    public AbilityDamage AbilDamage { get; set; }
    public Prayer Prayer { get; set; }
    public Perks Perks { get; set; }
    public CritCap CritCap { get; set; }

    private static Player_Ability m_Instance;

    public Player_Ability()
    {
        CombatLevel = new();
        AbilDamage = new(CombatLevel.BaseCombatSkillLevel);
        Prayer = new();
        Perks = new();
        CritCap = new();
    }

    public void SetBaseCombatSkillLevel(byte value)
    {
        CombatLevel.BaseCombatSkillLevel = value;
        AbilDamage.BoostedCombatLevel = CombatLevel.ModdedCombatSkillLevel;
    }
}