using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject for any armour that is charged directly in units of time
/// </summary>
[CreateAssetMenu(fileName = "TimeDegradeArmour", menuName = "Setup/Armour/TimeDegrade", order = 3)]
public class TimeDegradeArmourSO : EquipableItemSO
{
    public int _maxChargeInHours;
}
