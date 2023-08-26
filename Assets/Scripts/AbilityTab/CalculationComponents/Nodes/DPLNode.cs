using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPLNode : DamageCalculationNode
{
    private readonly double M_DPLCONST = 4.0d;

    public DPLNode()
    {

    }

    protected override double CalculateMin()
    {
        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
            return m_PrevNode.Min;

        return (M_DPLCONST * Mathf.Max(0, Player_Ability.Instance.CombatLevel.BoostAmount) + m_PrevNode.Min).RoundDown();
    }

    protected override double CalculateVar()
    {
        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
            return m_PrevNode.Var;

        return (M_DPLCONST * Mathf.Max(0, Player_Ability.Instance.CombatLevel.BoostAmount) + m_PrevNode.Var).RoundDown();
    }
}
