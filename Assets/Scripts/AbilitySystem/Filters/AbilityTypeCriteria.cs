using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Filter Abilities by AbilityType (basic, thresh, ult, spec)
/// This filter is a bit unique as each Ability type can be toggled on or off in the UI
/// meaning it handles everything internally as combinations of OR cases
/// </summary>
public class AbilityTypeCriteria : ICriteria<Ability>
{
    //  Fields & properties

    private readonly IEnumerable<AbilityInfo.AbilityTypeCategory> m_AbilityTypeList;

    //  Constructors

    public AbilityTypeCriteria(IEnumerable<AbilityInfo.AbilityTypeCategory> abilityTypeList)
    {
        m_AbilityTypeList = abilityTypeList;
    }

    //  ICriteria methods

    public bool MeetsCriteria(Ability abil)
    {
        //  Look through the list of AbilityTypes that we want to include and return true if any of them are a match to the current Ability
        foreach(AbilityInfo.AbilityTypeCategory abilType in m_AbilityTypeList)
        {
            if (abilType == abil.AbilityType) return true;
        }

        return false;
    }
}
