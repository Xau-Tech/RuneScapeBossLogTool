using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveBoost : IBoostType
{
    private readonly double m_BoostAmount;

    public AdditiveBoost(double boostAmount)
    {
        m_BoostAmount = boostAmount;
    }

    public double Calculate(double valueToBoost)
    {
        return valueToBoost + m_BoostAmount;
    }
}
