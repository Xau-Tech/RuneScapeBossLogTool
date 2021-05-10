using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Data class for a boss's affinity (how likely they are to be hit with a certain combat style/attack type)
//  Affinity ranges from 0-100 where higher correlates to a higher hit chance for the player
public class AffinityData
{
    public sbyte meleeAffinity { get; private set; }
    public sbyte rangedAffinity { get; private set; }
    public sbyte magicAffinity { get; private set; }
    public sbyte weaknessAffinity { get; private set; }
    public AttackType.AttackStyles attackStyleWeakness { get; private set; }

    public AffinityData(sbyte meleeAffinity, sbyte rangedAffinity, sbyte magicAffinity, sbyte weaknessAffinity, AttackType.AttackStyles attackStyleWeakness)
    {
        this.meleeAffinity = meleeAffinity;
        this.rangedAffinity = rangedAffinity;
        this.magicAffinity = magicAffinity;
        this.weaknessAffinity = weaknessAffinity;
        this.attackStyleWeakness = attackStyleWeakness;
    }

    //  Return the proper affinity value based on a weapon and modifier and this data
    public sbyte GetAffinity(in Weapon weapon, in AffinityModifier affinityModifier)
    {
        //  Null check
        if (weapon == null || affinityModifier == null)
            throw new System.Exception($"No weapon and/or affinity mod set to determine affinity value!");

        sbyte baseAffinity = 0;

        /*
         * Check to see if the boss' weakness matches the weapon attack style,
         * but never return the weaknessAffinity if the monster has no weakness
         */
        if (attackStyleWeakness == weapon.AttackStyle && attackStyleWeakness != AttackType.AttackStyles.None)
            baseAffinity = weaknessAffinity;
        else
        {
            //  If the attack type doesn't match, set the return value as the related combat style
            if (weapon.CombatStyle == AttackType.CombatClasses.Melee)
                baseAffinity = meleeAffinity;
            else if (weapon.CombatStyle == AttackType.CombatClasses.Ranged)
                baseAffinity = rangedAffinity;
            else if (weapon.CombatStyle == AttackType.CombatClasses.Magic)
                baseAffinity = magicAffinity;
            else
                throw new System.Exception($"Weapon \"{weapon.itemName}\" does not have a combat style set!");
        }

        //  Modify the value and return
        return (sbyte)(baseAffinity + affinityModifier.ModValue());
    }
}
