using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Class detailing the various combat types and attack styles
public class AttackType
{
    public enum CombatClasses { None, Melee, Magic, Ranged };
    public enum AttackStyles { None, Crush, Slash, Stab, Arrows, Bolts, Thrown, Air, Water, Earth, Fire };

    public CombatClasses CombatClass { get { return combatClass; } }
    public AttackStyles AttackStyle { get { return attackStyle; } }

    private CombatClasses combatClass;
    private AttackStyles attackStyle;

    public AttackType(AttackStyles attackStyle)
    {
        //  Set the attack style
        this.attackStyle = attackStyle;

        //  Set the combat class based on the attack style
        if (attackStyle == AttackStyles.Slash || attackStyle == AttackStyles.Stab || attackStyle == AttackStyles.Crush)
            this.combatClass = CombatClasses.Melee;
        else if (attackStyle == AttackStyles.Arrows || attackStyle == AttackStyles.Bolts || attackStyle == AttackStyles.Thrown)
            this.combatClass = CombatClasses.Ranged;
        else
            this.combatClass = CombatClasses.Magic;
    }

    //  Get a list of all attack style names
    public static List<string> GetAttackStyleOptions()
    {
        List<string> optionNames = new List<string>();

        for(int i = 1; i < Enum.GetNames(typeof(AttackStyles)).Length; ++i)
        {
            optionNames.Add(((AttackStyles)i).ToString());
        }

        return optionNames;
    }
}