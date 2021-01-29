using System.Collections;
using System.Collections.Generic;

//  List of BossLog instances that makes up the Values in BossLogDictionary
[System.Serializable]
public class BossLogList : ICollection<BossLog>
{
    public short bossID { get; private set; }
    //  ICollection properties
    public int Count { get { return data.Count; } }
    public bool IsReadOnly => ((ICollection<BossLog>)data).IsReadOnly;

    private List<BossLog> data { get; set; }

    public BossLogList()
    {
        data = new List<BossLog>();
    }
    public BossLogList(short bossID)
    {
        this.bossID = bossID;
        data = new List<BossLog>();
    }

    //  Remove the log with the passed name parameter
    public void Remove(string logName)
    {
        //  Make sure passed logname exists in this collection
        int index;
        if ((index = data.FindIndex(log => log.logName.CompareTo(logName) == 0)) == -1)
            throw new System.Exception($"Log -{logName}- not found!");

        data.RemoveAt(index);
    }

    //  IEnumerable interface
    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)data).GetEnumerator();
    }

    //  Wrapper for List.Contains checking with a passed logName parameter
    public bool Exists(string logName)
    {
        return data.Exists(bossLog => bossLog.logName.CompareTo(logName) == 0);
    }

    //  Wrapper for List.FindIndex checking with a passed logName parameter
    public int FindIndex(string logName)
    {
        return data.FindIndex(bossLog => bossLog.logName.CompareTo(logName) == 0);
    }

    //  Wrapper for List.Find checking with a passed logName parameter
    public BossLog Find(string logName)
    {
        return data.Find(bossLog => bossLog.logName.CompareTo(logName) == 0);
    }

    //  Return a custom struct with our total data
    public LogDataStruct GetBossTotalsData()
    {
        LogDataStruct logDataStruct = new LogDataStruct();
        foreach(BossLog log in data)
        {
            logDataStruct.kills += log.kills;
            logDataStruct.time += log.time;
            logDataStruct.loot += log.loot;
        }

        return logDataStruct;
    }

    public RareItemList GetRareItemList()
    {
        RareItemList returnList = new RareItemList();

        foreach(BossLog bossLog in data)
        {
            returnList += bossLog.rareItemList;
        }

        return returnList;
    }

    //  ICollection<T> interface methods
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
