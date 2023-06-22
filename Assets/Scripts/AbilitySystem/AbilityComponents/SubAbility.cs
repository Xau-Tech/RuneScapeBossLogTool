using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SubAbility
{
    //  Properties & fields

    public AbilityInfo.DamageTypeCategory DamageType { get { return m_DamageType; } }
    public AbilityInfo.AbilityTypeCategory AbilityType { get { return m_AbilityType; } }

    private readonly AbilityInfo.DamageTypeCategory m_DamageType;
    private readonly AbilityInfo.AbilityTypeCategory m_AbilityType;
    private readonly double m_MinDamage;     //  Set as a percent
    private readonly double m_MaxDamage;     //  Set as a percent

    //  Constructors

    public SubAbility(JToken subAbilJson, string name = "") 
    {
        m_MinDamage = Convert.ToDouble(subAbilJson["min"]);
        m_MaxDamage = Convert.ToDouble(subAbilJson["max"]);
        m_DamageType = (AbilityInfo.DamageTypeCategory)Convert.ToInt32(subAbilJson["damageType"]);
        m_AbilityType = (AbilityInfo.AbilityTypeCategory)Convert.ToInt32(subAbilJson["abilityType"]);

        //  Do some enum bounds checking in Editor
#if UNITY_EDITOR
        int numDamageTypes = Enum.GetNames(typeof(AbilityInfo.DamageTypeCategory)).Length - 1;
        int numAbilityTypes = Enum.GetNames(typeof(AbilityInfo.AbilityTypeCategory)).Length - 1;

        if ((int)m_DamageType > numDamageTypes || (int)m_AbilityType > numAbilityTypes)
            throw new System.Exception($"{name} has an impossible value for its damage type or ability type in the JSON!");
#endif
    }

    //  Methods

    public double DamageRange()
    {
        return m_MaxDamage - m_MinDamage;
    }

    public override string ToString()
    {
        return $"\tSub-Ability [ Min: {m_MinDamage}, Max: {m_MaxDamage}, Damage type: {m_DamageType}, Ability type: {m_AbilityType} ]";
    }
}