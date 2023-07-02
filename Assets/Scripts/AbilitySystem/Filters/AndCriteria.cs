using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndCriteria<T> : ICriteria<T>
{
    private ICriteria<T> m_FirstCriteria, m_SecondCriteria;

    public AndCriteria(ICriteria<T> firstCriteria, ICriteria<T> secondCriteria)
    {
        m_FirstCriteria = firstCriteria;
        m_SecondCriteria = secondCriteria;
    }

    List<T> ICriteria<T>.FilterByCriteria(IEnumerable<T> listToFilter)
    {
        List<T> firstFilteredList = m_FirstCriteria.FilterByCriteria(listToFilter);
        return m_SecondCriteria.FilterByCriteria(firstFilteredList);
    }
}
