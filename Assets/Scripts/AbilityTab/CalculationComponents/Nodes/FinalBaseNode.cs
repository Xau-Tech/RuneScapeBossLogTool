using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBaseNode : DamageCalculationNode
{
    private readonly double m_BleedExclusive = 1.0d;
    private readonly double m_CritExclusive = 1.0d;


    public FinalBaseNode()
    {

    }

    public override DamageCalcPassthrough Calculate(DamageCalcPassthrough prevNode, SubAbility subAbililty)
    {
        //  Var needs to be re-calculated because the CalculateVar function here is calculating the new maximum
        DamageCalcPassthrough passthrough = base.Calculate(prevNode, subAbililty);
        passthrough.Var -= passthrough.Min;
        return passthrough;
    }

    protected override double CalculateMin()
    {
        double bleedCalc = m_PrevNode.Min * Player_Ability.Instance.CombatLevel.BleedInclusive_Aura;

        if(m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
        {
            return bleedCalc.RoundDown();
        }
        else
        {
            return (bleedCalc * m_BleedExclusive).RoundDown();
        }
    }

    protected override double CalculateVar()
    {
        double bleedCalc = ((m_PrevNode.Min + m_PrevNode.Var) * Player_Ability.Instance.CombatLevel.BleedInclusive_Aura);

        if (m_SubAbility.DamageType == AbilityInfo.DamageTypeCategory.Bleed)
        {
            return bleedCalc.RoundDown();
        }
        else
        {
            return (bleedCalc * m_BleedExclusive * m_CritExclusive).RoundDown();
        }
    }
}
