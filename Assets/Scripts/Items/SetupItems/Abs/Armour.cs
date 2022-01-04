using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for any armour setup items
/// </summary>
public abstract class Armour : EquipableItem
{
    public Armour(NondegradeArmourSO armourData) : base(armourData) { }
    public Armour(DegradableArmourSO armourData) : base(armourData) { }
    public Armour(TimeDegradeArmourSO armourData) : base(armourData) { }
    public Armour(AugArmourSO armourData) : base(armourData) { }
}