using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject for any armour that degrades using an internal charge mechanic
/// </summary>
[CreateAssetMenu(fileName = "DegradableArmour", menuName = "Setup/Armour/Degradable", order = 2)]
public class DegradableArmourSO : EquipableItemSO
{
    public int _effectiveCharges;
    public bool _hasSmithCostReduction;
}
