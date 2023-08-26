using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayer
{
    public double DamageBoost { get; private set; }

    private readonly Dictionary<string, double> m_PrayerDict;

    public Prayer()
    {
        DamageBoost = 1.0d;
        m_PrayerDict = new();
        m_PrayerDict.Add("None", 1.0d);
        m_PrayerDict.Add("T1 Prayer", 1.02d);
        m_PrayerDict.Add("T2 Prayer", 1.04d);
        m_PrayerDict.Add("T3 Prayer", 1.06d);
        m_PrayerDict.Add("Piety variant", 1.08d);
        m_PrayerDict.Add("Leech curse", 1.08d);
        m_PrayerDict.Add("Turmoil variant", 1.1d);
        m_PrayerDict.Add("Praesul variant", 1.12d);
    }

    public List<string> GetPrayerNames()
    {
        return new List<string>(m_PrayerDict.Keys);
    }

    public void SetPrayer(string prayerName)
    {
        DamageBoost = m_PrayerDict[prayerName];
        EventManager.Instance.AbilityInputChanged();
    }
}