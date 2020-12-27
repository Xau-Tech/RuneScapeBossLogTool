using System;

//  General boss info class for use in populating the boss list and the item drop list
public class BossInfo : IComparable<BossInfo>
{
    public BossInfo() { }
    public BossInfo(short bossID, string bossName, bool hasAccessToRareDropTable, uint baseInstanceCost)
    {
        this.bossID = bossID;
        this.bossName = bossName;
        this.hasAccessToRareDropTable = hasAccessToRareDropTable;
        this.baseInstanceCost = baseInstanceCost;
    }

    //  Properties
    public short bossID { get; private set; }
    public string bossName { get; set; }
    public bool hasAccessToRareDropTable { get; private set; }
    public uint baseInstanceCost { get; private set; }

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
