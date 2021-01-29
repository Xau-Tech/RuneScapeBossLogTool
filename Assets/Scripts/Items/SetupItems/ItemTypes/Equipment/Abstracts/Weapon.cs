//  Abstract weapon class
public abstract class Weapon : EquippedItem
{
    public Weapon(NondegradeWeaponSO weaponData) : base(weaponData) { }
    public Weapon(DegradableWeaponSO weaponData) : base(weaponData) { }
    public Weapon(AugWeaponSO weaponData) : base(weaponData) { }
}
