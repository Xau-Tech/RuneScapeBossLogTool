using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject for any weapon that degrades using internal charges
/// </summary>
[CreateAssetMenu(fileName = "DegradableWeapon", menuName = "Setup/Weapons/Degradable", order = 2)]
public class DegradableWeaponSO : WeaponSO
{
    public int _effectiveCharges;
    public bool _smithingCostReduction;
}
