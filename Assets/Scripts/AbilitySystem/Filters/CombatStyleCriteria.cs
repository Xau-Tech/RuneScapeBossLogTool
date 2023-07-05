using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Filter Abilities by Combat style (melee, range, magic, defense, const, (necro soon))
/// </summary>
public class CombatStyleCriteria : ICriteria<Ability>
{
    //  Properties & fields

    private readonly AbilityInfo.CombatStyle m_CombatStyle;

    //  Constructor

    public CombatStyleCriteria(AbilityInfo.CombatStyle combatStyle)
    {
        m_CombatStyle = combatStyle;
    }

    //  ICriteria methods

    public bool MeetsCriteria(Ability abil)
    {
        //  True if combat style matches criteria, or ability combat style is defensive or constitution (those 2 are always listed currently)
        return (abil.CombatStyle == m_CombatStyle || abil.CombatStyle == AbilityInfo.CombatStyle.Defense || abil.CombatStyle == AbilityInfo.CombatStyle.Constitution);
    }
}
