using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Data class about a boss combat data/values
public class BossCombatData : IComparable<BossCombatData>
{
    public string name { get; private set; }
    public int lifepoints { get; private set; }
    public bool poisonous { get; private set; }
    public bool poisonImmune { get; private set; }
    public bool reflectImmune { get; private set; }
    public bool stunImmune { get; private set; }
    public bool statDrainImmune { get; private set; }
    public MonsterType monsterType { get; private set; }
    public AffinityData affinityData { get; private set; }
    public AttackType.CombatClasses combatClass { get; private set; }

    private short armour;
    private sbyte defenseLevel;

    public BossCombatData(RS3BossCombatDataSO combatData)
    {
        this.name = combatData.name;
        this.lifepoints = combatData.lifepoints;
        this.poisonous = combatData.poisonous;
        this.poisonImmune = combatData.poisonImmune;
        this.reflectImmune = combatData.reflectImmune;
        this.stunImmune = combatData.stunImmune;
        this.statDrainImmune = combatData.statDrainImmune;
        this.monsterType = combatData.monsterType;
        this.combatClass = combatData.combatClass;
        affinityData = new AffinityData(combatData.meleeAffinity, combatData.rangedAffinity, combatData.magicAffinity, combatData.weaknessAffinity, combatData.weakness);

        this.defenseLevel = combatData.defenseLevel;
        this.armour = combatData.armour;
    }

    //  Calculate the hit chance based on this data and an attackingplayer
    //  Formula is HitChance = affinity * accuracy / defense
    public double HitChance(in AttackingPlayer player)
    {
        return affinityData.GetAffinity(player.Weapon, player.AffinityModifier) * (double)player.Accuracy() / ArmourRating();
    }

    /*  Determines the defense aspect of the hit chance formula
    /   adds the armour bonus granted from the defense level with the armour value itself
    /   f(x) = .0008x^3 + 4x + 40; where x is the defense level
    */
    private int ArmourRating()
    {
        return (int)(System.Math.Round(Mathf.Pow(defenseLevel, 3) * .0008 + (4 * defenseLevel) + 40, MidpointRounding.AwayFromZero)
            + armour);
    }

    //  IComparable implementation
    int IComparable<BossCombatData>.CompareTo(BossCombatData other)
    {
        return name.CompareTo(other.name);
    }
}

public enum MonsterType { Other, Dagannoth, Kalphite, Dragon, Undead };     //  Monster type used for susceptibilities