//  Weapon that doesn't degrade with use
public class NondegradableWeapon : Weapon
{
    public NondegradableWeapon(NondegradeWeaponSO weaponData) : base(weaponData) { }

    //  No degrade means 0 cost/hr!
    public override ulong GetValue()
    {
        return 0;
    }
}
