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
        m_CalculationChain.Add(new PrayerNode());
        m_CalculationChain.Add(new DPLNode());
    }

    public DamageCalcPassthrough CalculateDamage(in Ability ability)
    {
        /*
         * TODO
         * Using a foreach and an inner loop at start which is obviously garbage O(n^2)
         * Will be experimenting with various strategies later such as
         * using multiple threads
         * having each node always occupied (as opposed to running one subabil through at a time)
         * using multiple chains
         */

        DamageCalcPassthrough rollingResults = new();

        foreach(SubAbility sa in ability)
        {
            DamageCalcPassthrough currentResult = new() { Min = sa.MinDamage, Var = sa.DamageRange() };
            int n = 0;

            foreach(DamageCalculationNode node in m_CalculationChain)
            {
                currentResult = node.Calculate(currentResult, sa);
                PrintTestingInfo(node, ability, currentResult);
            }

            currentResult.Min *= sa.BaseNumHits;
            currentResult.Var *= sa.BaseNumHits;

            rollingResults += currentResult;
        }

        return rollingResults;
    }

    private void PrintTestingInfo(DamageCalculationNode node, Ability ability, DamageCalcPassthrough currentResult)
    {
        string text = "";
        if (node is BaseNode) text = "Base Node";
        else if (node is PrayerNode) text = "Prayer Node";
        else if(node is DPLNode) text = "DPLNode";

        Debug.Log($"{ability.Name} - {text} [ {currentResult.Min} : {currentResult.Var} ]\n{Time.time}");
    }
}