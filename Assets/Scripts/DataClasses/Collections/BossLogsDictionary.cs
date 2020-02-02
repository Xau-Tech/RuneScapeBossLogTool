using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossLogsDictionary
{
    public BossLogsDictionary()
    {
        m_Data = new Dictionary<string, List<BossLog>>();
    }

    private Dictionary<string, List<BossLog>> m_Data;

    //  Properties
    public Dictionary<string, List<BossLog>> data { get { return m_Data; } }


    //  Returns the list of all logs for a boss by name
    public List<BossLog> GetBossLogList(string _name)
    {
        List<BossLog> list = new List<BossLog>();
        data.TryGetValue(_name, out list);
        return list;
    }


    //  Returns a sorted list of strings of all log names for a boss by name
    public List<string> GetBossLogNamesList(string _name)
    {
        List<BossLog> bossLogs = GetBossLogList(_name);

        List<string> returnList = new List<string>();

        foreach(BossLog data in bossLogs)
        {
            returnList.Add(data.LogName);
        }

        returnList.Sort();
        return returnList;
    }


    //  Update a log with passed data
    public void SetLog(string _boss, string _log, BossLog _data)
    {
        List<BossLog> list = GetBossLogList(_boss);

        list[list.IndexOf(GetBossLogData(_boss, _log))] = _data;
    }

    //  Remove a log based on boss name and log name
    public void RemoveLog(string _boss, string _log)
    {
        List<BossLog> list = GetBossLogList(_boss);

        list.RemoveAt(list.IndexOf(GetBossLogData(_boss, _log)));
    }


    //  Returns the log for a boss by boss name and log name
    public BossLog GetBossLogData(string _bossName, string _logName)
    {
        //  Get correct list of logs based on the boss name
        List<BossLog> logList = GetBossLogList(_bossName);

        //  Return log if/when found
        foreach(BossLog data in logList)
        {
            if (data.LogName.CompareTo(_logName) == 0)
                return data;
        }

        return null;
    }

    
    //  Return a Vector3[2] of each of the 3 combined average values (each log for boss x combined)
    //  [0] Returns x = value; y = kills; z = time
    //  [1] Returns x = kills/hr; y = value/kill; z = value/hr
    public Vector3[] GetBossTotalsData(string _bossName)
    {
        Vector3[] v = new Vector3[2];
        Vector3 totals = new Vector3();

        //  Get log list for passed boss
        List<BossLog> logList = GetBossLogList(_bossName);

        //  Total up each value from all logs
        foreach(BossLog data in logList)
        {
            totals.x += data.LootValue;
            totals.y += data.Kills;
            totals.z += data.TimeSpent;
            v[0] = totals;
        }

        //  Make sure we aren't dividing by 0
        if(totals.z != 0)
        {
            //  Kills/hr
            v[1].x = totals.y / (totals.z / 60f);
            //  Value/hr
            v[1].z = totals.x / (totals.z / 60f);
        }
        else
        {
            v[1].x = 0f;
            v[1].z = 0f;
        }
        if (totals.y != 0)
        {
            //  Value/kill
            v[1].y = totals.x / totals.y;
        }
        else
            v[1].y = 0f;

        return v;
    }
}
