using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPlayer
{
    //  Properties & fields

    public Weapon Weapon { get; private set; }
    public AffinityModifier AffinityModifier { get; private set; }

    private int _combatLevel;
    private AccuracyModifier _accMod;

    //  Constructor

    public AttackingPlayer(Weapon weapon, int combatLevel, AccuracyModifier accMod, AffinityModifier affMod)
    {
        this.Weapon = weapon;
        _combatLevel = combatLevel;
        _accMod = accMod;
        this.AffinityModifier = affMod;
    }

    //  Methods

    //  Modified final accuracy value from the base accuracy and the modifier
    public int Accuracy()
    {
        return (int)Math.Round(BaseAccuracy() * _accMod.Modifier, MidpointRounding.AwayFromZero);
    }

    //  Base accuracy is an addition of 2 accuracies, one based on combat level and one based on weapon tier
    private double BaseAccuracy()
    {
        return LevelBasedAccuracy() + WeaponAccuracy();
    }

    //  f(x) = .0008x^3 + 4x + 40 where x is the level of the style used
    private int LevelBasedAccuracy()
    {
        return (int)(Math.Round(Mathf.Pow(_combatLevel, 3) * .0008 + (4 * _combatLevel) + 40, MidpointRounding.AwayFromZero));
    }

    //  f(x) = x^3 / 500 + 10x + 100 always rounded down where x is the weapon accuracy tier
    private int WeaponAccuracy()
    {
        return Mathf.FloorToInt((Mathf.Pow(this.Weapon.AccuracyTier, 3) / 500.0f) + (10.0f * this.Weapon.AccuracyTier) + 100.0f);
    }
}
