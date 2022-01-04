using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Summoning pouch setup item
/// </summary>
public class SummoningPouch : SetupItem, ICloneable
{
    //  Constructor

    public SummoningPouch(SummPouchSO pouchData) : base(new Item(pouchData._itemId, pouchData._itemName, 0), pouchData._isStackable, pouchData._itemSprite) { }

    //  Methods

    public override object Clone()
    {
        return MemberwiseClone() as SummoningPouch;
    }

    public override Enums.SetupItemCategory GetItemCategory()
    {
        return Enums.SetupItemCategory.Familiars;
    }

    public override ulong GetValue()
    {
        return Price;
    }

    public override void SetIsEquipped(bool flag) { }
}
