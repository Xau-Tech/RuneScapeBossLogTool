using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;

public class Ability
{
    //  Properties & fields

    public string Id { get { return m_Id; } }

    private readonly string m_Id;
    private readonly string m_Name;
    private readonly Sprite m_Sprite;
    private readonly sbyte m_Length;           //  Length is stored in ticks where one tick is .6 seconds
    private readonly List<SubAbility> m_subAbilities;

    //  Constructors

    public Ability(JToken abilJson)
    {
        m_subAbilities = new();
        m_Id = Convert.ToString(abilJson["id"]);
        m_Name = Convert.ToString(abilJson["name"]);
        m_Length = Convert.ToSByte(abilJson["length"]);

        //  Iterate, build, and add each SubAbility
        var subAbilArr = abilJson["subAbilities"];
        foreach(var subAbilJson in subAbilArr)
        {
#if UNITY_EDITOR
            m_subAbilities.Add(new SubAbility(subAbilJson, m_Name));
#else
            m_subAbilities.Add(new SubAbility(subAbilJson));
#endif
        }

        Debug.Log(ToString());
    }

    //  Methods

    public override string ToString()
    {
        string subAbilInfo = "";
        foreach(SubAbility sa in m_subAbilities)
        {
            subAbilInfo += $"{sa}\n";
        }

        return $"Ability [ Id: {m_Id}, Name: {m_Name}, Length: {m_Length} ]\n{subAbilInfo}";
    }
}
