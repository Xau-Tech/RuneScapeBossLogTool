using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculationChain
{
    private readonly List<DamageCalculationNode> m_CalculationChain;

    public DamageCalculationChain()
    {
        m_CalculationChain = new();
        m_CalculationChain.Add(new BaseNode());
    }

    public void CalculateDamage(Player_Ability playerAbility, Ability ability)
    {
        /*
         * TODO
         * Using a foreach and an inner loop at start which is obviously garbage O(n^2)
         * Will be experimenting with various strategies later such as
         * using multiple threads
         * having each node always occupied (as opposed to running one subabil through at a time)
         * using multiple chains
         */

        DamageCalcPassthrough dcp = new();

        foreach(SubAbility sa in ability)
        {
            foreach(DamageCalculationNode node in m_CalculationChain)
            {
                dcp = node.Calculate(playerAbility, dcp, sa);
            }
        }

        Debug.Log($"{ability.Name}\nMin: {dcp.Min}\nMax: {dcp.Max}");
    }
}