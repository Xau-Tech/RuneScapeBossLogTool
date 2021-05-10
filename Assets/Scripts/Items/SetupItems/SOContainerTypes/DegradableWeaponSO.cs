using UnityEngine;

//  Data holder for degradable weapon
[CreateAssetMenu(fileName = "DegradableWeapon", menuName = "Setup/Weapons/Degradable", order = 2)]
public class DegradableWeaponSO : WeaponSO
{
    public int effectiveCharges;
    public bool smithingCostReduction;
}
