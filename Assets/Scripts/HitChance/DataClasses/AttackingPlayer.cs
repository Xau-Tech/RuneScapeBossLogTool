using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Data holder for player combat data to be passed to a boss to calculate hit chance
public class AttackingPlayer
{
    public Weapon Weapon { get { return weapon; } }
    public AffinityModifier AffinityModifier { get { return affinityMod; } }

    private Weapon weapon;
    private int combatLevel;
    private AffinityModifier affinityMod;
    private AccuracyModifier accuracyMod;

    public AttackingPlayer(Weapon weapon, int combatLevel, AccuracyModifier accuracyMod, AffinityModifier affinityMod)
    {
        this.combatLevel = combatLevel;
        this.weapon = weapon;
        this.affinityMod = affinityMod;
        this.accuracyMod = accuracyMod;
    }

    //  Modified final accuracy value from the base accuracy and the modifier
    public int Accuracy()
    {
        return (int)Math.Round(BaseAccuracy() * accuracyMod.GetModifier(), MidpointRounding.AwayFromZero);
    }

    //  Base accuracy is an addition of 2 accuracies, one based on combat level and one based on weapon tier
    private double BaseAccuracy()
    {
        return LevelBasedAccuracy() + WeaponAccuracy();
    }

    //  f(x) = .0008x^3 + 4x + 40 where x is the level of the style used
    private int LevelBasedAccuracy()
    {
        return (int)(Math.Round(Mathf.Pow(combatLevel, 3) * .0008 + (4 * combatLevel) + 40, MidpointRounding.AwayFromZero));
    }

    //  f(x) = x^3 / 500 + 10x + 100; always rounded down where x is the weaponAccTier
    private int WeaponAccuracy()
    {
        return Mathf.FloorToInt((Mathf.Pow(weapon.accuracyTier, 3) / 500.0f) + (10.0f * weapon.accuracyTier) + 100.0f);
    }
}
