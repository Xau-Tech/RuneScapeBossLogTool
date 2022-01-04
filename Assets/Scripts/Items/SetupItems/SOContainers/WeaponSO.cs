using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract ScriptableObject for any weapons
/// </summary>
public abstract class WeaponSO : EquipableItemSO
{
    public Enums.CombatClasses _combatStyle;
    public Enums.AttackStyles _attackStyle;
}
