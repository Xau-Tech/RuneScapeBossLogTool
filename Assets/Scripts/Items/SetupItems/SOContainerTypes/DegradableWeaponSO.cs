using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Data holder for degradable weapon
[CreateAssetMenu(fileName = "DegradableWeapon", menuName = "Setup/Weapon/Degradable", order = 2)]
public class DegradableWeaponSO : EquippedItemSO
{
    public int effectiveCharges;
}
