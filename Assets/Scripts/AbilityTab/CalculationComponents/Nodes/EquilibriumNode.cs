using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquilibriumNode : DamageCalculationNode
{
    private readonly double M_MINMOD = .03d;
    private readonly double M_MAXMOD = .04d;

    public EquilibriumNode()
    {

    }

    protected override double CalculateMin()
    {
        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
            return m_PrevNode.Min;

        double equilAddition = (m_PrevNode.Var * Player_Ability.Instance.Perks.EquilibriumRank * M_MINMOD).RoundDown();
        return m_PrevNode.Min + equilAddition;
    }

    protected override double CalculateVar()
    {
        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
            return m_PrevNode.Var;

        double equilMultiplier = 1.0d - (Player_Ability.Instance.Perks.EquilibriumRank * M_MAXMOD);
        return (m_PrevNode.Var * equilMultiplier).RoundDown();
    }
}
