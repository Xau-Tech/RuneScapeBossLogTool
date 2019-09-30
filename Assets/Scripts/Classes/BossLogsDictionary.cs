using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogsDictionaryClass
{
    public BossLogsDictionaryClass()
    {
        m_BossLogsDictionary = new Dictionary<string, List<SingleBossLogData>>();
    }

    private Dictionary<string, List<SingleBossLogData>> m_BossLogsDictionary;

    //  Properties
    private Dictionary<string, List<SingleBossLogData>> BossLogsDictionary { get { return m_BossLogsDictionary; } }


    //  Returns the list of all logs for a boss by name
    public List<SingleBossLogData> GetBossLogList(string _name)
    {
        List<SingleBossLogData> list = new List<SingleBossLogData>();

        //  Return null if key does not exist
        if (!m_BossLogsDictionary.TryGetValue(_name, out list))
            return null;

        return list;
    }


    //  Returns the log for a boss by boss name and log name
    public SingleBossLogData GetBossLogData(string _bossName, string _logName)
    {
        //  Get correct list of logs based on the boss name
        List<SingleBossLogData> logList = GetBossLogList(_bossName);

        //  Return log if/when found
        foreach(SingleBossLogData data in logList)
        {
            if (data.LogName.CompareTo(_logName) == 0)
                return data;
        }

        return null;
    }

    
    //  Return a Vector3 of each of the 3 combined average values (each log for boss x combined)
    //  Returns x = kills/hr; y = value/kill; z = value/hr
    public Vector3 GetCombinedAverages(string _bossName)
    {
        float value = 0;
        float kills = 0;
        float time = 0;
        Vector3 v = new Vector3();

        //  Get log list for passed boss
        List<SingleBossLogData> logList = GetBossLogList(_bossName);

        //  Total up each value from all logs
        foreach(SingleBossLogData data in logList)
        {
            value += data.LootValue;
            kills += data.Kills;
            time += data.TimeSpent;
        }

        //  Make sure we aren't dividing by 0
        if(time != 0)
        {
            //  Kills/hr
            v.x = kills / (time / 60f);
            //  Value/hr
            v.z = value / (time / 60f);
        }
        else
        {
            v.x = 0f;
            v.z = 0f;
        }
        if (kills != 0)
        {
            //  Value/kill
            v.y = value / kills;
        }
        else
            v.y = 0f;

        return v;
    }
}
