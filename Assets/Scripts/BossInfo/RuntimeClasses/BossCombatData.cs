using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossCombatData : IComparable<BossCombatData>
{
    public string name { get; private set; }

    public int lifepoints { get; private set; }

    public bool poisonous { get; private set; }
    public bool poisonImmune { get; private set; }
    public bool reflectImmune { get; private set; }
    public bool stunImmune { get; private set; }
    public bool statDrainImmune { get; private set; }

    public sbyte meleeAffinity { get; private set; }
    public sbyte rangedAffinity { get; private set; }
    public sbyte magicAffinity { get; private set; }
    public sbyte weaknessAffinity { get; private set; }

    public Weaknesses weakness { get; private set; }

    public MonsterType monsterType { get; private set; }

    public CombatClass combatClass { get; private set; }

    private short armour;

    private sbyte defenseLevel;

    public BossCombatData() { }
    public BossCombatData(RS3BossCombatDataSO combatData)
    {
        this.name = combatData.name;

        this.lifepoints = combatData.lifepoints;

        this.poisonous = combatData.poisonous;
        this.poisonImmune = combatData.poisonImmune;
        this.reflectImmune = combatData.reflectImmune;
        this.stunImmune = combatData.stunImmune;
        this.statDrainImmune = combatData.statDrainImmune;

        this.meleeAffinity = combatData.meleeAffinity;
        this.rangedAffinity = combatData.rangedAffinity;
        this.magicAffinity = combatData.magicAffinity;
        this.weaknessAffinity = combatData.weaknessAffinity;
        this.defenseLevel = combatData.defenseLevel;

        this.armour = combatData.armour;

        this.weakness = combatData.weakness;

        this.monsterType = combatData.monsterType;

        this.combatClass = combatData.combatClass;
    }

    int IComparable<BossCombatData>.CompareTo(BossCombatData other)
    {
        return this.name.CompareTo(other.name);
    }
}
