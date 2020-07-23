using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  General boss info class for use in populating the boss list and the item drop list
public class BossInfo
{
    public BossInfo()
    {

    }
    public BossInfo(string name, bool hasAccessToRareDropTable, bool isInstanceItem, uint baseInstanceCost)
    {
        bossName = name;
        this.hasAccessToRareDropTable = hasAccessToRareDropTable;
        this.isInstanceItem = isInstanceItem;
        this.baseInstanceCost = baseInstanceCost;
    }

    //  Properties
    public string bossName { get; private set; }
    public bool hasAccessToRareDropTable { get; private set; }
    public uint baseInstanceCost { get; private set; }
    public bool isInstanceItem { get; private set; }

    public override string ToString()
    {
        return $"BossInfo [ Name: {bossName}, HasRDT: {hasAccessToRareDropTable}, IsInstanceItem: {isInstanceItem}" +
            $", Base Instance Cost: {baseInstanceCost} ]";
    }
}
