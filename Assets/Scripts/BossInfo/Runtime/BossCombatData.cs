using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Data about a boss' combat info
/// </summary>
public class BossCombatData : IComparable<BossCombatData>
{
    //  Properties & fields
    public string Name { get; private set; }
    public int Lifepoints { get; private set; }
    public bool Poisonous { get; private set; }
    public bool PoisonImmune { get; private set; }
    public bool ReflectImmune { get; private set; }
    public bool StunImmune { get; private set; }
    public bool StatDrainImmune { get; private set; }
    public Enums.MonsterType MonsterType { get; private set; }
    public AffinityData AffinityData { get; private set; }
    public Enums.CombatClasses CombatClass { get; private set; }

    private short _armour;
    private sbyte _defLvl;

    //  Constructor

    public BossCombatData(RS3BossCombatInfoSO info)
    {
        this.Name = info.Name;
        this.Lifepoints = info.LifePoints;
        this.Poisonous = info.Poisonous;
        this.PoisonImmune = info.PoisonImmune;
        this.ReflectImmune = info.ReflectImmune;
        this.StunImmune = info.StunImmune;
        this.StatDrainImmune = info.StatDrainImmune;
        this.MonsterType = info.MonsterType;
        this.CombatClass = info.CombatClass;
        this._armour = info.Armour;
        this._defLvl = info.DefenseLevel;
        AffinityData = new AffinityData(info.MeleeAffinity, info.RangedAffinity, info.MagicAffinity, info.NecromancyAffinity, info.WeaknessAffinity, info.Weakness);
    }

    //  Methods

    /// <summary>
    /// Calculate the hit chance based on this data and an attackingplayer.  Formula is HitChance = affinity * accuracy / defense
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public double HitChance(AttackingPlayer player)
    {
        return AffinityData.GetAffinity(player.Weapon, player.AffinityModifier) * (double)player.Accuracy() / ArmourRating();
    }

    /// <summary>
    /// Determines the defense aspect of the hit chance formula.
    /// Adds the armor bonus granted from the defense level with the armor value itself
    /// f(x) = .0008x^3 + 4x + 40; where x is defense level
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    private int ArmourRating()
    {
        return (int)((Mathf.Pow(_defLvl, 3) * .0008 + (4 * _defLvl) + 40) + _armour);
    }

    public int CompareTo(BossCombatData other)
    {
        return Name.CompareTo(other.Name);
    }
}