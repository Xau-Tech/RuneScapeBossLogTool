using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

/// <summary>
/// Each SubAbility has a min and max damage, a damage type, and a base number of hits
/// Bleeds & channeled abilities overwhelmingly do the same damage on each hit
/// In cases where the number of hits is modified by buffs or the damage range is changed, getters that add in those modifier while retaining the immutable base value will be provided
/// </summary>
public class SubAbility
{
    public AbilityInfo.DamageTypeCategory DamageType { get { return m_DamageType; } }
    public double MinDamage { get { return m_MinDamage; } }
    public ushort BaseNumHits { get { return m_BaseNumHits; } }

    private readonly AbilityInfo.DamageTypeCategory m_DamageType;
    private readonly double m_MinDamage;     //  Set as a the percent ability damage converted to a double
    private readonly double m_MaxDamage;     //  ""
    private readonly ushort m_BaseNumHits = 1;

    public SubAbility(JToken subAbilJson, string name = "") 
    {
        m_MinDamage = Convert.ToDouble(subAbilJson["min"]);
        m_MaxDamage = Convert.ToDouble(subAbilJson["max"]);
        m_DamageType = (AbilityInfo.DamageTypeCategory)Convert.ToInt32(subAbilJson["damageType"]);

        //  Do some enum bounds checking in Editor
#if UNITY_EDITOR
        int numDamageTypes = Enum.GetNames(typeof(AbilityInfo.DamageTypeCategory)).Length - 1;

        if ((int)m_DamageType > numDamageTypes)
            throw new System.Exception($"{name} has an impossible value for its damage type in the JSON!");
#endif

        if (m_DamageType != AbilityInfo.DamageTypeCategory.Normal)
            m_BaseNumHits = Convert.ToUInt16(subAbilJson["baseNumHits"]);
    }

    public double DamageRange()
    {
        return m_MaxDamage - m_MinDamage;
    }

    public override string ToString()
    {
        return $"\tSub-Ability [ Min: {m_MinDamage}, Max: {m_MaxDamage}, Damage type: {m_DamageType}, BaseNumHits: {m_BaseNumHits} ]";
    }
}