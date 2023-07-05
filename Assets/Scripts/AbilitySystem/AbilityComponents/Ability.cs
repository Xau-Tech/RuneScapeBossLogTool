using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;

/// <summary>
/// Ability objects are made up of 1 to n sub-abilities as some Abilities apply multiple hits of varying types and damage ranges
/// </summary>
public class Ability
{
    //  Properties & fields

    public string Id { get { return m_Id; } }
    public AbilityInfo.AbilityTypeCategory AbilityType { get { return m_AbilityType; } }
    public AbilityInfo.CombatStyle CombatStyle { get { return m_CombatStyle; } }
    public AbilityInfo.WeaponStyle WeaponStyle { get { return m_WeaponStyle; } }

    private readonly string m_Id;
    private readonly string m_Name;
    private readonly Sprite m_Sprite;
    private readonly sbyte m_Length;           //  Length is stored in ticks where one tick is .6 seconds
    private readonly AbilityInfo.AbilityTypeCategory m_AbilityType;
    private readonly AbilityInfo.CombatStyle m_CombatStyle;
    private readonly AbilityInfo.WeaponStyle m_WeaponStyle;
    private readonly List<SubAbility> m_subAbilities;

    //  Constructors

    public Ability(JToken abilJson)
    {
        m_subAbilities = new();
        m_Id = Convert.ToString(abilJson["id"]);
        m_Name = Convert.ToString(abilJson["name"]);
        m_Length = Convert.ToSByte(abilJson["length"]);
        m_AbilityType = (AbilityInfo.AbilityTypeCategory)Convert.ToInt32(abilJson["abilityType"]);
        m_CombatStyle = (AbilityInfo.CombatStyle)Convert.ToInt32(abilJson["combatStyle"]);
        m_WeaponStyle = (AbilityInfo.WeaponStyle)Convert.ToInt32(abilJson["weaponStyle"]);

        //  Editor checking to ensure no improper values are set in the JSON file
#if UNITY_EDITOR
        int numAbilityTypes = Enum.GetNames(typeof(AbilityInfo.AbilityTypeCategory)).Length - 1;
        int numCombatStyles = Enum.GetNames(typeof(AbilityInfo.CombatStyle)).Length - 1;
        int numWeaponStyles = Enum.GetNames(typeof(AbilityInfo.WeaponStyle)).Length - 1;

        if ((int)m_AbilityType > numAbilityTypes || (int)m_CombatStyle > numCombatStyles || (int)m_WeaponStyle > numWeaponStyles)
            throw new System.Exception($"{m_Name} has an impossible value for its ability type, combat style, and/or weapon style in the JSON!");
#endif

        //  Iterate, build, and add each SubAbility - Editor constructor with name is to simplify finding the error causing Ability in the JSON if needed
        var subAbilArr = abilJson["subAbilities"];
        foreach(var subAbilJson in subAbilArr)
        {
#if UNITY_EDITOR
            m_subAbilities.Add(new SubAbility(subAbilJson, m_Name));
#else
            m_subAbilities.Add(new SubAbility(subAbilJson));
#endif
        }

        //Debug.Log(ToString());
    }

    //  Methods

    public override string ToString()
    {
        string subAbilInfo = "";
        foreach(SubAbility sa in m_subAbilities)
        {
            subAbilInfo += $"{sa}\n";
        }

        return $"Ability [ Id: {m_Id}, Name: {m_Name}, Length: {m_Length}, AbilType: {m_AbilityType}, CombatStyle: {m_CombatStyle}, WeaponStyle: {m_WeaponStyle} ]\n{subAbilInfo}";
    }
}
