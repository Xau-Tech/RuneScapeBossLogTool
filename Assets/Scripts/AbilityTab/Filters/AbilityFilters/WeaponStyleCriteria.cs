using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Filter Abilities by Weapon Style (any, dw, 2h)
/// </summary>
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

    public bool MeetsCriteria(Ability abil)
    {
        //  True if the criteria to meet is all any weapon style, the criteria matches the ability's weapon style, or the ability's weapon style is any
        if (m_WeaponStyle == AbilityInfo.WeaponStyle.Any ||
            m_WeaponStyle == abil.WeaponStyle ||
            abil.WeaponStyle == AbilityInfo.WeaponStyle.Any)
            return true;

        return false;
    }
}
