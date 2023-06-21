using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAbility
{
    //  Properties & fields

    public AbilityInfo.DamageTypeCategory DamageType { get { return m_DamageType; } }
    public AbilityInfo.AbilityTypeCategory AbilityType { get { return m_AbilityType; } }


    private readonly AbilityInfo.DamageTypeCategory m_DamageType;
    private readonly AbilityInfo.AbilityTypeCategory m_AbilityType;
    private readonly double m_MinDamage;     //  Set as a percent
    private readonly double m_MaxDamage;     //  Set as a percent

    //  Constructor

    public SubAbility(double minDamage, double maxDamage, int damageType, int abilityType) 
    {
    #if UNITY_EDITOR
        int numDamageTypes = Enum.GetNames(typeof(AbilityInfo.DamageTypeCategory)).Length - 1;
        int numAbililtyTypes = Enum.GetNames(typeof(AbilityInfo.AbilityTypeCategory)).Length - 1;

        if (damageType > numDamageTypes || abilityType > numAbililtyTypes)
            throw new System.Exception(" has an impossible value for its damage type or ability type in the JSON!");
    #endif

        m_MinDamage = minDamage;
        m_MaxDamage = maxDamage;
        m_DamageType = (AbilityInfo.DamageTypeCategory)damageType;
        m_AbilityType = (AbilityInfo.AbilityTypeCategory)abilityType;
    }

    //  Methods

    public double DamageRange()
    {
        return m_MaxDamage - m_MinDamage;
    }
}