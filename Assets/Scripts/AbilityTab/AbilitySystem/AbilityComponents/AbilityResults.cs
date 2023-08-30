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

    private readonly string m_Name;
    private readonly double m_Min;
    private readonly double m_Max;
    private readonly double m_MinDps;

    public AbilityResult(string name, double min, double max, sbyte tickLength)
    {
        m_Name = name;
        m_Min = min;
        m_Max = max;
        m_MinDps = min / (tickLength * 0.6d);
    }
}