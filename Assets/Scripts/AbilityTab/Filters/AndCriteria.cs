using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Criteria to And together 2 simple criteria
/// </summary>
/// <typeparam name="T">The object type of your 2 ICriteria of which you are filtering</typeparam>
public class AndCriteria<T> : ICriteria<T>
{
    private readonly ICriteria<T> m_FirstCriteria, m_SecondCriteria;

    public AndCriteria(ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
    {
        //  Ensure we aren't chaining together AndCriteria when building
#if UNITY_EDITOR
        if (m_FirstCriteria is AndCriteria<T> || m_SecondCriteria is AndCriteria<T>)
        {
            throw new System.Exception($"ERROR: Zach please don't chain multiple ands together!");
        }
#endif

        m_FirstCriteria = firstCriteria;
        m_SecondCriteria = secondCriteria;
    }

    public bool MeetsCriteria(T obj)
    {
        return m_FirstCriteria.MeetsCriteria(obj) && m_SecondCriteria.MeetsCriteria(obj);
    }
}
