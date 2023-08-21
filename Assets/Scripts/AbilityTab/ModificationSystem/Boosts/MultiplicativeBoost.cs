using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicativeBoost : IBoostType
{
    private readonly double m_BoostAmount;

    public MultiplicativeBoost(double boostAmount)
    {
        m_BoostAmount = boostAmount;
    }

    public double Calculate(double valueToBoost)
    {
        return valueToBoost * m_BoostAmount;
    }
}