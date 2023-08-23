using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : DamageCalculationNode
{
    public BaseNode()
    {

    }

    protected override double CalculateMax()
    {
        return (m_SubAbility.DamageRange() * m_PlayerAbil.AbilDamage.Damage).RoundDown();
    }

    protected override double CalculateMin()
    {
        return (m_SubAbility.MinDamage * m_PlayerAbil.AbilDamage.Damage).RoundDown();
    }
}
