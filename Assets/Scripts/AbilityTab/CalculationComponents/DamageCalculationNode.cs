using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageCalculationNode
{
    protected Player_Ability m_PlayerAbil;
    protected DamageCalcPassthrough m_PrevNode;
    protected SubAbility m_SubAbility;

    public virtual DamageCalcPassthrough Calculate(Player_Ability playerAbil, DamageCalcPassthrough prevNode, SubAbility subAbililty)
    {
        m_PlayerAbil = playerAbil;
        m_PrevNode = prevNode;
        m_SubAbility = subAbililty;
        DamageCalcPassthrough newNode;

        newNode.Min = CalculateMin();
        newNode.Max = CalculateMax();

        return newNode;
    }

    protected abstract double CalculateMin();
    protected abstract double CalculateMax();
}

public struct DamageCalcPassthrough
{
    public double Min;
    public double Max;
}