using UnityEngine;

/*  Data holder for an individual boss in either rs3 or osrs
 *  Base instance cost is the coin value (0 if instance requires an item or no cost)
 */
[CreateAssetMenu(menuName = "Bosses/BossInfo", order = 0)]
[System.Serializable]
public class BossDataSO : ScriptableObject
{
    public short bossID;
    public string bossName;
    public uint baseInstanceCost;
    public bool hasAccessToRareDropTable;

    public RS3BossCombatDataSO[] combatData;
}

[System.Serializable]
public class RS3BossCombatDataSO
{
    public string name;

    public int lifepoints;

    public bool poisonous;
    public bool poisonImmune;
    public bool reflectImmune;
    public bool stunImmune;
    public bool statDrainImmune;

    public sbyte meleeAffinity;
    public sbyte rangedAffinity;
    public sbyte magicAffinity;
    public sbyte weaknessAffinity;
    public sbyte defenseLevel;

    public short armour;

    public Weaknesses weakness;

    public MonsterType monsterType;

    public CombatClass combatClass;
}

public enum Weaknesses { None, Crush, Slash, Stab, Arrows, Bolts, Thrown, Air, Water, Earth, Fire };
public enum MonsterType { Other, Dagannoth, Kalphite, Dragon, Undead };
public enum CombatClass { None, Melee, Magic, Ranged, All };
