using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : DamageCalculationNode
{
    public BaseNode()
    {

    }

    protected override double CalculateMin()
    {
        return (m_SubAbility.MinDamage * Player_Ability.Instance.AbilDamage.Damage).RoundDown();
    }

    protected override double CalculateVar()
    {
        return (m_SubAbility.DamageRange() * Player_Ability.Instance.AbilDamage.Damage).RoundDown();
    }
}
