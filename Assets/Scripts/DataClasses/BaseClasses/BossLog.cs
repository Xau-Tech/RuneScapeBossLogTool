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
    private uint m_Kills, m_TimeSpent;
    private ulong m_LootValue;


    //  Properties
    public string LogName { get { return m_LogName; } }
    public string BossName { get { return m_BossName; } }
    public uint Kills { get { return m_Kills; } set { m_Kills += value; } }
    public ulong LootValue { get { return m_LootValue; } set { m_LootValue += value; } }
    public  uint TimeSpent { get { return m_TimeSpent; } set { m_TimeSpent += value; } }


    //  Operator overload to add two logs together
    public static BossLog operator +(BossLog d1, BossLog d2)
    {
        d1.Kills += d2.Kills;
        d1.TimeSpent += d2.TimeSpent;
        d1.LootValue += d2.LootValue;

        return d1;
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
            return m_Kills / (m_TimeSpent / 60f);

        return 0f;
    }


    public float AverageValuePerHour()
    {
        if(m_TimeSpent != 0)
            return m_LootValue / (m_TimeSpent / 60f);

        return 0f;
    }
}
