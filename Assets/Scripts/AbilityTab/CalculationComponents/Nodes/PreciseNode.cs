using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciseNode : DamageCalculationNode
{
    private readonly double M_PRECISEBOOST = .015d;

    public PreciseNode()
    {

    }

    protected override double CalculateMin()
    {
        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
            return m_PrevNode.Min;

        double preciseAddition = ((m_PrevNode.Min + m_PrevNode.Var) * Player_Ability.Instance.Perks.PreciseRank * M_PRECISEBOOST).RoundDown();
        return m_PrevNode.Min + preciseAddition;
    }

    protected override double CalculateVar()
    {
        //  Note that the var decreases because the min increases but the actual max (min + var) remains the same
        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
            return m_PrevNode.Var;

        double preciseSubtraction = ((m_PrevNode.Min + m_PrevNode.Var) * Player_Ability.Instance.Perks.PreciseRank * M_PRECISEBOOST).RoundDown();
        return m_PrevNode.Var - preciseSubtraction;
    }
}