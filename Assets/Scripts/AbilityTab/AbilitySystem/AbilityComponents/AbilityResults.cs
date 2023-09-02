using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityResults : IEnumerable<AbilityResult>
{
    private List<AbilityResult> m_AbilResultList = new();

    public AbilityResults()
    {

    }

    public void Add(AbilityResult abilResult)
    {
        m_AbilResultList.Add(abilResult);
    }

    public IEnumerator<AbilityResult> GetEnumerator()
    {
        return ((IEnumerable<AbilityResult>)m_AbilResultList).GetEnumerator();
    }

    public void Sort(IComparer<AbilityResult> comparer)
    {
        QuickSort<AbilityResult> sorter = new(comparer);
        sorter.Sort(m_AbilResultList);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)m_AbilResultList).GetEnumerator();
    }
}

public class AbilityResult
{
    public string Name { get { return m_Name; } }
    public double Min { get { return m_Min; } }
    public double Max { get { return m_Max; } }
    public double MinDps { get { return m_MinDps; } }
    public double MaxDps { get { return m_MaxDps; } }
    public bool CritCapped { get { return m_CritCapped; } }

    private readonly string m_Name;
    private readonly double m_Min;
    private readonly double m_Max;
    private readonly double m_MinDps;
    private readonly double m_MaxDps;
    private readonly bool m_CritCapped;
    private readonly double m_SecondsPerTick = .06d;

    public AbilityResult(string name, double min, double max, sbyte abilityLength, bool critCapped)
    {
        m_Name = name;
        m_Min = min;
        m_Max = max;
        double abilityLength_Seconds = abilityLength * m_SecondsPerTick;
        m_MinDps = min / abilityLength_Seconds;
        m_MaxDps = max / abilityLength_Seconds;
        m_CritCapped = critCapped;
    }

    public override string ToString()
    {
        return Name;
    }
}