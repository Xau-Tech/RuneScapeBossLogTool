using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Any food setup item
/// </summary>
public class Food : SetupItem, ICloneable
{
    //  Constructor

    public Food(FoodSO foodData) : base(new Item(foodData._itemId, foodData._itemName, 0), foodData._isStackable, foodData._itemSprite) { }

    //  Methods

    public override object Clone()
    {
        return MemberwiseClone() as Food;
    }

    public override Enums.SetupItemCategory GetItemCategory()
    {
        return Enums.SetupItemCategory.Food;
    }

    public override ulong GetValue()
    {
        return Price;
    }

    public override void SetIsEquipped(bool flag) { }
}
