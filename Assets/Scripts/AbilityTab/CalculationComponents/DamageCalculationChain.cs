using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculationChain
{
    private readonly List<DamageCalculationNode> m_CalculationChain;

    public DamageCalculationChain()
    {

    }

    public DamageCalculationResults CalculateDamage(Player_Ability playerAbility, Ability ability)
    {
        DamageCalculationResults dcr = new();



        return dcr;
    }
}