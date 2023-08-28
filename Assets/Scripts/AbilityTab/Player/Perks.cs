using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perks
{
    public byte PreciseRank
    {
        get { return m_PreciseRank; }
        set
        {
            m_PreciseRank = value;
            EventManager.Instance.AbilityInputChanged();
        }
    }
    public byte EquilibriumRank
    {
        get { return m_EquilibriumRank; }
        set
        {
            m_EquilibriumRank = value;
            EventManager.Instance.AbilityInputChanged();
        }
    }

    private byte m_PreciseRank = 0;
    private byte m_EquilibriumRank = 0;

    public Perks()
    {

    }
}
