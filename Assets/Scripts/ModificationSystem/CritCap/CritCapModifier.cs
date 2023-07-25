using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritCapModifier : IModifier
{
    private readonly IBoostType m_BoostType;

    public CritCapModifier(IBoostType boostType)
    {
        m_BoostType = boostType;
    }

    public object Apply(object objToModify)
    {
        return (int)m_BoostType.Calculate((int)objToModify);
    }
}