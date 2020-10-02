using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossLog
{
    public BossLog()
    {
        rareItemList = new RareItemList();
    }
    public BossLog(short bossID, string logName)
    {
        this.bossID = bossID;
        this.logName = logName;
        this.kills = 0;
        this.loot = 0;
        this.time = 0;
        rareItemList = new RareItemList();
    }
    public BossLog(short bossID, string logName, uint Kills, ulong Loot, uint Time)
    {
        this.bossID = bossID;
        this.logName = logName;
        this.kills = Kills;
        this.loot = Loot;
        this.time = Time;
        rareItemList = new RareItemList();
    }

    //  Properties
    public string logName { get; set; }
    public short bossID { get; private set; }
    public uint kills { get; set; }
    public ulong loot { get; set; }
    //  Time is stored in seconds
    public uint time { get; set; }
    public string setupName { get; set; }
    public RareItemList rareItemList { get; private set; }

    //  Operator overload to add two logs together
    public static BossLog operator +(BossLog log1, BossLog log2)
    {
        //  Make sure none of the values are going to wrap around from the addition
        if(log1.kills.WillWrap(log2.kills))
        {
            InputWarningWindow.Instance.OpenWindow($"Cannot add - kills value overflow!");
            return log1;
        }
        else if (log1.loot.WillWrap(log2.loot))
        {
            InputWarningWindow.Instance.OpenWindow($"Cannot add - loot value overflow!");
            return log1;
        }
        else if (log1.time.WillWrap(log2.time))
        {
            InputWarningWindow.Instance.OpenWindow($"Cannot add - time value overflow!");
            return log1;
        }
        
        log1.kills += log2.kills;
        log1.loot += log2.loot;
        log1.time += log2.time;

        return log1;
    }

    public void AddToRareItemList(in ItemSlotList itemSlotList)
    {
        Debug.Log($"Adding to {this.bossID} : {this.logName}\nInitial List:");
        rareItemList.AddFromDropsList(in itemSlotList);
    }

    public override string ToString()
    {
        return $"{logName}\nKills: {kills}, Loot: {loot}, Time (s): {time}";
    }
}
