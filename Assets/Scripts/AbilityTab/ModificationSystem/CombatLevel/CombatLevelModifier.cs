using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLevelModifier : IModifier<byte>
{
    private readonly IBoostType m_BoostType;

    public CombatLevelModifier(IBoostType boostType)
    {
        m_BoostType = boostType;
    }

    public byte Apply(in byte objToModify)
    {
        return (byte)m_BoostType.Calculate(objToModify);
    }
}
