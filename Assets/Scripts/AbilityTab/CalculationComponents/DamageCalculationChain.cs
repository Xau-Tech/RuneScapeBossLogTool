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
        m_CalculationChain.Add(new PreciseNode());
        m_CalculationChain.Add(new EquilibriumNode());
        m_CalculationChain.Add(new FinalBaseNode());
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

        DamageCalcPassthrough rollingResults = new()
        {
            Min = 0,
            Var = 0,
            CritCapped = false
        };

        foreach(SubAbility sa in ability)
        {
            DamageCalcPassthrough currentResult = new() { Min = sa.MinDamage, Var = sa.DamageRange(), CritCapped = false };

            foreach(DamageCalculationNode node in m_CalculationChain)
            {
                currentResult = node.Calculate(currentResult, sa);
                PrintTestingInfo(node, ability, currentResult);
            }

            if(currentResult.Min > Player_Ability.Instance.CritCap.ModdedMaxCrit)
            {
                currentResult.Min = Player_Ability.Instance.CritCap.ModdedMaxCrit;
                currentResult.CritCapped = true;
            }

            if((currentResult.Min + currentResult.Var) > Player_Ability.Instance.CritCap.ModdedMaxCrit)
            {
                Debug.Log($"{ability.Name} has a sub-ability that is over the crit cap - reducing damage!");
                currentResult.Var = Player_Ability.Instance.CritCap.ModdedMaxCrit - currentResult.Min;
                currentResult.CritCapped = true;
            }

            currentResult.Min *= sa.BaseNumHits;
            currentResult.Var *= sa.BaseNumHits;
            //Debug.Log($"Current results for {ability.Name} w/ {sa.BaseNumHits} hits: {currentResult.Min} - {currentResult.Var}");

            rollingResults += currentResult;
            //Debug.Log($"Rolling results for {ability.Name}: {rollingResults.Min} - {rollingResults.Var}");
        }

        return rollingResults;
    }

    private void PrintTestingInfo(DamageCalculationNode node, Ability ability, DamageCalcPassthrough currentResult)
    {
        string text = "";
        if (node is BaseNode) text = "Base Node";
        else if (node is PrayerNode) text = "Prayer Node";
        else if (node is DPLNode) text = "DPL Node";
        else if (node is PreciseNode) text = "Precise Node";
        else if (node is EquilibriumNode) text = "Equilibrium Node";
        else if (node is FinalBaseNode) text = "Final Base Node";

        Debug.Log($"{ability.Name} - {text} [ {currentResult.Min} : {currentResult.Var} ]\n{Time.time}");
    }
}