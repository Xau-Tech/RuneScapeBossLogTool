using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritCap
{
    public int ModdedMaxCrit { get { return m_ModdedMaxCrit; } }

    private int m_ModdedMaxCrit;
    private IModdableUnit m_ModdableUnit;
    private readonly int m_BaseMaxCrit = 12000;
    
    public CritCap()
    {
        m_ModdedMaxCrit = m_BaseMaxCrit;

        List<IModifier> critCapMods = new();

        AdditiveBoost grimBoost = new(3000);
        CritCapModifier ccm = new(grimBoost);
        critCapMods.Add(ccm);

        MultiplicativeBoost tempestBoost = new(1.30d);
        ccm = new(tempestBoost);
        critCapMods.Add(ccm);

        ModdableUnit mu = new(m_BaseMaxCrit, critCapMods);
        m_ModdableUnit = mu;
    }

    public void Modify()
    {
        m_ModdedMaxCrit = (int)m_ModdableUnit.ModifyObject();
        Debug.Log("Modded value is " + m_ModdedMaxCrit);
    }
}