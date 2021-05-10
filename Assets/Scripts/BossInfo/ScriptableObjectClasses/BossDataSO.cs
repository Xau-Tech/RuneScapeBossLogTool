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

    public AttackType.AttackStyles weakness;

    public MonsterType monsterType;

    public AttackType.CombatClasses combatClass;
}