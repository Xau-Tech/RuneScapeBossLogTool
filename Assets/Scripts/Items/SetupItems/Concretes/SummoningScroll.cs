using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Summoning scroll setup item
/// </summary>
public class SummoningScroll : SetupItem, ICloneable
{
    //  Constructor

    public SummoningScroll(SummScrollSO scrollData) : base(new Item(scrollData._itemId, scrollData._itemName, 0), scrollData._isStackable, scrollData._itemSprite) { }

    //  Methods

    public override object Clone()
    {
        return MemberwiseClone() as SummoningScroll; 
    }

    public override Enums.SetupItemCategory GetItemCategory()
    {
        return Enums.SetupItemCategory.Scrolls;
    }

    public override ulong GetValue()
    {
        return Price;
    }

    public override void SetIsEquipped(bool flag) { }
}
