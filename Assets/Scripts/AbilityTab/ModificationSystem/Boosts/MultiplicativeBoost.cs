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
        return (valueToBoost * m_BoostAmount).RoundDown();
    }

    public static MultiplicativeBoost operator +(MultiplicativeBoost boost1, MultiplicativeBoost boost2)
    {
        return new(boost1.m_BoostAmount + boost2.m_BoostAmount - 1.0d);
    }
}