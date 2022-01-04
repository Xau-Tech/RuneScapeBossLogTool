using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Wrapper class for a list of BossLog objects
/// </summary>
[System.Serializable]
public class BossLogList : ICollection<BossLog>
{
    //  Properties & fields
    public short bossID { get; private set; }
    public int Count { get { return data.Count; } }
    public bool IsReadOnly => ((ICollection<BossLog>)data).IsReadOnly;

    private List<BossLog> data { get; set; }

    //  Constructors
    public BossLogList()
    {
        data = new List<BossLog>();
    }
    public BossLogList(short bossId)
    {
        this.bossID = bossId;
        data = new List<BossLog>();
    }

    //  Methods
    
    public void Remove(string logName)
    {
        int index;
        if ((index = data.FindIndex(log => log.logName.CompareTo(logName) == 0)) == -1)
            throw new System.Exception($"Log - {logName} - not found!");
        else
            data.RemoveAt(index);
    }

    public bool Exists(string logName)
    {
        return data.Exists(log => log.logName.CompareTo(logName) == 0);
    }

    public int FindIndex(string logName)
    {
        return data.FindIndex(log => log.logName.CompareTo(logName) == 0);
    }

    public BossLog Find(string logName)
    {
        return data.Find(log => log.logName.CompareTo(logName) == 0);
    }

    public LogDataStruct CalculateTotals()
    {
        LogDataStruct logDataStruct = new LogDataStruct();
        foreach(BossLog log in data)
        {
            logDataStruct.Kills += log.kills;
            logDataStruct.Loot += log.loot;
            logDataStruct.Time += log.time;
        }

        return logDataStruct;
    }

    public RareItemList GetRareItemList()
    {
        RareItemList rareList = new RareItemList();
        foreach(BossLog log in data)
        {
            rareList += log.rareItemList;
        }

        return rareList;
    }

    //  ICollection methods

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)data).GetEnumerator();
    }

    public void Add(BossLog item)
    {
        ((ICollection<BossLog>)data).Add(item);
    }

    public void Clear()
    {
        ((ICollection<BossLog>)data).Clear();
    }

    public bool Contains(BossLog item)
    {
        return ((ICollection<BossLog>)data).Contains(item);
    }

    public void CopyTo(BossLog[] array, int arrayIndex)
    {
        ((ICollection<BossLog>)data).CopyTo(array, arrayIndex);
    }

    public bool Remove(BossLog item)
    {
        return ((ICollection<BossLog>)data).Remove(item);
    }

    IEnumerator<BossLog> IEnumerable<BossLog>.GetEnumerator()
    {
        return ((ICollection<BossLog>)data).GetEnumerator();
    }
}
