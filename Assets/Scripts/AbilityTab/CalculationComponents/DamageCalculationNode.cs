using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageCalculationNode
{
    protected DamageCalcPassthrough m_PrevNode;
    protected SubAbility m_SubAbility;

    public virtual DamageCalcPassthrough Calculate(DamageCalcPassthrough prevNode, SubAbility subAbililty)
    {
        m_PrevNode = prevNode;
        m_SubAbility = subAbililty;
        DamageCalcPassthrough newNode = new() { Var = 0, Min = 0 };

        newNode.Min = CalculateMin();
        newNode.Var = CalculateVar();

        return newNode;
    }

    protected abstract double CalculateMin();
    protected abstract double CalculateVar();
}

public struct DamageCalcPassthrough
{
    public double Min;
    public double Var;
    public bool CritCapped;

    public static DamageCalcPassthrough operator +(DamageCalcPassthrough dcp1, DamageCalcPassthrough dcp2)
    {
        DamageCalcPassthrough combinedPassthrough = new()
        {
            Min = dcp1.Min + dcp2.Min,
            Var = dcp1.Var + dcp2.Var,
            CritCapped = dcp1.CritCapped | dcp2.CritCapped
        };

        return combinedPassthrough;
    }
}