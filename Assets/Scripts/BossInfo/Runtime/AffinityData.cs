using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class for a boss's affinity (how likely they are to be hit with a certain combat style/attack type)
///  Affinity ranges from 0-100 where higher correlates to a higher hit chance for the player
/// </summary>
public class AffinityData : MonoBehaviour
{
    public sbyte MeleeAffinity { get; private set; }
    public sbyte RangedAffinity { get; private set; }
    public sbyte MagicAffinity { get; private set; }
    public sbyte NecromancyAffinity { get; private set; }
    public sbyte WeaknessAffinity { get; private set; }
    public Enums.AttackStyles AttackStyleWeakness { get; private set; }

    public AffinityData(sbyte meleeAffinity, sbyte rangedAffinity, sbyte magicAffinity, sbyte necromancyAffinity, sbyte weaknessAffinity, Enums.AttackStyles attackStyleWeakness)
    {
        this.MeleeAffinity = meleeAffinity;
        this.RangedAffinity = rangedAffinity;
        this.MagicAffinity = magicAffinity;
        this.NecromancyAffinity = necromancyAffinity;
        this.WeaknessAffinity = weaknessAffinity;
        this.AttackStyleWeakness = attackStyleWeakness;
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
        if (AttackStyleWeakness == weapon.AttackStyle && AttackStyleWeakness !=  Enums.AttackStyles.None)
            baseAffinity = WeaknessAffinity;
        else
        {
            //  If the attack type doesn't match, set the return value as the related combat style
            if (weapon.CombatStyle == Enums.CombatClasses.Melee)
                baseAffinity = MeleeAffinity;
            else if (weapon.CombatStyle == Enums.CombatClasses.Ranged)
                baseAffinity = RangedAffinity;
            else if (weapon.CombatStyle == Enums.CombatClasses.Magic)
                baseAffinity = MagicAffinity;
            else if (weapon.CombatStyle == Enums.CombatClasses.Necromancy)
                baseAffinity = NecromancyAffinity;
            else
                throw new System.Exception($"Weapon \"{weapon.ItemName}\" does not have a combat style set!");
        }

        //  Modify the value and return
        return (sbyte)(baseAffinity + affinityModifier.ModValue);
    }
}
