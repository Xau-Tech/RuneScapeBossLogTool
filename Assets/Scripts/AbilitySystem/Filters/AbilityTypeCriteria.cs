using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTypeCriteria : ICriteria<Ability>
{
    //  Fields & properties

    private readonly AbilityInfo.AbilityTypeCategory m_AbilityType;

    //  Constructors

    public AbilityTypeCriteria(AbilityInfo.AbilityTypeCategory abilityType)
    {
        m_AbilityType = abilityType;
    }

    //  ICriteria methods

    List<Ability> ICriteria<Ability>.FilterByCriteria(IEnumerable<Ability> listToFilter)
    {
        List<Ability> retList = new();

        foreach (Ability a in listToFilter)
        {
            if (a.AbilityType == m_AbilityType) retList.Add(a);
        }

        return retList;
    }
}
