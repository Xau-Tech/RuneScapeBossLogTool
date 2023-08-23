using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ability
{
    public static Player_Ability Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = new();
            return m_Instance;
        }
    }
    public AbilityDamage AbilDamage { get; set; }

    private static Player_Ability m_Instance;

    public Player_Ability()
    {
        AbilDamage = new();
    }
}