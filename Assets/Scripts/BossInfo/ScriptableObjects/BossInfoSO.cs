using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/BossInfo", order = 0)]
[System.Serializable]
public class BossInfoSO : ScriptableObject
{
    public short BossId;
    public string BossName;
    public uint BaseInstanceCost;
    public bool HasAccessToRareDropTable;
    public RS3BossCombatInfoSO[] CombatData;
}

[System.Serializable]
public class RS3BossCombatInfoSO
{
    public string Name;
    public int LifePoints;
    public bool Poisonous;
    public bool PoisonImmune;
    public bool ReflectImmune;
    public bool StunImmune;
    public bool StatDrainImmune;
    public sbyte MeleeAffinity;
    public sbyte RangedAffinity;
    public sbyte MagicAffinity;
    public sbyte NecromancyAffinity;
    public sbyte WeaknessAffinity;
    public sbyte DefenseLevel;
    public short Armour;
    public Enums.AttackStyles Weakness;
    public Enums.MonsterType MonsterType;
    public Enums.CombatClasses CombatClass;
}
