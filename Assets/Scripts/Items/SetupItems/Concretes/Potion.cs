using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Potion setup items
/// </summary>
public class Potion : SetupItem, ICloneable
{
    //  Constructor

    public Potion(PotionSO potionData) : base(new Item(potionData._itemId, potionData._itemName, 0), potionData._isStackable, potionData._itemSprite) { }

    //  Methods

    public override object Clone()
    {
        return MemberwiseClone() as Potion;
    }

    public override Enums.SetupItemCategory GetItemCategory()
    {
        return Enums.SetupItemCategory.Potion;
    }

    public override ulong GetValue()
    {
        return Price;
    }

    public override void SetIsEquipped(bool flag) { }
}
