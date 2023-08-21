using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritCapModifier : IModifier<int>
{
    private readonly IBoostType m_BoostType;

    public CritCapModifier(IBoostType boostType)
    {
        m_BoostType = boostType;
    }

    public int Apply(in int objToModify)
    {
        return (int)m_BoostType.Calculate(objToModify);
    }
}