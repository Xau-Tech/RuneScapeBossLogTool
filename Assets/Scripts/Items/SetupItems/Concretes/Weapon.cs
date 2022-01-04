using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for any weapon setup items
/// </summary>
public class Weapon : EquipableItem
{
    //  Properties & fields
    
    public Enums.CombatClasses combatStyle { get; private set; }
    public Enums.AttackStyles attackStyle { get; private set; }
    public int AccuracyTier { get; private set; }
    public Enums.AttackStyles AttackStyle { get { return _attackType.AttackStyle; } }
    public Enums.CombatClasses CombatStyle { get { return _attackType.CombatClass; } }

    private AttackType _attackType;

    //  Constructor

    public Weapon(NondegradeWeaponSO weaponData) : base(weaponData)
    {
        this.combatStyle = weaponData._combatStyle;
        this.attackStyle = weaponData._attackStyle;
    }
    public Weapon(DegradableWeaponSO weaponData) : base(weaponData)
    {
        this.combatStyle = weaponData._combatStyle;
        this.attackStyle = weaponData._attackStyle;
    }
    public Weapon(AugWeaponSO weaponData) : base(weaponData)
    {
        this.combatStyle = weaponData._combatStyle;
        this.attackStyle = weaponData._attackStyle;
    }
    public Weapon(Enums.AttackStyles attackStyle, int accuracyTier)
    {
        this._attackType = new AttackType(attackStyle);
        this.AccuracyTier = accuracyTier;
    }

    //  Methods

    public override ulong GetValue()
    {
        throw new System.NotImplementedException();
    }
}
