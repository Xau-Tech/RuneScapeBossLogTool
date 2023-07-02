using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStyleCriteria : ICriteria<Ability>
{
    //  Fields & properties

    private readonly AbilityInfo.WeaponStyle m_WeaponStyle;

    //  Constructors

    public WeaponStyleCriteria(AbilityInfo.WeaponStyle weaponStyle)
    {
        m_WeaponStyle = weaponStyle;
    }

    //  ICriteria methods

    List<Ability> ICriteria<Ability>.FilterByCriteria(IEnumerable<Ability> listToFilter)
    {
        if (m_WeaponStyle == AbilityInfo.WeaponStyle.Any)
            return new List<Ability>(listToFilter);

        List<Ability> retList = new();

        foreach(Ability a in listToFilter)
        {
            if (a.WeaponStyle == m_WeaponStyle || a.WeaponStyle == AbilityInfo.WeaponStyle.Any) retList.Add(a);
        }

        return retList;
    }
}
