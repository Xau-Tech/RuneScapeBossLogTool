using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerNode : DamageCalculationNode
{
    public PrayerNode()
    {

    }

    protected override double CalculateMin()
    {
        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
            return m_PrevNode.Min;

        return (m_PrevNode.Min * Player_Ability.Instance.Prayer.DamageBoost).RoundDown();
    }

    protected override double CalculateVar()
    {
        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
            return m_PrevNode.Var;

        return (m_PrevNode.Var * Player_Ability.Instance.Prayer.DamageBoost).RoundDown();
    }
}
