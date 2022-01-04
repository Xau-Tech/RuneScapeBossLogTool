using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds user-logged data for a specific boss instance
/// </summary>
[System.Serializable]
public class BossLog
{
    //  Properties & fields
    public string logName { get; set; }
    public short bossID { get; private set; }
    public uint kills { get; set; }
    public ulong loot { get; set; }
    //  Time is stored in seconds
    public uint time { get; set; }
    public string setupName { get; set; }
    public RareItemList rareItemList { get; private set; }

    //  Constructors
    public BossLog()
    {
        rareItemList = new RareItemList();
    }
    public BossLog(short bossId, string logName)
    {
        this.bossID = bossId;
        this.logName = logName;
        this.kills = 0;
        this.loot = 0;
        this.time = 0;
        rareItemList = new RareItemList();
    }
    public BossLog(short bossId, string logName, uint Kills, ulong Loot, uint Time)
    {
        this.bossID = bossId;
        this.logName = logName;
        this.kills = Kills;
        this.loot = Loot;
        this.time = Time;
        rareItemList = new RareItemList();
    }

    //  Methods

    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(logName);
    }

    public void AddToRareItemList(ItemSlotList itemSlotList)
    {
        Debug.Log($"Adding to {this.bossID} : {this.logName}\nInitial List:");
        rareItemList.AddFromDropsList(itemSlotList);
    }

    public static BossLog operator +(BossLog log1, BossLog log2)
    {
        //  Make sure none of the values are going to wrap
        if (log1.kills.WillWrap(log2.kills))
        {
            PopupManager.Instance.ShowNotification("Cannot add - kills value overflow!");
            return log1;
        }
        else if (log1.loot.WillWrap(log2.loot))
        {
            PopupManager.Instance.ShowNotification("Cannot add - loot value overflow!");
            return log1;
        }
        else if (log1.time.WillWrap(log2.time))
        {
            PopupManager.Instance.ShowNotification("Cannot add - time value overflow!");
            return log1;
        }
        else
        {
            log1.kills += log2.kills;
            log1.loot += log2.loot;
            log1.time += log2.time;
            return log1;
        }
    }

    public override string ToString()
    {
        return $"{logName}\nKills: {kills}, Loot: {loot}, Time (s): {time}";
    }
}
