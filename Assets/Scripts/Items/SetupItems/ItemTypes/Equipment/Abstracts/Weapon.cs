//  Abstract weapon class
public class Weapon : EquippedItem
{
    public AttackType.CombatClasses combatStyle { get; private set; }
    public AttackType.AttackStyles attackStyle { get; private set; }
    public int accuracyTier { get; private set; }

    public AttackType.AttackStyles AttackStyle { get { return attackType.AttackStyle; } }
    public AttackType.CombatClasses CombatStyle { get { return attackType.CombatClass; } }

    private AttackType attackType;

    public Weapon(NondegradeWeaponSO weaponData) : base(weaponData)
    {
        this.combatStyle = weaponData.combatStyle;
        this.attackStyle = weaponData.attackStyle;
    }
    public Weapon(DegradableWeaponSO weaponData) : base(weaponData)
    {
        this.combatStyle = weaponData.combatStyle;
        this.attackStyle = weaponData.attackStyle;
    }
    public Weapon(AugWeaponSO weaponData) : base(weaponData)
    {
        this.combatStyle = weaponData.combatStyle;
        this.attackStyle = weaponData.attackStyle;
    }
    public Weapon(AttackType.AttackStyles attackStyle, int accuracyTier)
    {
        this.attackType = new AttackType(attackStyle);
        this.accuracyTier = accuracyTier;
    }

    public override ulong GetValue()
    {
        throw new System.NotImplementedException();
    }
}
