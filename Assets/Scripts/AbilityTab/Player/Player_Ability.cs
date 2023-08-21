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
    public short AbilityDamage { get { return m_AbilityDamage; } }
    public byte WeaponDamageTier 
    { 
        set 
        { 
            m_WeaponDamageTier = value;
            DetermineAbilityDamage();
        }
        get { return m_WeaponDamageTier; }
    }
    public byte BoostedCombatLevel
    {
        set
        {
            m_BoostedCombatLevel = value;
            DetermineAbilityDamage();
        }
        get { return m_BoostedCombatLevel; }
    }
    public byte EquipmentBonus
    {
        set
        {
            m_EquipmentBonus = value;
            DetermineAbilityDamage();
        }
        get { return m_EquipmentBonus; }
    }

    private static Player_Ability m_Instance;
    private short m_AbilityDamage;
    private byte m_WeaponDamageTier = 1;
    private byte m_BoostedCombatLevel = 1;
    private byte m_EquipmentBonus = 0;

    public Player_Ability()
    {
        DetermineAbilityDamage();
    }

    private void DetermineAbilityDamage()
    {
        short damageTier = (short)(m_WeaponDamageTier * 9.6 * 1.5);     //  1.5 is the standard for 2h/dw but can be different if using only mh/oh - may change later if I decide to add this mechanic
        short levelDamage = (short)(m_BoostedCombatLevel * 3.75);       //  Same as above except this value is the above one times 2.5
        short equipmentDamage = (short)(m_EquipmentBonus * 1.5);

        m_AbilityDamage = (short)(damageTier + levelDamage + equipmentDamage);
    }
}