using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Details attack info for hit chance data
/// </summary>
public class AttackType
{
    //  Properties & fields

    public Enums.CombatClasses CombatClass { get; }
    public Enums.AttackStyles AttackStyle { get; }

    //  Methods

    public AttackType(Enums.AttackStyles attackStyle)
    {
        //  Set attack style
        this.AttackStyle = attackStyle;

        //  Set combat class based on attack style
        if (attackStyle == Enums.AttackStyles.Slash || attackStyle == Enums.AttackStyles.Stab || attackStyle == Enums.AttackStyles.Crush)
            this.CombatClass = Enums.CombatClasses.Melee;
        else if (attackStyle == Enums.AttackStyles.Arrows || attackStyle == Enums.AttackStyles.Bolts || attackStyle == Enums.AttackStyles.Thrown)
            this.CombatClass = Enums.CombatClasses.Ranged;
        else if (attackStyle == Enums.AttackStyles.Necromancy)
            this.CombatClass = Enums.CombatClasses.Necromancy;
        else
            this.CombatClass = Enums.CombatClasses.Magic;
    }

    public static List<string> GetAttackStyles()
    {
        List<string> styles = new List<string>();

        for(int i = 1; i < Enum.GetNames(typeof(Enums.AttackStyles)).Length; ++i)
        {
            styles.Add(((Enums.AttackStyles)i).ToString());
        }

        return styles;
    }
}
