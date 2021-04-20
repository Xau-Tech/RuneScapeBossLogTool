using System;
using System.Collections.Generic;

//  General boss info class for use in populating the boss list and the item drop list
public class BossInfo : IComparable<BossInfo>
{
    public BossInfo() { }
    public BossInfo(short bossID, string bossName, bool hasAccessToRareDropTable, uint baseInstanceCost, RS3BossCombatDataSO[] combatData)
    {
        this.bossID = bossID;
        this.bossName = bossName;
        this.hasAccessToRareDropTable = hasAccessToRareDropTable;
        this.baseInstanceCost = baseInstanceCost;

        this.combatData = new List<BossCombatData>();

        if (combatData == null)
            return;
        else
        {
            BossCombatData bcd;

            for (int i = 0; i < combatData.Length; ++i)
            {
                bcd = new BossCombatData(combatData[i]);
                this.combatData.Add(bcd);
            }
        }
    }

    //  Properties
    public short bossID { get; private set; }
    public string bossName { get; set; }
    public bool hasAccessToRareDropTable { get; private set; }
    public uint baseInstanceCost { get; private set; }

    public List<BossCombatData> combatData { get; private set; }

    //  IComparable interface
    public int CompareTo(BossInfo other)
    {
        return bossName.CompareTo(other.bossName);
    }

    public override string ToString()
    {
        return $"BossInfo [ ID: {bossID}, Name: {bossName}, HasRDT: {hasAccessToRareDropTable}, Base Instance Cost: {baseInstanceCost} ]";
    }
}
