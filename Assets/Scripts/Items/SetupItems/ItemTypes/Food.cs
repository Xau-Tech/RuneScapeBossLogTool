using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Food", menuName = "Setup/ItemTypes/Food", order = 1)]
public class Food : SetupItem, ICloneable
{
    public Food(FoodSO foodData) : base(new Item(foodData.itemID, foodData.itemName, 0), foodData.isStackable, foodData.itemSprite)
    {

    }

    public override object Clone()
    {
        return MemberwiseClone() as Food;
    }

    public override SetupItemCategories GetItemCategory()
    {
        return SetupItemCategories.Food;
    }

    public override ulong GetValue()
    {
        return price;
    }

    public override void SetIsEquipped(bool flag) { }
}
