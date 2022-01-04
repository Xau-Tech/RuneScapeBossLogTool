using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// General boss info class
/// </summary>
public class BossInfo : IComparable<BossInfo>
{
    //  Properties & fields
    public short BossId { get; private set; }
    public string BossName { get; set; }
    public bool HasAccessToRareDropTable { get; private set; }
    public uint BaseInstanceCost { get; private set; }
    public List<BossCombatData> CombatDataList { get; private set; }

    //  Constructors
    public BossInfo() { }
    public BossInfo(BossInfoSO info)
    {
        this.BossId = info.BossId;
        this.BossName = info.BossName;
        this.HasAccessToRareDropTable = info.HasAccessToRareDropTable;
        this.BaseInstanceCost = info.BaseInstanceCost;

        CombatDataList = new List<BossCombatData>();
        if(info.CombatData != null)
        {
            BossCombatData bcd;

            foreach(RS3BossCombatInfoSO combatInfo in info.CombatData)
            {
                bcd = new BossCombatData(combatInfo);
                this.CombatDataList.Add(bcd);
            }
        }
    }

    //  Methods
    public int CompareTo(BossInfo other)
    {
        return BossName.CompareTo(other.BossName);
    }

    public override string ToString()
    {
        return $"BossInfo [ ID: {BossId}, Name: {BossName}, HasRDT: {HasAccessToRareDropTable}, Base Instance Cost: {BaseInstanceCost} ]";
    }
}
