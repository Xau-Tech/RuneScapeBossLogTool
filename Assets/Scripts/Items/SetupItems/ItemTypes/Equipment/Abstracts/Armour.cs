using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Abstract armour class
public abstract class Armour : EquippedItem
{
    public Armour(NondegradeArmourSO armourData) : base(armourData) { }
    public Armour(DegradableArmourSO armourData) : base(armourData) { }
    public Armour(AugArmourSO armourData) : base(armourData) { }
}
