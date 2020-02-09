using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossLog
{
    public BossLog()
    {

    }
    public BossLog(string LogName, string BossName)
    {
        m_LogName = LogName;
        m_BossName = BossName;
        m_Kills = 0;
        m_LootValue = 0;
        m_TimeSpent = 0;
    }
    public BossLog(string LogName, string BossName, uint Kills,     ulong Loot, uint Time)
    {
        m_LogName = LogName;
        m_BossName = BossName;
        m_Kills = Kills;
        m_LootValue = Loot;
        m_TimeSpent = Time;
    }


    private string m_LogName, m_BossName;
    //  Time spent is stored in seconds
    private uint m_Kills, m_TimeSpent;
    private ulong m_LootValue;


    //  Properties
    public string LogName { get { return m_LogName; } }
    public string BossName { get { return m_BossName; } }
    public uint Kills { get { return m_Kills; } set { m_Kills += value; } }
    public ulong LootValue { get { return m_LootValue; } set { m_LootValue += value; } }
    public  uint TimeSpent { get { return m_TimeSpent; } set { m_TimeSpent += value; } }


    //  Operator overload to add two logs together
    public static BossLog operator +(BossLog log1, BossLog log2)
    {
        BossLog returnLog = new BossLog(log1.LogName, log1.BossName);

        returnLog.Kills = log1.Kills + log2.Kills;
        returnLog.LootValue = log1.LootValue + log2.LootValue;
        returnLog.TimeSpent = log1.TimeSpent + log2.TimeSpent;

        return returnLog;
    }


    public float AverageValuePerKill()
    {
        if(m_Kills != 0)
            return (m_LootValue / m_Kills);

        return 0f;
    }


    public float AverageKillsPerHour()
    {
        if(m_TimeSpent != 0)
            return m_Kills / (m_TimeSpent / 3600f);

        return 0f;
    }


    public float AverageValuePerHour()
    {
        if(m_TimeSpent != 0)
            return m_LootValue / (m_TimeSpent / 3600f);

        return 0f;
    }
}
